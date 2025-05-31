using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Maui.Views;
using CraftUI.Library.Maui.Common.Extensions;
using CraftUI.Library.Maui.Controls.Popups;

namespace CraftUI.Library.Maui.Controls;

public partial class CfMultiPickerPopup
{
    private CfCollectionMultiSelectionPopup? _collectionPopup;
    private readonly TapGestureRecognizer _tapGestureRecognizer;

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CfMultiPickerPopup));
    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(ObservableCollection<object>), typeof(CfMultiPickerPopup), defaultValue: new ObservableCollection<object>(), defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedItemsChanged);
    public static readonly BindableProperty ItemDisplayProperty = BindableProperty.Create(nameof(ItemDisplay), typeof(string), typeof(CfMultiPickerPopup), defaultBindingMode: BindingMode.OneWay);
    public static readonly BindableProperty DefaultValueProperty = BindableProperty.Create(nameof(DefaultValue), typeof(string), typeof(CfMultiPickerPopup), defaultBindingMode: BindingMode.OneWay);
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(CfMultiPickerPopup), defaultBindingMode: BindingMode.OneWay);
    
    // TODO: Temporary, could be removed by using SelectedItems directly.
    public ObservableCollection<string> SelectedStrings { get; set; }

    public IList? ItemsSource
    {
        get => (IList?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public ObservableCollection<object>? SelectedItems
    {
        get => (ObservableCollection<object>?)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    public string ItemDisplay
    {
        get => (string)GetValue(ItemDisplayProperty);
        set => SetValue(ItemDisplayProperty, value);
    }

    public string DefaultValue
    {
        get => (string)GetValue(DefaultValueProperty);
        set => SetValue(DefaultValueProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public CfMultiPickerPopup()
    {
        InitializeComponent();
        
        _tapGestureRecognizer = new TapGestureRecognizer();
        _tapGestureRecognizer.Tapped += OnTapped;

        SelectedStrings = new ObservableCollection<string>();
        
        GestureRecognizers.Add(_tapGestureRecognizer);
    }
    
    private static void OnSelectedItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (oldValue is ObservableCollection<object> oldCollection)
        {
            oldCollection.CollectionChanged -= ((CfMultiPickerPopup)bindable).SelectedItems_CollectionChanged;
        }

        if (newValue is ObservableCollection<object> newCollection)
        {
            newCollection.CollectionChanged += ((CfMultiPickerPopup)bindable).SelectedItems_CollectionChanged;
        }
    }
    
    private void SelectedItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e is { Action: NotifyCollectionChangedAction.Add, NewItems: not null })
        {
            foreach (var item in e.NewItems)
            {
                var propertyContent = item.GetDisplayString(propertyName: ItemDisplay);
                if (propertyContent != null && !SelectedStrings.Contains(propertyContent))
                {
                    SelectedStrings.Add(propertyContent);
                }
            }
        }
        else if (e is { Action: NotifyCollectionChangedAction.Remove, OldItems: not null })
        {
            foreach (var item in e.OldItems)
            {
                var propertyContent = item.GetDisplayString(propertyName: ItemDisplay);
                if (propertyContent != null)
                {
                    SelectedStrings.Remove(propertyContent);
                }
            }
        }
        
        OnPropertyChanged(nameof(SelectedStrings));
        // MainLayout.InvalidateMeasure();
        // MainLayout.PlatformSizeChanged();
        // InvalidateMeasure();
        // PlatformSizeChanged();
        // PlatformSizeChangedCanvasView();
        InvalidateSurfaceForCanvasView();
    }
    
    private void OnTapped(object? sender, EventArgs e)
    {
        if (SelectedItems is not { } observableSelectedItems)
        {
            observableSelectedItems = new ObservableCollection<object>();
            if (SelectedItems != null)
            {
                foreach (var item in SelectedItems)
                {
                    observableSelectedItems.Add(item);
                }
            }
            SelectedItems = observableSelectedItems;
        }
        
        _collectionPopup = new CfCollectionMultiSelectionPopup
        {
            BindingContext = this,
            Title = !string.IsNullOrEmpty(Title) ? Title : Label,
            ItemsSource = ItemsSource,
            SelectedItems = observableSelectedItems,
            ItemDisplay = ItemDisplay
        };

        _collectionPopup.SetBinding(CfCollectionMultiSelectionPopup.SelectedItemsProperty, path: nameof(SelectedItems));
        _collectionPopup.SetBinding(CfCollectionMultiSelectionPopup.ItemsSourceProperty, path: nameof(ItemsSource));

        Shell.Current.ShowPopup(_collectionPopup);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        ActionIconSource ??= "chevron_bottom.png";
        ActionIconCommand ??= new Command(() => OnTapped(null, EventArgs.Empty));
    }
}