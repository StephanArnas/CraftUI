using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Extensions;
using CraftUI.Maui.Core.Common;
using CraftUI.Maui.Core.Common.Extensions;
using CraftUI.Maui.Core.Popups;

namespace CraftUI.Maui.Core.Controls.PickerMultipleSelection;

public partial class CfPickerMultipleSelection
{
    private CfCollectionMultiSelectionPopup? _collectionPopup;
    private readonly TapGestureRecognizer _tapGestureRecognizer;

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CfPickerMultipleSelection));
    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(IList<object>), typeof(CfPickerMultipleSelection), defaultBindingMode: BindingMode.TwoWay, propertyChanged: SelectedItemsPropertyChanged, coerceValue: CoerceSelectedItems, defaultValueCreator: DefaultValueCreator);
    public static readonly BindableProperty ItemDisplayProperty = BindableProperty.Create(nameof(ItemDisplay), typeof(string), typeof(CfPickerMultipleSelection), defaultBindingMode: BindingMode.OneWay);
    public static readonly BindableProperty DefaultValueProperty = BindableProperty.Create(nameof(DefaultValue), typeof(string), typeof(CfPickerMultipleSelection), defaultBindingMode: BindingMode.OneWay);
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(CfPickerMultipleSelection), propertyChanged: ItemsSourceChanged, defaultBindingMode: BindingMode.OneWay);
    public static readonly BindableProperty SelectionChangedCommandProperty = BindableProperty.Create(nameof(SelectionChangedCommand), typeof(ICommand), typeof(CfPickerMultipleSelection));
    
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

    public CfPickerMultipleSelection()
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
    
    private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) => ((CfPickerMultipleSelection)bindable).UpdateItemsSourceView();

    private async void UpdateItemsSourceView()
    {
        if (_collectionPopup?.ItemsSource?.Count > 0 && DeviceInfo.Platform == DevicePlatform.iOS)
        {
            // On iOS, we need to close the popup before showing a new one to avoid issues with the collection view.
            await _collectionPopup.CloseAsync().ContinueWith(_ => 
            {
                MainThread.BeginInvokeOnMainThread(() => OnTapped(null, EventArgs.Empty));
            });
        }
    }
    
    private static void SelectedItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var selectableItemsView = (CfPickerMultipleSelection)bindable;
        var oldSelection = (IList<object>)oldValue;
        var newSelection = (IList<object>)newValue;

        selectableItemsView.SelectedItemsPropertyChanged(oldSelection, newSelection);
    }

    private static object CoerceSelectedItems(BindableObject bindable, object? value)
    {
        if (value == null)
        {
            return new SelectionList((CfPickerMultipleSelection)bindable);
        }

        if (value is SelectionList)
        {
            return value;
        }

        return new SelectionList((CfPickerMultipleSelection)bindable, value as IList<object>);
    }

    private static object DefaultValueCreator(BindableObject bindable)
    {
        return new SelectionList((CfPickerMultipleSelection)bindable);
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
    
    private async void OnTapped(object? sender, EventArgs e)
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

        await Shell.Current.ShowPopupAsync(_collectionPopup);
    }
}