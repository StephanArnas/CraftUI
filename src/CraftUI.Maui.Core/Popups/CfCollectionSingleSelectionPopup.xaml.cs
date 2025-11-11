using System.Collections;
using CraftUI.Maui.Core.Common;
using CraftUI.Maui.Core.Common.Helpers;

namespace CraftUI.Maui.Core.Popups;

public partial class CfCollectionSingleSelectionPopup
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CfCollectionSingleSelectionPopup), propertyChanged: TitleChanged, defaultBindingMode: BindingMode.OneWayToSource);
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(CfCollectionSingleSelectionPopup), propertyChanged: ItemsSourceChanged);
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(CfCollectionSingleSelectionPopup), defaultBindingMode: BindingMode.TwoWay);
    public static readonly BindableProperty ItemDisplayProperty = BindableProperty.Create(nameof(ItemDisplay), typeof(string), typeof(CfCollectionSingleSelectionPopup), defaultBindingMode: BindingMode.OneWayToSource);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

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

    public string? ItemDisplay
    {
        get => (string?)GetValue(ItemDisplayProperty);
        set
        {
            SetValue(ItemDisplayProperty, value);
            InitCollectionViewItems();
        }
    }

    public CfCollectionSingleSelectionPopup()
    {
        InitializeComponent();
        
        var tapped = new TapGestureRecognizer();
        tapped.Tapped += async (_, _) => await CloseAsync();
        CloseImage.GestureRecognizers.Add(tapped); 
    }

    private static void TitleChanged(BindableObject bindable, object oldValue, object newValue) => ((CfCollectionSingleSelectionPopup)bindable).UpdateTitleView();
    private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) => ((CfCollectionSingleSelectionPopup)bindable).UpdateItemsSourceView();

    private void UpdateTitleView() => TitleLabel.Text = Title;

    private void UpdateItemsSourceView()
    {
        PickerCollectionView.ItemsSource = ItemsSource;
    }

    private void InitCollectionViewItems()
    {
        PickerCollectionView.ItemTemplate = new DataTemplate(() =>
        {
            var contentView = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(16, 14),
                WidthRequest = ScreenSize.WidthScreen,
                BackgroundColor = Colors.Transparent
            };

            contentView.Triggers.Add(new DataTrigger(typeof(ContentView))
            {
                Binding = new Binding("."),
                Value = SelectedItem,
                Setters =
                {
                    new Setter
                    {
                        Property = VisualElement.BackgroundColorProperty,
                        Value = ResourceHelper.GetThemeColor("PrimaryDark", "Primary")
                    }
                }
            });
            
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, _) =>
            {
                SelectedItem = ((ContentView)s!).BindingContext;
                await CloseAsync();
            };
            contentView.GestureRecognizers.Add(tapGestureRecognizer);

            var label = new Label
            {
                FontSize = 16,
                HorizontalOptions = LayoutOptions.Fill,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            label.SetBinding(Label.TextProperty, new Binding(ItemDisplay));
            
            contentView.Content = label;

            return contentView;
        });
        
        PickerCollectionView.MaximumHeightRequest = ScreenSize.HeightScreen / 1.3;
    }
}