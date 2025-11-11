using System.Collections;
using System.Windows.Input;
using CommunityToolkit.Maui.Extensions;
using CraftUI.Maui.Core.Common.Extensions;
using CraftUI.Maui.Core.Common.Helpers;
using CraftUI.Maui.Core.Popups;

namespace CraftUI.Maui.Core.Controls.PickerSingleSelection;

public class CfPickerSingleSelection : InputTextLayout.InputTextLayout
{
    private CfCollectionSingleSelectionPopup? _collectionPopup;
    private readonly TapGestureRecognizer _tapGestureRecognizer;
    private readonly Label _element;

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(CfPickerSingleSelection));

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem),
        typeof(object),
        typeof(CfPickerSingleSelection),
        propertyChanged: SelectedItemChanged,
        defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
        nameof(TapCommand),
        typeof(ICommand),
        typeof(CfPickerSingleSelection),
        defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty ItemDisplayProperty = BindableProperty.Create(
        nameof(ItemDisplay),
        typeof(string),
        typeof(CfPickerSingleSelection),
        defaultBindingMode: BindingMode.OneWay);

    public static readonly BindableProperty DefaultValueProperty = BindableProperty.Create(
        nameof(DefaultValue),
        typeof(string),
        typeof(CfPickerSingleSelection),
        propertyChanged: DefaultValueChanged,
        defaultBindingMode: BindingMode.OneWay);

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IList),
        typeof(CfPickerSingleSelection),
        propertyChanged: ItemsSourceChanged,
        defaultBindingMode: BindingMode.OneWay);

    public IList? ItemsSource
    {
        get => (IList?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public ICommand? TapCommand
    {
        get => (ICommand?)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
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

    public CfPickerSingleSelection()
    {
        _element = new Label
        {
            FontSize = 16,
            VerticalOptions = LayoutOptions.Center,
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalOptions = LayoutOptions.Start,
            MinimumHeightRequest = 40
        };
        _element.SetAppThemeColor(Microsoft.Maui.Controls.Label.TextColorProperty,
            ResourceHelper.GetResource<Color>("Gray950"),
            ResourceHelper.GetResource<Color>("Gray950"));

        _tapGestureRecognizer = new TapGestureRecognizer();
        _tapGestureRecognizer.Tapped += OnTapped;

        View = _element;
        GestureRecognizers.Add(_tapGestureRecognizer);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        ActionIconSource ??= "chevron_bottom.png";
        ActionIconCommand ??= new Command(() => OnTapped(null, EventArgs.Empty));
    }

    private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfPickerSingleSelection)bindable).UpdateSelectedItemView();

    private static void DefaultValueChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfPickerSingleSelection)bindable).UpdateDefaultValueView();

    private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfPickerSingleSelection)bindable).UpdateItemsSourceView();

    private void UpdateSelectedItemView()
    {
        TapCommand?.Execute(SelectedItem);
        _element.Text = SelectedItem?.GetPropertyValue<string>(ItemDisplay) ?? string.Empty;
    }

    private void UpdateDefaultValueView()
    {
        _element.Text = DefaultValue;
    }

    private async void UpdateItemsSourceView()
    {
        if (_collectionPopup?.ItemsSource?.Count > 0 && DeviceInfo.Platform == DevicePlatform.iOS)
        {
            await _collectionPopup.CloseAsync().ContinueWith(_ =>
            {
                MainThread.BeginInvokeOnMainThread(() => OnTapped(null, EventArgs.Empty));
            });
        }
    }

    private async void OnTapped(object? sender, EventArgs e)
    {
        _collectionPopup = new CfCollectionSingleSelectionPopup
        {
            BindingContext = this,
            Title = !string.IsNullOrEmpty(Title) ? Title : Label,
            ItemsSource = ItemsSource,
            SelectedItem = SelectedItem,
            ItemDisplay = ItemDisplay
        };

        _collectionPopup.SetBinding(CfCollectionSingleSelectionPopup.SelectedItemProperty, path: nameof(SelectedItem));
        _collectionPopup.SetBinding(CfCollectionSingleSelectionPopup.ItemsSourceProperty, path: nameof(ItemsSource));

        await Shell.Current.ShowPopupAsync(_collectionPopup);
    }
}
