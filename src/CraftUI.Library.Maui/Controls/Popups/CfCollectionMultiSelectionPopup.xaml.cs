using System.Collections;
using System.Collections.ObjectModel;
using CraftUI.Library.Maui.Common.Helpers;
using CraftUI.Library.Maui.Converters;
using CraftUI.Library.Maui.MarkupExtensions;

namespace CraftUI.Library.Maui.Controls.Popups;

public partial class CfCollectionMultiSelectionPopup
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(CfCollectionMultiSelectionPopup), propertyChanged: TitleChanged, defaultBindingMode: BindingMode.OneWayToSource);
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(CfCollectionMultiSelectionPopup), propertyChanged: ItemsSourceChanged);
    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(ObservableCollection<object>), typeof(CfCollectionMultiSelectionPopup), defaultBindingMode: BindingMode.TwoWay);
    public static readonly BindableProperty ItemDisplayProperty = BindableProperty.Create(nameof(ItemDisplay), typeof(string), typeof(CfCollectionMultiSelectionPopup), defaultBindingMode: BindingMode.OneWayToSource);

    public CfCollectionMultiSelectionPopup()
    {
        InitializeComponent();

        var tapped = new TapGestureRecognizer();
        tapped.Tapped += (_, _) => Close();
        CloseImage.GestureRecognizers.Add(tapped);

        SelectedItems = new ObservableCollection<object>();
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

    public ObservableCollection<object>? SelectedItems
    {
        get => (ObservableCollection<object>?)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
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

    private static void TitleChanged(BindableObject bindable, object oldValue, object newValue) => ((CfCollectionMultiSelectionPopup)bindable).UpdateTitleView();
    private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) => ((CfCollectionMultiSelectionPopup)bindable).UpdateItemsSourceView();

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
                WidthRequest = GutterSystem.WidthScreen,
                BackgroundColor = Colors.Transparent
            };

            contentView.Triggers.Add(new DataTrigger(typeof(ContentView))
            {
                Binding = new Binding
                {
                    Path = ".",
                    Converter = new SelectedItemsContainsConverter(),
                    ConverterParameter = this
                },
                Value = true,
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
                var item = ((ContentView)s!).BindingContext;
                if (SelectedItems is null)
                {
                    return;
                }
                
                if (SelectedItems.Contains(item))
                {
                    SelectedItems.Remove(item);
                }
                else
                {
                    SelectedItems.Add(item);
                }
                
                // Force only the tapped item to update its bindings
                ((ContentView)s).BindingContext = null;
                ((ContentView)s).BindingContext = item;
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