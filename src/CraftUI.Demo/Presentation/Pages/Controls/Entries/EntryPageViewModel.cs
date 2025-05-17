using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CraftUI.Demo.Application.Common.Interfaces.Infrastructure;
using CraftUI.Demo.Presentation.Common;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Sharpnado.TaskLoaderView;

namespace CraftUI.Demo.Presentation.Pages.Controls.Entries;

public partial class EntryPageViewModel : ViewModelBase
{
    private readonly ILogger<EntryPageViewModel> _logger;
    private readonly IToastService _toastService;    
    private readonly IDisplayService _displayService;    
    
    [ObservableProperty]
    private string? _fullName;
    
    [ObservableProperty]
    private string? _email;
    
    [ObservableProperty]
    private string? _website;

    [ObservableProperty]
    private ValidationResult? _validationResult;
    
    public TaskLoaderNotifier<string> WebsiteLoader { get; }
    
    public EntryPageViewModel(
        ILogger<EntryPageViewModel> logger,
        IToastService toastService,
        IDisplayService displayService)    
    {
        _logger = logger;
        _toastService = toastService;
        _displayService = displayService;

        WebsiteLoader = new TaskLoaderNotifier<string>();

        _logger.LogInformation("Building EntryPageViewModel");
    }
    
    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _logger.LogInformation("ApplyQueryAttributes( query: {Query} )", query);
    }

    public override void OnAppearing()
    {
        _logger.LogInformation("OnAppearing()");

        if (WebsiteLoader.IsNotStarted)
        {
            WebsiteLoader.Load(_ => LoadWebsiteAsync());
        }
        
        base.OnAppearing();
    }

    public override void OnDisappearing()
    {
        _logger.LogInformation("OnDisappearing()");
        
        base.OnDisappearing();
    }
    
    private async Task<string> LoadWebsiteAsync()
    {
        _logger.LogInformation("LoadAsync()");

        await Task.Delay(4000);

        return "https://www.stephanarnas.com/";
    }
    
    [RelayCommand]
    private async Task Save()
    {
        _logger.LogInformation("Save()");
        
        var validator = new EntryPageViewModelValidator();
        ValidationResult = await validator.ValidateAsync(this);
        if (!ValidationResult.IsValid)
        {
            return;
        }
        
        await _toastService.ShowAsync("Saved !!");
    }
    
    [RelayCommand]
    private async Task FullnameInfo()
    {
        _logger.LogInformation("FullnameInfo()");

        await _displayService.ShowPopupAsync("Full name", "Your first name and last name are used for communication purpose.");
    }
    
    [RelayCommand]
    private async Task OpenWebsite()
    {
        _logger.LogInformation("OpenWebsite()");

        if (WebsiteLoader.ShowResult)
        {
            await Launcher.OpenAsync(uri: new Uri(WebsiteLoader.Result));
        }
    }
}