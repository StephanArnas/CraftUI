namespace CraftUI.Maui.Core.Controls.DatePicker;

public class CfDatePicker : InputTextLayout.InputTextLayout
{
    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(
        nameof(PlaceHolder),
        typeof(string), 
        typeof(CfDatePicker), 
        defaultValue: "/ . / . /");
    
    public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(
        nameof(NullableDate), 
        typeof(DateTime?), 
        typeof(CfDatePicker), 
        defaultValue: null, 
        defaultBindingMode: BindingMode.TwoWay, 
        propertyChanged: OnNullableDateChanged);
    
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(
        nameof(Format), 
        typeof(string), 
        typeof(CfDatePicker), 
        defaultValue: "d", 
        propertyChanged: OnFormatChanged);
    
    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(
        nameof(MinimumDate), 
        typeof(DateTime), 
        typeof(CfDatePicker), 
        propertyChanged: OnMinimumDateChanged);
    
    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(
        nameof(MaximumDate), 
        typeof(DateTime), 
        typeof(CfDatePicker), 
        propertyChanged: OnMaximumDateChanged);
    
    public static readonly BindableProperty ShowClearButtonProperty = BindableProperty.Create(
        nameof(ShowClearButton), 
        typeof(bool),
        typeof(CfDatePicker),
        defaultValue: true, 
        propertyChanged: OnShowClearButtonChanged);

    private readonly CfDatePickerInternal _element;
    private readonly Image _closeImage;

    public string PlaceHolder
    {
        get => (string)GetValue(PlaceHolderProperty);
        set => SetValue(PlaceHolderProperty, value);
    }

    public DateTime? NullableDate
    {
        get => (DateTime?)GetValue(NullableDateProperty);
        set => SetValue(NullableDateProperty, value);
    }

    public string Format
    {
        get => (string)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    public DateTime MinimumDate
    {
        get => (DateTime)GetValue(MinimumDateProperty);
        set => SetValue(MinimumDateProperty, value);
    }

    public DateTime MaximumDate
    {
        get => (DateTime)GetValue(MaximumDateProperty);
        set => SetValue(MaximumDateProperty, value);
    }

    public bool ShowClearButton
    {
        get => (bool)GetValue(ShowClearButtonProperty);
        set => SetValue(ShowClearButtonProperty, value);
    }

    public CfDatePicker()
    {
        _element = new CfDatePickerInternal();
        _element.DateSelected += OnDateSelected;

        _closeImage = new Image
        {
            Source = "close.png",
            WidthRequest = 26,
            HeightRequest = 26,
            HorizontalOptions = LayoutOptions.End
        };

        var tapped = new TapGestureRecognizer();
        tapped.Tapped += (_, _) =>
        {
            _element.Date = DateTime.Today;
            NullableDate = null;
        };
        _closeImage.GestureRecognizers.Add(tapped);

        var grid = new Grid
        {
            ColumnDefinitions =
            [
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            ]
        };

        grid.Children.Add(_element);
        Grid.SetColumn(_element, 0);

        grid.Children.Add(_closeImage);
        Grid.SetColumn(_closeImage, 1);

        View = grid;
    }

    private static void OnFormatChanged(BindableObject bindable, object oldValue, object newValue) => ((CfDatePicker)bindable).UpdateFormatView();
    
    private static void OnMinimumDateChanged(BindableObject bindable, object oldValue, object newValue) => ((CfDatePicker)bindable).UpdateMinimumDateView();
    
    private static void OnMaximumDateChanged(BindableObject bindable, object oldValue, object newValue) => ((CfDatePicker)bindable).UpdateMaximumDateView();
    
    private static void OnShowClearButtonChanged(BindableObject bindable, object oldValue, object newValue) => ((CfDatePicker)bindable).UpdateClearButtonVisibility();
    
    private static void OnNullableDateChanged(BindableObject bindable, object oldValue, object newValue) => ((CfDatePicker)bindable).UpdateDateView();

    private void OnDateSelected(object? sender, DateChangedEventArgs e)
    {
        NullableDate = e.NewDate;
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (BindingContext != null)
        {
            _element.Format = PlaceHolder;
        }
    }

    private void UpdateFormatView()
    {
        _element.Format = Format;
        UpdateDateView();
    }

    private void UpdateMinimumDateView() => _element.MinimumDate = MinimumDate;
    
    private void UpdateMaximumDateView() => _element.MaximumDate = MaximumDate;
    
    private void UpdateClearButtonVisibility() => _closeImage.IsVisible = ShowClearButton;
    
    private void UpdateDateView()
    {
        if (NullableDate.HasValue)
        {
            _element.Date = NullableDate.Value;
            _element.Format = Format;
        }
        else
        {
            _element.Format = PlaceHolder;
        }
    }
}