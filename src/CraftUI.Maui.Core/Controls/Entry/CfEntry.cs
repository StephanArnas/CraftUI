using System.Windows.Input;

namespace CraftUI.Maui.Core.Controls.Entry;

public class CfEntry : InputTextLayout.InputTextLayout
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text), 
        typeof(string),
        typeof(CfEntry),
        propertyChanged: TextChanged, 
        defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(Placeholder),
        typeof(string), 
        typeof(CfEntry), 
        propertyChanged: PlaceholderChanged, 
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
        nameof(Keyboard),
        typeof(Keyboard), 
        typeof(CfEntry), 
        defaultValue: Keyboard.Plain, 
        propertyChanged: KeyboardChanged,
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(
        nameof(ReturnType),
        typeof(ReturnType),
        typeof(CfEntry), defaultValue: ReturnType.Done, 
        propertyChanged: ReturnTypeChanged,
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(
        nameof(ReturnCommand),
        typeof(ICommand), 
        typeof(CfEntry), 
        defaultValue: null, 
        propertyChanged: ReturnCommandChanged,
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(
        nameof(TextTransform),
        typeof(TextTransform), 
        typeof(CfEntry), 
        defaultValue: TextTransform.Default,
        propertyChanged: TextTransformChanged, 
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty IsReadOnlyProperty = BindableProperty.Create(
        nameof(IsReadOnly),
        typeof(bool), 
        typeof(CfEntry), 
        defaultValue: false, 
        propertyChanged: IsReadOnlyChanged);

    private readonly Microsoft.Maui.Controls.Entry _element;

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    public ReturnType ReturnType
    {
        get => (ReturnType)GetValue(ReturnTypeProperty);
        set => SetValue(ReturnTypeProperty, value);
    }

    public ICommand ReturnCommand
    {
        get => (ICommand)GetValue(ReturnCommandProperty);
        set => SetValue(ReturnCommandProperty, value);
    }

    public TextTransform TextTransform
    {
        get => (TextTransform)GetValue(TextTransformProperty);
        set => SetValue(TextTransformProperty, value);
    }

    public new bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    public CfEntry()
    {
        _element = new Microsoft.Maui.Controls.Entry
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Center
        };

        // Do NOT force a fixed height; let the inner Entry drive the size
        VerticalOptions = LayoutOptions.Fill;

        // Bind to this control instance
        _element.BindingContext = this;
        _element.SetBinding(Microsoft.Maui.Controls.Entry.TextProperty,
            BindingBase.Create<CfEntry, string>(static c => c.Text, source: this, mode: BindingMode.TwoWay));

        // Track inner Entry size to mirror height
        SizeChanged += OnContainerSizeChanged;

        // Attach the inner view to the layout so it participates in measure/arrange
        View = _element;
    }

    private void OnContainerSizeChanged(object? sender, EventArgs e)
    {
        // Re-measure the Entry with the current width to get an accurate height
        var widthConstraint = Width > 0 ? Width : double.PositiveInfinity;
        var measured = _element.Measure(widthConstraint, double.PositiveInfinity);
        var desired = measured.Height;
        if (desired > 0)
        {
            HeightRequest = desired + Padding.VerticalThickness;
        }
    }

    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        switch (propertyName)
        {
            case nameof(IsEnabled):
                _element.IsEnabled = IsEnabled;
                break;
            case nameof(IsVisible):
                _element.IsVisible = IsVisible;
                break;
        }
    }

    private static void TextChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfEntry)bindable).UpdateTextView();

    private static void PlaceholderChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfEntry)bindable).UpdatePlaceholderView();

    private static void KeyboardChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfEntry)bindable).UpdateKeyboardView();

    private static void ReturnTypeChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfEntry)bindable).UpdateReturnTypeView();

    private static void ReturnCommandChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfEntry)bindable).UpdateReturnCommandView();

    private static void TextTransformChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfEntry)bindable).UpdateTextTransformView();

    private static void IsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfEntry)bindable).UpdateIsReadOnlyView();

    private void UpdateTextView()
    {
        if (Keyboard == Keyboard.Numeric)
        {
            if (string.IsNullOrEmpty(Text))
            {
                _element.Text = Text;
                return;
            }

            _element.Text = int.TryParse(Text, out var number)
                ? number.ToString()
                : Text[..^1];
        }
        else
        {
            _element.Text = Text;
        }
    }

    private void UpdatePlaceholderView() => _element.Placeholder = Placeholder;
    private void UpdateKeyboardView() => _element.Keyboard = Keyboard;
    private void UpdateReturnTypeView() => _element.ReturnType = ReturnType;
    private void UpdateReturnCommandView() => _element.ReturnCommand = ReturnCommand;
    private void UpdateTextTransformView() => _element.TextTransform = TextTransform;
    private void UpdateIsReadOnlyView() => _element.IsReadOnly = IsReadOnly;
}