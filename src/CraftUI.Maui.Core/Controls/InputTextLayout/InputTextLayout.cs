using System.Windows.Input;
using CraftUI.Maui.Core.Common.Helpers;
using Microsoft.Maui.Controls.Shapes;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace CraftUI.Maui.Core.Controls.InputTextLayout;

public class InputTextLayout : Grid
{
    public static readonly BindableProperty ViewProperty = BindableProperty.Create(
        nameof(View),
        typeof(View),
        typeof(InputTextLayout),
        defaultValue: null,
        BindingMode.OneWay,
        ViewHelper.ValidateCustomView,
        ElementChanged);

    public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(
        nameof(IsRequired),
        typeof(bool),
        typeof(InputTextLayout),
        defaultValue: false,
        propertyChanged: IsRequiredChanged);

    public static readonly BindableProperty LabelProperty = BindableProperty.Create(
        nameof(Label),
        typeof(string),
        typeof(InputTextLayout),
        propertyChanged: LabelChanged);

    public static readonly BindableProperty InfoProperty = BindableProperty.Create(
        nameof(Info),
        typeof(string),
        typeof(InputTextLayout),
        propertyChanged: InfoChanged);

    public static readonly BindableProperty ErrorProperty = BindableProperty.Create(
        nameof(Error),
        typeof(string),
        typeof(InputTextLayout),
        propertyChanged: ErrorChanged);

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
        nameof(IsLoading),
        typeof(bool),
        typeof(InputTextLayout),
        defaultValue: false,
        propertyChanged: IsLoadingChanged);

    public static readonly BindableProperty ActionIconSourceProperty = BindableProperty.Create(
        nameof(ActionIconSource),
        typeof(ImageSource),
        typeof(InputTextLayout),
        defaultValue: null,
        propertyChanged: ActionIconSourceChanged);

    public static readonly BindableProperty ActionIconCommandProperty = BindableProperty.Create(
        nameof(ActionIconCommand),
        typeof(ICommand),
        typeof(InputTextLayout),
        defaultValue: null);

    private readonly Label _requiredLabel;
    private readonly Label _labelLabel;
    private readonly SKCanvasView _borderCanvasView;
    private readonly Border _borderLabel;
    private readonly Label _infoLabel;
    private readonly Label _errorLabel;
    private readonly ActivityIndicator _loaderActivityIndicator;
    private readonly Image _actionIconButton;

    public View View
    {
        get => (View)GetValue(ViewProperty);
        set => SetValue(ViewProperty, value);
    }

    public bool IsRequired
    {
        get => (bool)GetValue(IsRequiredProperty);
        set => SetValue(IsRequiredProperty, value);
    }

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public string Info
    {
        get => (string)GetValue(InfoProperty);
        set => SetValue(InfoProperty, value);
    }

    public string Error
    {
        get => (string)GetValue(ErrorProperty);
        set => SetValue(ErrorProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public ImageSource? ActionIconSource
    {
        get => (ImageSource?)GetValue(ActionIconSourceProperty);
        set => SetValue(ActionIconSourceProperty, value);
    }

    public ICommand? ActionIconCommand
    {
        get => (ICommand?)GetValue(ActionIconCommandProperty);
        set => SetValue(ActionIconCommandProperty, value);
    }

    protected InputTextLayout()
    {
        RowDefinitions =
        [
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto },
            new RowDefinition { Height = GridLength.Auto }
        ];
        Margin = new Thickness(0, 10, 0, 0);

        // Required Label
        _requiredLabel = new Label
        {
            Text = "*",
            Padding = new Thickness(4, 0),
            Margin = new Thickness(4, 0, -4, 0),
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start
        };

        _requiredLabel.SetAppThemeColor(Microsoft.Maui.Controls.Label.TextColorProperty,
            ResourceHelper.GetResource<Color>("Danger"),
            ResourceHelper.GetResource<Color>("Danger"));

        // Label Label
        _labelLabel = new Label
        {
            Padding = new Thickness(4, 0),
            Margin = new Thickness(0, 4, 0, 4),
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start
        };
        _labelLabel.SetAppThemeColor(Microsoft.Maui.Controls.Label.TextColorProperty,
            ResourceHelper.GetResource<Color>("Gray900"),
            ResourceHelper.GetResource<Color>("Gray100"));

        var labelStackLayout = new HorizontalStackLayout
        {
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(12, -13, 0, 0),
            Children = { _requiredLabel, _labelLabel }
        };

        // Border Canvas View
        _borderCanvasView = new SKCanvasView();
        _borderCanvasView.PaintSurface += OnCanvasViewPaintSurface;

        // Border Label
        _borderLabel = new Border
        {
            Padding = new Thickness(8, 4),
            Stroke = Colors.Transparent,
            StrokeShape = new RoundRectangle { CornerRadius = 4 },
            StrokeThickness = 0
        };

        // Info Label
        _infoLabel = new Label
        {
            IsVisible = false,
            Padding = new Thickness(4, 0),
            Margin = new Thickness(12, 4, 0, 4),
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start
        };
        _infoLabel.SetAppThemeColor(Microsoft.Maui.Controls.Label.TextColorProperty,
            ResourceHelper.GetResource<Color>("Gray500"),
            ResourceHelper.GetResource<Color>("Gray500"));
        SetRow((BindableObject)_infoLabel, 1);

        // Error Label
        _errorLabel = new Label
        {
            IsVisible = false,
            Padding = new Thickness(4, 0),
            Margin = new Thickness(12, 4, 0, 4),
            FontSize = 14,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start
        };
        _errorLabel.SetAppThemeColor(Microsoft.Maui.Controls.Label.TextColorProperty,
            ResourceHelper.GetResource<Color>("Danger"),
            ResourceHelper.GetResource<Color>("Danger"));
        SetRow((BindableObject)_errorLabel, 2);

        // Loader Activity Indicator
        _loaderActivityIndicator = new ActivityIndicator
        {
            IsVisible = false,
            IsRunning = false,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 24,
            HeightRequest = 24,
            Margin = new Thickness(0, 0, 8, 0)
        };
        _loaderActivityIndicator.SetAppThemeColor(ActivityIndicator.ColorProperty,
            ResourceHelper.GetResource<Color>("Primary600"),
            ResourceHelper.GetResource<Color>("Primary400"));

        // Action Icon Button
        _actionIconButton = new Image
        {
            IsVisible = false,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = 24,
            HeightRequest = 24,
            Margin = new Thickness(0, 0, 8, 0)
        };
        _actionIconButton.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(OnActionIconTapped)
        });

        Children.Add(labelStackLayout);
        Children.Add(_borderCanvasView);
        Children.Add(_borderLabel);
        Children.Add(_infoLabel);
        Children.Add(_errorLabel);
        Children.Add(_loaderActivityIndicator);
        Children.Add(_actionIconButton);
    }

    private static void ElementChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((InputTextLayout)bindable).UpdateElementView();

    private static void IsRequiredChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((InputTextLayout)bindable).UpdateIsRequiredView();

    private static void LabelChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((InputTextLayout)bindable).UpdateLabelView();

    private static void InfoChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((InputTextLayout)bindable).UpdateInfoView();

    private static void ErrorChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((InputTextLayout)bindable).UpdateErrorView();

    private static void IsLoadingChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((InputTextLayout)bindable).UpdateIsLoadingView();

    private static void ActionIconSourceChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((InputTextLayout)bindable).UpdateActionIconSourceView();

    private void UpdateElementView()
    {
        _borderLabel.Content = View;
        UpdateIsRequiredView();
    }

    private void UpdateIsRequiredView()
    {
        _requiredLabel.IsVisible = IsRequired;
    }

    private void UpdateLabelView()
    {
        _labelLabel.Text = Label;
        _labelLabel.IsVisible = !string.IsNullOrEmpty(Label);
    }

    private void UpdateInfoView()
    {
        _infoLabel.Text = Info;
        _infoLabel.IsVisible = !string.IsNullOrEmpty(Info);
    }

    private void UpdateErrorView()
    {
        _errorLabel.Text = Error;
        _errorLabel.IsVisible = !string.IsNullOrEmpty(Error);
        InvalidateSurfaceForCanvasView();
    }

    private void UpdateIsLoadingView()
    {
        _loaderActivityIndicator.IsVisible = IsLoading;
        _loaderActivityIndicator.IsRunning = IsLoading;

        if (ActionIconSource is not null)
        {
            _actionIconButton.IsVisible = !IsLoading;
        }
    }

    private void UpdateActionIconSourceView()
    {
        _actionIconButton.IsVisible = ActionIconSource is not null;
        _actionIconButton.Source = ActionIconSource;
    }

    private void OnActionIconTapped()
    {
        if (ActionIconCommand?.CanExecute(null) == true)
        {
            ActionIconCommand.Execute(null);
        }
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        InvalidateSurfaceForCanvasView();
    }

    protected void PlatformSizeChangedCanvasView()
    {
        _borderCanvasView.InvalidateMeasure();
        _borderCanvasView.PlatformSizeChanged();
    }

    public void InvalidateSurfaceForCanvasView()
    {
        _borderCanvasView.InvalidateSurface();
    }

    private void OnCanvasViewPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear();

        var paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 3,
            IsAntialias = true
        };

        paint.Color = !string.IsNullOrEmpty(Error)
            ? ResourceHelper.GetResource<Color>("Danger").ToSKColor()
            : ResourceHelper.GetThemeColor("Gray900", "Gray100").ToSKColor();

        const float radius = 20f;
        const float labelExtraSpace = 8;
        float borderThickness = paint.StrokeWidth / 2;

        var rect = new SKRect(
            borderThickness,
            borderThickness,
            e.Info.Width - borderThickness,
            e.Info.Height - borderThickness
        );

        float labelWidth = (float)_labelLabel.Width * e.Info.Width / (float)_borderCanvasView.Width;
        float isRequiredWidth = _requiredLabel.IsVisible
            ? (float)_requiredLabel.Width * e.Info.Width / (float)_borderCanvasView.Width
            : 0;
        float topLineRightSegmentStartX =
            rect.Left + radius + labelExtraSpace + isRequiredWidth + labelWidth + labelExtraSpace;

        canvas.DrawArc(new SKRect(rect.Left, rect.Top, rect.Left + 2 * radius, rect.Top + 2 * radius), startAngle: 180,
            sweepAngle: 90, useCenter: false, paint);
        canvas.DrawArc(new SKRect(rect.Right - 2 * radius, rect.Top, rect.Right, rect.Top + 2 * radius),
            startAngle: 270, sweepAngle: 90, useCenter: false, paint);
        canvas.DrawArc(new SKRect(rect.Right - 2 * radius, rect.Bottom - 2 * radius, rect.Right, rect.Bottom),
            startAngle: 0, sweepAngle: 90, useCenter: false, paint);
        canvas.DrawArc(new SKRect(rect.Left, rect.Bottom - 2 * radius, rect.Left + 2 * radius, rect.Bottom),
            startAngle: 90, sweepAngle: 90, useCenter: false, paint);
        canvas.DrawLine(rect.Left + radius, rect.Top, rect.Left + radius + labelExtraSpace, rect.Top, paint);
        canvas.DrawLine(topLineRightSegmentStartX, rect.Top, rect.Right - radius, rect.Top, paint);
        canvas.DrawLine(rect.Right, rect.Top + radius, rect.Right, rect.Bottom - radius, paint);
        canvas.DrawLine(rect.Left + radius, rect.Bottom, rect.Right - radius, rect.Bottom, paint);
        canvas.DrawLine(rect.Left, rect.Top + radius, rect.Left, rect.Bottom - radius, paint);
    }
}