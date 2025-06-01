namespace CraftUI.Library.Maui.Controls;

public partial class CfDatePicker
{
    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(CfDatePicker), defaultValue: "/ . / . /");
    public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(CfDatePicker), defaultValue: null, defaultBindingMode: BindingMode.TwoWay);
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(nameof(Format), typeof(string), typeof(CfDatePicker), defaultValue: "d", propertyChanged: OnFormatChanged);
    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(CfDatePicker), propertyChanged: OnMinimumDateChanged);
    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(CfDatePicker), propertyChanged: OnMaximumDateChanged);
    public static readonly BindableProperty ShowClearButtonProperty = BindableProperty.Create(nameof(ShowClearButton), typeof(bool), typeof(CfDatePicker), defaultValue: true, propertyChanged: OnShowClearButtonChanged);
    
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
        InitializeComponent();
        Element.DateSelected += OnDateSelected;
        
        var tapped = new TapGestureRecognizer();
        tapped.Tapped += (_, _) =>
        {
            Element.Date = DateTime.Today;
            NullableDate = null;
        };
        CloseImage.GestureRecognizers.Add(tapped);
    }
    
    private static void OnFormatChanged(BindableObject bindable, object oldValue, object newValue) => ((CfDatePicker)bindable).OnFormatChanged();
    private static void OnMinimumDateChanged(BindableObject bindable, object oldValue, object newValue) => ((CfDatePicker)bindable).OnMinimumDateChanged();
    private static void OnMaximumDateChanged(BindableObject bindable, object oldValue, object newValue) => ((CfDatePicker)bindable).OnMaximumDateChanged();
    private static void OnShowClearButtonChanged(BindableObject bindable, object oldValue, object newValue) => ((CfDatePicker)bindable).UpdateClearButtonVisibility();
    
    private void OnDateSelected(object? sender, DateChangedEventArgs e)
    {
        NullableDate = e.NewDate;
    }
    
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        
        if (BindingContext != null)
        {
            Element.Format = PlaceHolder;
        }
    }
    
    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == NullableDateProperty.PropertyName)
        {
            if (NullableDate.HasValue)
            {
                Element.Date = NullableDate.Value;
            }
            
            UpdateDateView();
        }
    }

    public void OnFormatChanged()
    {
        Element.Format = Format;
        UpdateDateView();
    }

    public void OnMinimumDateChanged()
    {
        Element.MinimumDate = MinimumDate;
    }

    public void OnMaximumDateChanged()
    {
        Element.MaximumDate = MaximumDate;
    }
    
    private void UpdateClearButtonVisibility()
    {
        CloseImage.IsVisible = ShowClearButton;
    }
    
    private void UpdateDateView()
    {
        if (NullableDate.HasValue)
        {
            Element.Date = NullableDate.Value;
            Element.Format = Format;
        }
        else
        {
            Element.Format = PlaceHolder;
        }
    }
}
