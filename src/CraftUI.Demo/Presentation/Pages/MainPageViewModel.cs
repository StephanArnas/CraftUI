using CraftUI.Demo.Application.Common.Interfaces.Infrastructure;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using CraftUI.Demo.Presentation.Common;

namespace CraftUI.Demo.Presentation.Pages;

public partial class MainPageViewModel : ViewModelBase
{   
    private readonly ILogger<MainPageViewModel> _logger;
    private readonly INavigationService _navigationService;
    
    public MainPageViewModel(
        ILogger<MainPageViewModel> logger,
        INavigationService navigationService)
    {
        _logger = logger;
        _navigationService = navigationService;
        
        _logger.LogInformation("Building MainPageViewModel");
    }
    
    [RelayCommand]
    private async Task GoToButtonPage()
    {
        _logger.LogInformation("GoToButtonPage()");
        
        await _navigationService.NavigateToAsync(RouteConstants.ButtonPage);
    }
    
    [RelayCommand]
    private async Task GoToEntryPage()
    {
        _logger.LogInformation("GoToEntryPage()");
        
        await _navigationService.NavigateToAsync(RouteConstants.EntryPage);
    }
    
    [RelayCommand]
    private async Task GoToPickerPage()
    {
        _logger.LogInformation("GoToPickerPage()");
        
        await _navigationService.NavigateToAsync(RouteConstants.PickerPage);
    }
    
    [RelayCommand]
    private async Task GoToPickerPopupPage()
    {
        _logger.LogInformation("GoToPickerPopupPage()");
        
        await _navigationService.NavigateToAsync(RouteConstants.PickerPopupPage);
    }
}