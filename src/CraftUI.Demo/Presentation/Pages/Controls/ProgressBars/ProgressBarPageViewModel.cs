using CommunityToolkit.Mvvm.ComponentModel;
using CraftUI.Demo.Presentation.Common;
using Microsoft.Extensions.Logging;

namespace CraftUI.Demo.Presentation.Pages.Controls.ProgressBars;

public partial class ProgressBarPageViewModel : ViewModelBase
{
    private readonly ILogger<ProgressBarPageViewModel> _logger;
    private bool _isAnimating;
    private double _angle;
    private IDispatcherTimer? _timer;
    private readonly Random _random = new();

    [ObservableProperty]
    private float _currentProgress;

    [ObservableProperty]
    private float _basicProgress;

    [ObservableProperty]
    private float _wavyProgress;

    [ObservableProperty]
    private float _pulsatingProgress;

    [ObservableProperty]
    private float _stepProgress;


    public ProgressBarPageViewModel(
        ILogger<ProgressBarPageViewModel> logger)
    {
        _logger = logger;
        
        _logger.LogInformation("Building ProgressBarPageViewModel");
    }
    
    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _logger.LogInformation("ApplyQueryAttributes( query: {Query} )", query);
        
        base.ApplyQueryAttributes(query);
    }

    public override void OnAppearing()
    {
        _logger.LogInformation("OnAppearing()");

        InitializeProgressValues();
        StartAnimation();
        
        base.OnAppearing();
    }

    public override void OnDisappearing()
    {
        _logger.LogInformation("OnDisappearing()");

        StopAnimation();
        
        base.OnDisappearing();
    }
    
    private void InitializeProgressValues()
    {
        CurrentProgress = 0.0f;
        BasicProgress = 0.0f;
        WavyProgress = 0.5f;
        PulsatingProgress = 0.75f;
        StepProgress = 0.0f;
        _angle = 0;
    }

    private void StartAnimation()
    {
        if (_isAnimating)
        {
            return;
        }

        _isAnimating = true;
        _timer = Dispatcher.GetForCurrentThread()!.CreateTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(16); // ~60fps
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }

    private void StopAnimation()
    {
        if (!_isAnimating)
        {
            return;
        }

        if (_timer is not null)
        {
            _timer.Stop();
            _timer = null;
        }
        _isAnimating = false;
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        // Linear progress animation
        CurrentProgress += 0.005f;
        if (CurrentProgress > 1.0f)
        {
            CurrentProgress = 0.0f;
        }

        // Smooth basic progress animation
        BasicProgress += 0.003f;
        if (BasicProgress > 1.0f)
        {
            BasicProgress = 0.0f;
        }

        // Sine wave animation
        _angle += 0.02;
        WavyProgress = 0.5f + 0.5f * (float)Math.Sin(_angle);

        // Pulsating animation (breathe effect)
        PulsatingProgress = 0.5f + 0.25f * (float)Math.Sin(_angle * 0.5);

        // Step progress animation
        if (_random.NextDouble() < 0.01) // Randomly increment
        {
            StepProgress = Math.Min(1.0f, StepProgress + _random.Next(1, 5) * 0.05f);
        }
        
        if (StepProgress >= 1.0f && _random.NextDouble() < 0.05) // Reset if full
        {
            StepProgress = 0.0f;
        }
    }
}