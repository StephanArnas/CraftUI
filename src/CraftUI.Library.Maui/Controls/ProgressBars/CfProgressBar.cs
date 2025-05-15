using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using CraftUI.Library.Maui.Common.Resources;

namespace CraftUI.Library.Maui.Controls.ProgressBars;

// Source: https://github.com/ewerspej/epj.SaProgressBar.Maui

public class CfProgressBar : SKCanvasView
{
    private SKCanvas _canvas = null!;
    private SKRect _drawRect;
    private SKImageInfo _info;

    public static readonly BindableProperty ProgressProperty = BindableProperty.Create(nameof(Progress), typeof(float), typeof(CfProgressBar), 0.0f, propertyChanged: OnBindablePropertyChanged);
    public static readonly BindableProperty ProgressColorProperty = BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CfProgressBar), propertyChanged: OnBindablePropertyChanged);
    public static readonly BindableProperty GradientColorProperty = BindableProperty.Create(nameof(GradientColor), typeof(Color), typeof(CfProgressBar), propertyChanged: OnBindablePropertyChanged);
    public static readonly BindableProperty BaseColorProperty = BindableProperty.Create(nameof(BaseColor), typeof(Color), typeof(CfProgressBar), propertyChanged: OnBindablePropertyChanged);
    public static readonly BindableProperty UseRangeProperty = BindableProperty.Create(nameof(UseRange), typeof(bool), typeof(CfProgressBar), false, propertyChanged: OnBindablePropertyChanged);
    public static readonly BindableProperty LowerRangeValueProperty = BindableProperty.Create(nameof(LowerRangeValue), typeof(float), typeof(CfProgressBar), 0.0f, propertyChanged: OnBindablePropertyChanged);
    public static readonly BindableProperty UpperRangeValueProperty = BindableProperty.Create(nameof(UpperRangeValue), typeof(float), typeof(CfProgressBar), 0.0f, propertyChanged: OnBindablePropertyChanged);
    public static readonly BindableProperty UseGradientProperty = BindableProperty.Create(nameof(UseGradient), typeof(bool), typeof(CfProgressBar), false, propertyChanged: OnBindablePropertyChanged);
    public static readonly BindableProperty RoundCapsProperty = BindableProperty.Create(nameof(RoundCaps), typeof(bool), typeof(CfProgressBar), false, propertyChanged: OnBindablePropertyChanged);

    public float Progress
    {
        get => (float)GetValue(ProgressProperty);
        set => SetValue(ProgressProperty, value);
    }

    public Color ProgressColor
    {
        get => (Color)GetValue(ProgressColorProperty);
        set => SetValue(ProgressColorProperty, value);
    }

    public Color GradientColor
    {
        get => (Color)GetValue(GradientColorProperty);
        set => SetValue(GradientColorProperty, value);
    }

    public Color BaseColor
    {
        get => (Color)GetValue(BaseColorProperty);
        set => SetValue(BaseColorProperty, value);
    }

    public bool UseRange
    {
        get => (bool)GetValue(UseRangeProperty);
        set => SetValue(UseRangeProperty, value);
    }

    public float LowerRangeValue
    {
        get => (float)GetValue(LowerRangeValueProperty);
        set => SetValue(LowerRangeValueProperty, value);
    }

    public float UpperRangeValue
    {
        get => (float)GetValue(UpperRangeValueProperty);
        set => SetValue(UpperRangeValueProperty, value);
    }

    public bool UseGradient
    {
        get => (bool)GetValue(UseGradientProperty);
        set => SetValue(UseGradientProperty, value);
    }

    public bool RoundCaps
    {
        get => (bool)GetValue(RoundCapsProperty);
        set => SetValue(RoundCapsProperty, value);
    }
    
    public CfProgressBar()
    {
        IgnorePixelScaling = false;
        
        SetProgressColor(Application.Current?.RequestedTheme ?? AppTheme.Unspecified);
    }
    
    private void SetProgressColor(AppTheme theme)
    {
        if (Application.Current?.Resources == null)
        {
            return;
        }
        
        if (theme == AppTheme.Dark)
        {
            if (Application.Current.Resources.TryGetValue(ColorResources.Primary400, out var progressDarkColor))
            {
                ProgressColor = (Color)progressDarkColor;
            }
            
            if (Application.Current.Resources.TryGetValue(ColorResources.Primary800, out var baseColorDarkColor))
            {
                BaseColor = (Color)baseColorDarkColor;
            }
        }
        else
        {
            if (Application.Current.Resources.TryGetValue(ColorResources.Primary600, out var progressLightColor))
            {
                ProgressColor = (Color)progressLightColor;
            }
            
            if (Application.Current.Resources.TryGetValue(ColorResources.Primary200, out var baseLightColor))
            {
                BaseColor = (Color)baseLightColor;
            }
        }
    }
    
    private static void OnBindablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((CfProgressBar)bindable).InvalidateSurface();
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        base.OnPaintSurface(e);

        _canvas = e.Surface.Canvas;
        _canvas.Clear();

        _info = e.Info;

        _drawRect = new SKRect(left: 0, top: 0, right: _info.Width, bottom: _info.Height);

        DrawBase();
        DrawProgress();
    }

    private void DrawBase()
    {
        using var basePath = new SKPath();

        if (RoundCaps)
        {
            basePath.AddRoundRect(_drawRect, rx: _drawRect.Height / 2, ry: _drawRect.Height / 2);
        }
        else
        {
            basePath.AddRect(_drawRect);
        }

        _canvas.ClipPath(basePath);
        _canvas.DrawPath(basePath, new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = BaseColor.ToSKColor(),
            IsAntialias = true
        });
    }

    private void DrawProgress()
    {
        using var progressPath = new SKPath();

        var progressRect = UseRange
            ? new SKRect(left: _info.Width * LowerRangeValue, top: 0, right: _info.Width * UpperRangeValue, bottom: _info.Height)
            : new SKRect(left: 0, top: 0, right: _info.Width * Progress, bottom: _info.Height);

        if (RoundCaps)
        {
            progressPath.AddRoundRect(progressRect, rx: progressRect.Height / 2, ry: progressRect.Height / 2);
        }
        else
        {
            progressPath.AddRect(progressRect);
        }

        using var progressPaint = new SKPaint();
        progressPaint.Style = SKPaintStyle.Fill;
        progressPaint.IsAntialias = true;

        if (UseGradient)
        {
            progressPaint.Shader = SKShader.CreateLinearGradient(
                new SKPoint(_drawRect.Left, _drawRect.MidY),
                new SKPoint(_drawRect.Right, _drawRect.MidY),
                colors: [GradientColor.ToSKColor(), ProgressColor.ToSKColor()],
                colorPos: [0.0f, 1.0f],
                SKShaderTileMode.Clamp
            );
        }
        else
        {
            progressPaint.Color = ProgressColor.ToSKColor();
        }

        _canvas.DrawPath(progressPath, progressPaint);
    }
}