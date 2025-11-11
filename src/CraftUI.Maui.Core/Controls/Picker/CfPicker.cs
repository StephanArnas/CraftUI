using System.Collections;

namespace CraftUI.Maui.Core.Controls.Picker;

public class CfPicker : InputTextLayout.InputTextLayout
{
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IList),
        typeof(CfPicker),
        defaultValue: null,
        propertyChanged: OnItemsSourceChanged);

    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(
        nameof(SelectedIndex),
        typeof(int),
        typeof(CfPicker),
        defaultValue: -1,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnSelectedIndexChanged);

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem),
        typeof(object),
        typeof(CfPicker),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnSelectedItemChanged);

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(CfPicker),
        defaultValue: null,
        propertyChanged: OnTitleChanged);

    private readonly Microsoft.Maui.Controls.Picker _element;

    public IList ItemsSource
    {
        get => (IList)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

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
        
        _element.SelectedIndexChanged += OnPickerSelectedIndexChanged;

        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += OnPickerTapped;
        _element.GestureRecognizers.Add(tapGesture);

        View = _element;
    }

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfPicker)bindable).UpdateItemsSourceView();

    private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfPicker)bindable).UpdateSelectedIndexView();

    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfPicker)bindable).UpdateSelectedItemView();

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfPicker)bindable).UpdateTitleView();

    private void UpdateItemsSourceView()
    {
        _element.ItemsSource = ItemsSource;
    }

    private void UpdateSelectedIndexView()
    {
        if (_element.SelectedIndex != SelectedIndex)
        {
            _element.SelectedIndex = SelectedIndex;
        }
    }

    private void UpdateSelectedItemView()
    {
        if (_element.SelectedItem != SelectedItem)
        {
            _element.SelectedItem = SelectedItem;
        }
    }

    private void UpdateTitleView()
    {
        _element.Title = Title;
    }

    private void OnPickerSelectedIndexChanged(object? sender, EventArgs e)
    {
        SelectedIndex = _element.SelectedIndex;
        SelectedItem = _element.SelectedItem;
    }

    private void OnPickerTapped(object? sender, TappedEventArgs e)
    {
        _element.Focus();
    }
}