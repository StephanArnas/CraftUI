using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CraftUI.Demo.Application.Common;
using CraftUI.Demo.Application.Common.Interfaces.Infrastructure;
using CraftUI.Demo.Presentation.Common;
using CraftUI.Demo.Presentation.Pages.Controls.Pickers;
using Microsoft.Extensions.Logging;

namespace CraftUI.Demo.Presentation.Pages.Controls;

public partial class ControlsListViewModel : ViewModelBase
{
    private readonly ILogger<PickerPageViewModel> _logger;
    private readonly INavigationService _navigationService;

    [ObservableProperty] 
    private ObservableCollection<LinkMenuItem> _items;

    public ControlsListViewModel(
        ILogger<PickerPageViewModel> logger,
        INavigationService navigationService)
    {
        _logger = logger;
        _navigationService = navigationService;

        InitializeItems();
        
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
    private async Task NavigateToPage(LinkMenuItem? item)
    {
        _logger.LogInformation("NavigateToPage( item: {Item} )", item);

        if (item is not null)
        {
            await _navigationService.NavigateToAsync(item.RoutePage);
        }
    }

    private void InitializeItems()
    {
        Items =
        [
            new LinkMenuItem("Button", "demo_hand_click.png", RouteConstants.ButtonPage),
            new LinkMenuItem("Entry", "demo_input.png", RouteConstants.EntryPage),
            new LinkMenuItem("Date Picker", "demo_date_picker.png", RouteConstants.DatePickerPage),
            new LinkMenuItem("Native Picker", "demo_picker.png", RouteConstants.PickerPage),
            new LinkMenuItem("Popup Picker", "demo_picker.png", RouteConstants.PickerPopupPage),
            new LinkMenuItem("Mutli Popup Picker", "demo_picker.png", RouteConstants.MultiPickerPopupPage),
            new LinkMenuItem("Progress Bar", "demo_progress_bar.png", RouteConstants.ProgressBarPage)
        ];
    }
}