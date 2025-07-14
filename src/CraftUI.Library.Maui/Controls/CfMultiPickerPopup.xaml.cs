using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CraftUI.Library.Maui.Common;
using CraftUI.Library.Maui.Common.Extensions;
using CraftUI.Library.Maui.Controls.Popups;

namespace CraftUI.Library.Maui.Controls;

public partial class CfMultiPickerPopup
{
    private CfCollectionMultiSelectionPopup? _collectionPopup;
    private readonly TapGestureRecognizer _tapGestureRecognizer;

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CfMultiPickerPopup));
    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(IList<object>), typeof(CfMultiPickerPopup), defaultBindingMode: BindingMode.TwoWay, propertyChanged: SelectedItemsPropertyChanged, coerceValue: CoerceSelectedItems, defaultValueCreator: DefaultValueCreator);
    public static readonly BindableProperty ItemDisplayProperty = BindableProperty.Create(nameof(ItemDisplay), typeof(string), typeof(CfMultiPickerPopup), defaultBindingMode: BindingMode.OneWay);
    public static readonly BindableProperty DefaultValueProperty = BindableProperty.Create(nameof(DefaultValue), typeof(string), typeof(CfMultiPickerPopup), defaultBindingMode: BindingMode.OneWay);
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(CfMultiPickerPopup), defaultBindingMode: BindingMode.OneWay);
    public static readonly BindableProperty SelectionChangedCommandProperty = BindableProperty.Create(nameof(SelectionChangedCommand), typeof(ICommand), typeof(CfMultiPickerPopup));
    
    public ObservableCollection<string> SelectedStrings { get; set; }

    public IList? ItemsSource
    {
        get => (IList?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public IList<object>? SelectedItems
    {
        get => (IList<object>?)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, new SelectionList(this, value));
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
    
    public ICommand? SelectionChangedCommand
    {
        get => (ICommand?)GetValue(SelectionChangedCommandProperty);
        set => SetValue(SelectionChangedCommandProperty, value);
    }

    public CfMultiPickerPopup()
    {
        InitializeComponent();
        
        _tapGestureRecognizer = new TapGestureRecognizer();
        _tapGestureRecognizer.Tapped += OnTapped;

        SelectedStrings = new ObservableCollection<string>();
        
        GestureRecognizers.Add(_tapGestureRecognizer);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        ActionIconSource ??= "chevron_bottom.png";
        ActionIconCommand ??= new Command(() => OnTapped(null, EventArgs.Empty));
    }

    private static void SelectedItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var selectableItemsView = (CfMultiPickerPopup)bindable;
        var oldSelection = (IList<object>)oldValue;
        var newSelection = (IList<object>)newValue;

        selectableItemsView.SelectedItemsPropertyChanged(oldSelection, newSelection);
    }

    private static object CoerceSelectedItems(BindableObject bindable, object? value)
    {
        if (value == null)
        {
            return new SelectionList((CfMultiPickerPopup)bindable);
        }

        if (value is SelectionList)
        {
            return value;
        }

        return new SelectionList((CfMultiPickerPopup)bindable, value as IList<object>);
    }

    private static object DefaultValueCreator(BindableObject bindable)
    {
        return new SelectionList((CfMultiPickerPopup)bindable);
    }

    internal void SelectedItemsPropertyChanged(IList<object> oldSelection, IList<object> newSelection)
    {
        UpdateSelectedStrings();
        OnPropertyChanged(SelectedItemsProperty.PropertyName);
    }

    private void UpdateSelectedStrings()
    {
        SelectedStrings.Clear();

        if (SelectedItems is null)
        {
            return;
        }
        
        foreach (var item in SelectedItems)
        {
            var displayValue = item.GetDisplayString(ItemDisplay);
            if (!string.IsNullOrEmpty(displayValue) && !SelectedStrings.Contains(displayValue))
            {
                SelectedStrings.Add(displayValue);
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
        _collectionPopup = new CfCollectionMultiSelectionPopup
        {
            BindingContext = this,
            Title = !string.IsNullOrEmpty(Title) ? Title : Label,
            ItemsSource = ItemsSource,
            SelectedItems = SelectedItems,
            ItemDisplay = ItemDisplay
        };
        
        _collectionPopup.Closed += (_, _) =>
        {
            SelectionChangedCommand?.Execute(null);
        };
        
        _collectionPopup.SetBinding(CfCollectionMultiSelectionPopup.ItemsSourceProperty, path: nameof(ItemsSource));
        _collectionPopup.SetBinding(CfCollectionMultiSelectionPopup.SelectedItemsProperty, path: nameof(SelectedItems));

        Shell.Current.ShowPopup(_collectionPopup);
    }
}