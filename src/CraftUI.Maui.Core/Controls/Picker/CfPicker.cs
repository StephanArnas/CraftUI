using System.Collections;
using System.Windows.Input;

namespace CraftUI.Maui.Core.Controls.Picker;

public class CfPicker : InputTextLayout.InputTextLayout
{
    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IList),
            typeof(CfPicker),
            propertyChanged: OnItemsSourceChanged);

    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(CfPicker),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnSelectedItemChanged);

    public static readonly BindableProperty ItemDisplayProperty =
        BindableProperty.Create(
            nameof(ItemDisplay),
            typeof(string),
            typeof(CfPicker),
            propertyChanged: OnItemDisplayBindingChanged,
            defaultBindingMode: BindingMode.OneWay);

    public static readonly BindableProperty SelectionChangedCommandProperty =
        BindableProperty.Create(
            nameof(SelectionChangedCommand),
            typeof(ICommand),
            typeof(CfPicker));

    public IList ItemsSource
    {
        get => (IList)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public string ItemDisplay
    {
        get => (string)GetValue(ItemDisplayProperty);
        set => SetValue(ItemDisplayProperty, value);
    }

    public ICommand? SelectionChangedCommand
    {
        get => (ICommand?)GetValue(SelectionChangedCommandProperty);
        set => SetValue(SelectionChangedCommandProperty, value);
    }

    private readonly Microsoft.Maui.Controls.Picker _element;

    public CfPicker()
    {
        _element = new Microsoft.Maui.Controls.Picker
        {
            FontSize = 16,
            VerticalOptions = LayoutOptions.Center,
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalOptions = LayoutOptions.Fill,
            MinimumHeightRequest = 40
        };

        _element.SetDynamicResource(Microsoft.Maui.Controls.Picker.TextColorProperty, "Gray950");

        var tap = new TapGestureRecognizer();
        tap.Tapped += OnPickerTapped;
        _element.GestureRecognizers.Add(tap);

        View = _element;

        OnItemsSourceChanged();
        OnSelectedItemChanged();
        OnItemDisplayBindingChanged();

        _element.SelectedIndexChanged += (_, __) =>
        {
            if (_element.SelectedItem != SelectedItem)
            {
                SelectedItem = _element.SelectedItem;
            }

            SelectionChangedCommand?.Execute(null);
        };
    }

    private void OnPickerTapped(object? sender, EventArgs e)
    {
        if (_element.ItemsSource == null || !_element.ItemsSource.Cast<object>().Any())
        {
            return;
        }

        _element.Unfocus();
        _element.Focus();
    }

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) => ((CfPicker)bindable).OnItemsSourceChanged();

    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue) => ((CfPicker)bindable).OnSelectedItemChanged();

    private static void OnItemDisplayBindingChanged(BindableObject bindable, object oldValue, object newValue) => ((CfPicker)bindable).OnItemDisplayBindingChanged();

    private void OnItemsSourceChanged()
    {
        _element.ItemsSource = ItemsSource;
    }

    private void OnSelectedItemChanged()
    {
        if (!Equals(_element.SelectedItem, SelectedItem))
        {
            _element.SelectedItem = SelectedItem;
        }

        SelectionChangedCommand?.Execute(null);
    }

    private void OnItemDisplayBindingChanged()
    {
        _element.ItemDisplayBinding = !string.IsNullOrWhiteSpace(ItemDisplay) ? new Binding(ItemDisplay) : null;
    }
}