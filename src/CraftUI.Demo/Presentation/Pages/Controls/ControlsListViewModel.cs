using CommunityToolkit.Mvvm.Input;
using CraftUI.Demo.Application.Common.Interfaces.Infrastructure;
using CraftUI.Demo.Presentation.Common;
using CraftUI.Demo.Presentation.Pages.Controls.Pickers;
using Microsoft.Extensions.Logging;

namespace CraftUI.Demo.Presentation.Pages.Controls;

public partial class ControlsListViewModel : ViewModelBase
{
    private readonly ILogger<PickerPageViewModel> _logger;
    private readonly INavigationService _navigationService;

    public ControlsListViewModel(
        ILogger<PickerPageViewModel> logger,
        INavigationService navigationService)
    {
        _logger = logger;
        _navigationService = navigationService;
        
        _logger.LogInformation("Building ControlsListViewModel");
    }
    
    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _logger.LogInformation("ApplyQueryAttributes( query: {Query} )", query);
        
        base.ApplyQueryAttributes(query);
    }

    public override void OnAppearing()
    {
        _logger.LogInformation("OnAppearing()");

        base.OnAppearing();
    }

    public override void OnDisappearing()
    {
        _logger.LogInformation("OnDisappearing()");
        
        base.OnDisappearing();
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