using System.Collections;
using CraftUI.Library.Maui.Common.Helpers;
using CraftUI.Library.Maui.MarkupExtensions;

namespace CraftUI.Library.Maui.Controls.Popups;

public partial class SaCollectionPopup
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(SaCollectionPopup), propertyChanged: TitleChanged, defaultBindingMode: BindingMode.OneWayToSource);
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IList), typeof(SaCollectionPopup));
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(SaCollectionPopup), defaultBindingMode: BindingMode.TwoWay);

    public SaCollectionPopup()
    {
        InitializeComponent();
        
        var tapped = new TapGestureRecognizer();
        tapped.Tapped += (_, _) => Close();
        CloseImage.GestureRecognizers.Add(tapped); 
    }

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

    public static readonly BindableProperty ItemDisplayProperty = BindableProperty.Create("ItemDisplay",
        typeof(string), typeof(SaCollectionPopup), defaultBindingMode: BindingMode.OneWayToSource);

    public string? ItemDisplay
    {
        get => (string?)GetValue(ItemDisplayProperty);
        set
        {
            SetValue(ItemDisplayProperty, value);
            InitCollectionViewItems();
        }
    }

    private static void TitleChanged(BindableObject bindable, object oldValue, object newValue) => ((SaCollectionPopup)bindable).UpdateTitleView();

    private void UpdateTitleView() => TitleLabel.Text = Title;

    private void InitCollectionViewItems()
    {
        PickerCollectionView.ItemTemplate = new DataTemplate(() =>
        {
            var contentView = new ContentView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(16, 14),
                WidthRequest = GutterSystem.WidthScreen,
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
            tapGestureRecognizer.Tapped += (s, _) =>
            {
                SelectedItem = ((ContentView)s!).BindingContext;
                Close();
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
        
        PickerCollectionView.MaximumHeightRequest = GutterSystem.HeightScreen / 1.3;
    }
}