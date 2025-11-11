using System.Windows.Input;
using CraftUI.Maui.Core.Controls.ProgressBars;

namespace CraftUI.Maui.Core.Controls.Button;

public class CfButton : Grid
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

    public string Text
    {
        get => (string)GetValue(TextProperty);
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

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public CfButton()
    {
        _button = new Microsoft.Maui.Controls.Button();
        _button.Clicked += Button_OnClicked;

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
    }

    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == IsEnabledProperty.PropertyName)
        {
            _button.IsEnabled = IsEnabled;
        }
        else if (propertyName == BackgroundColorProperty.PropertyName)
        {
            _button.BackgroundColor = BackgroundColor;
        }
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue) => ((CfButton)bindable)._button.Text = (string)newValue;

    private static void OnTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var button = (CfButton)bindable;
        var color = (Color)newValue;
        button._animatedProgressBar.ProgressColor = color;
        button._button.TextColor = color;
    }

    private static void IsLoadingChanged(BindableObject bindable, object oldValue, object newValue) => ((CfButton)bindable).UpdateIsLoadingView();

    private void UpdateIsLoadingView()
    {
        _button.IsEnabled = !IsLoading;
        _animatedProgressBar.IsVisible = IsLoading;

        if (IsLoading)
        {
            _lowerAnimation.Commit(owner: this, name: LowerKey, length: 1000, easing: Easing.CubicInOut, repeat: () => true);
            _upperAnimation.Commit(owner: this, name: UpperKey, length: 1000, easing: Easing.CubicInOut, repeat: () => true);
        }
        else
        {
            this.AbortAnimation(handle: LowerKey);
            this.AbortAnimation(handle: UpperKey);
        }
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        if (Command != null && Command.CanExecute(CommandParameter))
        {
            Command.Execute(CommandParameter);
        }
    }
}
