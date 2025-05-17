using CraftUI.Demo.Presentation.Common;
using Microsoft.Extensions.Logging;

namespace CraftUI.Demo.Presentation.Pages.Settings;

public class SettingsPageViewModel : ViewModelBase
{
    private readonly ILogger<SettingsPageViewModel> _logger;

    public SettingsPageViewModel(
        ILogger<SettingsPageViewModel> logger)
    {
        _logger = logger;

        _logger.LogInformation("Building SettingsPageViewModel");
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
}