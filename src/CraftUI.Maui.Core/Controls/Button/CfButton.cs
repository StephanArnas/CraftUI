using System.Windows.Input;
using CraftUI.Maui.Core.Common.Resources;
using CraftUI.Maui.Core.Controls.Button.Enums;
using CraftUI.Maui.Core.Controls.ProgressBars;

namespace CraftUI.Maui.Core.Controls.Button;

public partial class CfButton : Grid
{
    private const string LowerKey = "lower";
    private const string UpperKey = "upper";

    private readonly Animation _lowerAnimation;
    private readonly Animation _upperAnimation;
    private readonly Microsoft.Maui.Controls.Button _button;
    private readonly CfProgressBar _animatedProgressBar;

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(CfButton),
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: OnTextChanged);

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
        nameof(IsLoading),
        typeof(bool),
        typeof(CfButton),
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: IsLoadingChanged);

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(CfButton));

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(CfButton));

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(CfButton),
        propertyChanged: OnTextColorChanged);

    public static readonly BindableProperty ButtonStyleProperty = BindableProperty.Create(
        nameof(ButtonStyle),
        typeof(ButtonStyle),
        typeof(CfButton),
        defaultValue: ButtonStyle.Plain,
        propertyChanged: OnButtonStyleChanged);

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius),
        typeof(int), 
        typeof(CfButton), 
        defaultValue: -1);


    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public Color? TextColor
    {
        get => (Color?)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public ButtonStyle ButtonStyle
    {
        get => (ButtonStyle)GetValue(ButtonStyleProperty);
        set => SetValue(ButtonStyleProperty, value);
    }

    public int CornerRadius
    {
        get { return (int)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }


    public CfButton()
    {
        _button = new Microsoft.Maui.Controls.Button();
        _button.SetBinding(Microsoft.Maui.Controls.Button.CommandProperty, new Binding(nameof(Command), source: this));
        _button.SetBinding(Microsoft.Maui.Controls.Button.CommandParameterProperty, new Binding(nameof(CommandParameter), source: this));
        _button.SetBinding(Microsoft.Maui.Controls.Button.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));

        _animatedProgressBar = new CfProgressBar
        {
            IsVisible = false,
            Margin = new Thickness(6, 0),
            HeightRequest = 5,
            UseRange = true,
            RoundCaps = true,
            VerticalOptions = LayoutOptions.End
        };

        Children.Add(_button);
        Children.Add(_animatedProgressBar);

        _lowerAnimation = new Animation(v => _animatedProgressBar.LowerRangeValue = (float)v, start: -0.4, end: 1.0);
        _upperAnimation = new Animation(v => _animatedProgressBar.UpperRangeValue = (float)v, start: 0.0, end: 1.4);

        UpdateButtonStyle();
    }

    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == IsEnabledProperty.PropertyName)
        {
            _button.IsEnabled = IsEnabled;
            UpdateButtonStyle();
        }
        else if (propertyName == BackgroundColorProperty.PropertyName)
        {
            _button.BackgroundColor = BackgroundColor;
        }
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfButton)bindable).UpdateText();

    private static void OnTextColorChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfButton)bindable).UpdateTextColor();

    private static void IsLoadingChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfButton)bindable).UpdateIsLoading();

    private static void OnButtonStyleChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((CfButton)bindable).UpdateButtonStyle();

    private void UpdateText() => _button.Text = Text ?? string.Empty;

    private void UpdateTextColor()
    {
        if (TextColor != null)
        {
            _button.TextColor = TextColor;
            _animatedProgressBar.ProgressColor = TextColor;
        }
    }

    private void UpdateButtonStyle()
    {
        if (!IsEnabled)
        {
            _button.SetAppThemeColor(
                Microsoft.Maui.Controls.Button.TextColorProperty,
                Application.Current?.Resources["Gray900"] as Color ?? Color.FromArgb("#424242"),
                Application.Current?.Resources["Gray200"] as Color ?? Color.FromArgb("#E5E5E5"));

            _button.SetAppThemeColor(
                Microsoft.Maui.Controls.Button.BackgroundColorProperty,
                Application.Current?.Resources["Gray400"] as Color ?? Color.FromArgb("#BDBDBD"),
                Application.Current?.Resources["Gray600"] as Color ?? Color.FromArgb("#757575"));

            _button.BorderColor = Colors.Transparent;
            _button.BorderWidth = 0;
            
            return;
        }

        switch (ButtonStyle)
        {
            case ButtonStyle.Plain:
                ApplyPlainStyle();
                break;
            case ButtonStyle.Outlined:
                ApplyOutlinedStyle();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateIsLoading()
    {
        _animatedProgressBar.IsVisible = IsLoading;

        if (IsLoading)
        {
            _lowerAnimation.Commit(owner: this, name: LowerKey, length: 1000, easing: Easing.CubicInOut, repeat: () => true);
            _upperAnimation.Commit(owner: this, name: UpperKey, length: 1000, easing: Easing.CubicInOut, repeat: () => true);
            _button.BackgroundColor = Application.Current?.Resources[ColorResources.Primary200] as Color;
            _button.IsEnabled = false;
        }
        else
        {
            this.AbortAnimation(handle: LowerKey);
            this.AbortAnimation(handle: UpperKey);
            _button.BackgroundColor = Application.Current?.Resources[ColorResources.Primary] as Color;
            _button.IsEnabled = true;
        }
    }
}