using System.Collections;
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CraftUI.Library.Maui.Common.Extensions;
using CraftUI.Library.Maui.Controls.Popups;

namespace CraftUI.Library.Maui.Controls;

public partial class SaPickerPopup
{
    private SaCollectionPopup? _collectionPopup;
    private readonly TapGestureRecognizer _tapGestureRecognizer;
    
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SaPickerPopup));
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(SaPickerPopup), propertyChanged: SelectedItemChanged, defaultBindingMode: BindingMode.TwoWay);
    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(SaPickerPopup), defaultBindingMode: BindingMode.TwoWay);
    public static readonly BindableProperty ItemDisplayProperty = BindableProperty.Create(nameof(ItemDisplay), typeof(string), typeof(SaPickerPopup), defaultBindingMode: BindingMode.OneWay);
    public static readonly BindableProperty DefaultValueProperty = BindableProperty.Create(nameof(DefaultValue), typeof(string), typeof(SaPickerPopup), propertyChanged: DefaultValueChanged, defaultBindingMode: BindingMode.OneWay);
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(SaPickerPopup), defaultBindingMode: BindingMode.OneWay);

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

    public SaPickerPopup()
    {
        InitializeComponent();
        
        _tapGestureRecognizer = new TapGestureRecognizer();
        _tapGestureRecognizer.Tapped += OnTapped;

        GestureRecognizers.Add(_tapGestureRecognizer);
    }
    
    private void OnTapped(object? sender, EventArgs e)
    {
        _collectionPopup = new SaCollectionPopup
        {
            BindingContext = this,
            Title = !string.IsNullOrEmpty(Title) ? Title : Label,
            ItemsSource = ItemsSource,
            SelectedItem = SelectedItem,
            ItemDisplay = ItemDisplay,
        };

        _collectionPopup.SetBinding(SaCollectionPopup.SelectedItemProperty, "SelectedItem");
        _collectionPopup.SetBinding(SaCollectionPopup.ItemsSourceProperty, "ItemsSource");

        Shell.Current.ShowPopup(_collectionPopup);
    }
    
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        ActionIconSource ??= "chevron_bottom.png";
        ActionIconCommand ??= new Command(() => OnTapped(null, EventArgs.Empty));
    }

    private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) => ((SaPickerPopup)bindable).UpdateSelectedItemView();
    private static void DefaultValueChanged(BindableObject bindable, object oldValue, object newValue) => ((SaPickerPopup)bindable).UpdateDefaultValueView();

    private void UpdateSelectedItemView()
    {
        TapCommand?.Execute(SelectedItem);
        Element.Text = SelectedItem?.GetPropertyValue<string>(ItemDisplay) ?? string.Empty;
    }

    private void UpdateDefaultValueView()
    {
        Element.Text = DefaultValue;
    }
}