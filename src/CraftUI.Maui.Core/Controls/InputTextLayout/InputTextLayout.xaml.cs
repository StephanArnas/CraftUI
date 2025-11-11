using System.Windows.Input;
using CraftUI.Maui.Common.Helpers;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace CraftUI.Maui.Controls.InputTextLayout;

public partial class InputTextLayout
{
    public static readonly BindableProperty ViewProperty = BindableProperty.Create(nameof(View), typeof(View), typeof(InputTextLayout), defaultValue: null, BindingMode.OneWay, ViewHelper.ValidateCustomView, ElementChanged);
    public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(InputTextLayout), defaultValue: false, propertyChanged: IsRequiredChanged);
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(InputTextLayout), propertyChanged: LabelChanged);
    public static readonly BindableProperty InfoProperty = BindableProperty.Create(nameof(Info), typeof(string), typeof(InputTextLayout), propertyChanged: InfoChanged);
    public static readonly BindableProperty ErrorProperty = BindableProperty.Create(nameof(Error), typeof(string), typeof(InputTextLayout), propertyChanged: ErrorChanged);
    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(InputTextLayout), defaultValue: false, propertyChanged: IsLoadingChanged);
    public static readonly BindableProperty ActionIconSourceProperty = BindableProperty.Create(nameof(ActionIconSource), typeof(ImageSource), typeof(InputTextLayout), defaultValue: null, propertyChanged: ActionIconSourceChanged);
    public static readonly BindableProperty ActionIconCommandProperty = BindableProperty.Create(nameof(ActionIconCommand), typeof(ICommand), typeof(InputTextLayout), defaultValue: null);

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

    public InputTextLayout()
    {
        InitializeComponent();
    }

    private static void ElementChanged(BindableObject bindable, object oldValue, object newValue) => ((InputTextLayout)bindable).UpdateElementView();
    private static void IsRequiredChanged(BindableObject bindable, object oldValue, object newValue) => ((InputTextLayout)bindable).UpdateIsRequiredView();
    private static void LabelChanged(BindableObject bindable, object oldValue, object newValue) => ((InputTextLayout)bindable).UpdateLabelView();
    private static void InfoChanged(BindableObject bindable, object oldValue, object newValue) => ((InputTextLayout)bindable).UpdateInfoView();
    private static void ErrorChanged(BindableObject bindable, object oldValue, object newValue) => ((InputTextLayout)bindable).UpdateErrorView();
    private static void IsLoadingChanged(BindableObject bindable, object oldValue, object newValue) => ((InputTextLayout)bindable).UpdateIsLoadingView();
    private static void ActionIconSourceChanged(BindableObject bindable, object oldValue, object newValue) => ((InputTextLayout)bindable).UpdateActionIconSourceView();
    
    private void UpdateElementView()
    {
        BorderLabel.Content = View;
        UpdateIsRequiredView();
    }

    private void UpdateIsRequiredView()
    {
        RequiredLabel.IsVisible = IsRequired;
    }

    private void UpdateLabelView()
    {
        LabelLabel.Text = Label;
        LabelLabel.IsVisible = !string.IsNullOrEmpty(Label);
    }
    
    private void UpdateInfoView()
    {
        InfoLabel.Text = Info;
        InfoLabel.IsVisible = !string.IsNullOrEmpty(Info);
    }

    private void UpdateErrorView()
    {
        ErrorLabel.Text = Error;
        ErrorLabel.IsVisible = !string.IsNullOrEmpty(Error);
        InvalidateSurfaceForCanvasView();
    }

    private void UpdateIsLoadingView()
    {
        LoaderActivityIndicator.IsVisible = IsLoading;
        LoaderActivityIndicator.IsRunning = IsLoading;
        
        if (ActionIconSource is not null)
        {
            ActionIconButton.IsVisible = !IsLoading;
        }
    }

    private void UpdateActionIconSourceView()
    {
        ActionIconButton.IsVisible = ActionIconSource is not null;
        ActionIconButton.Source = ActionIconSource;
    }
    
    private void OnActionIconTapped(object sender, EventArgs e)
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
        BorderCanvasView.InvalidateMeasure();
        BorderCanvasView.PlatformSizeChanged();
    }

    protected void InvalidateSurfaceForCanvasView()
    {
        BorderCanvasView.InvalidateSurface();
    }
    
    private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        // System.Diagnostics.Debug.WriteLine($"SKCanvasView: {BorderCanvasView.Width}x{BorderCanvasView.Height}, SKInfo: {e.Info.Width}x{e.Info.Height}");

        var canvas = e.Surface.Canvas;
        canvas.Clear(); // Clear the canvas

        var paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 3,
            IsAntialias = true // Smooth edges
        };
        
        paint.Color = !string.IsNullOrEmpty(Error) 
            ? ResourceHelper.GetResource<Color>("Danger").ToSKColor() 
            : ResourceHelper.GetThemeColor("Gray900", "Gray100").ToSKColor();

        const float radius = 20f; // Corner radius
        const float labelExtraSpace = 8; // Fixed length for the segment
        float borderThickness = paint.StrokeWidth / 2;

        // Define the full rectangle based on the canvas size
        var rect = new SKRect(
            borderThickness,
            borderThickness,
            e.Info.Width - borderThickness,
            e.Info.Height - borderThickness
        );

        // Measure the Label's width
        float labelWidth = (float)LabelLabel.Width * e.Info.Width / (float)BorderCanvasView.Width;

        // Measure the RequiredLabel's width
        float isRequiredWidth = RequiredLabel.IsVisible ? (float)RequiredLabel.Width * e.Info.Width / (float)BorderCanvasView.Width : 0;

        // Calculate endX correctly
        float topLineRightSegmentStartX = 
            rect.Left + radius + labelExtraSpace + isRequiredWidth + labelWidth + labelExtraSpace;

        // Draw the top-left arc
        canvas.DrawArc(new SKRect(rect.Left, rect.Top, rect.Left + 2 * radius, rect.Top + 2 * radius), startAngle: 180, sweepAngle: 90, useCenter: false, paint);

        // Draw the top-right arc
        canvas.DrawArc(new SKRect(rect.Right - 2 * radius, rect.Top, rect.Right, rect.Top + 2 * radius), startAngle: 270, sweepAngle: 90, useCenter: false, paint);

        // Draw the bottom-right arc
        canvas.DrawArc(new SKRect(rect.Right - 2 * radius, rect.Bottom - 2 * radius, rect.Right, rect.Bottom), startAngle: 0, sweepAngle: 90, useCenter: false, paint);

        // Draw the bottom-left arc
        canvas.DrawArc(new SKRect(rect.Left, rect.Bottom - 2 * radius, rect.Left + 2 * radius, rect.Bottom), startAngle: 90, sweepAngle: 90, useCenter: false, paint);

        // Draw the left segment of the top line with a fixed length of 10 units
        canvas.DrawLine(rect.Left + radius, rect.Top, rect.Left + radius + labelExtraSpace, rect.Top, paint);

        // Draw the right segment of the top line from the end of the label to the top-right arc
        canvas.DrawLine(topLineRightSegmentStartX, rect.Top, rect.Right - radius, rect.Top, paint);

        // Draw the right line between the top-right and bottom-right arcs
        canvas.DrawLine(rect.Right, rect.Top + radius, rect.Right, rect.Bottom - radius, paint);

        // Draw the bottom line between the bottom-right and bottom-left arcs
        canvas.DrawLine(rect.Left + radius, rect.Bottom, rect.Right - radius, rect.Bottom, paint);

        // Draw the left line between the bottom-left and top-left arcs
        canvas.DrawLine(rect.Left, rect.Top + radius, rect.Left, rect.Bottom - radius, paint);
    }
}