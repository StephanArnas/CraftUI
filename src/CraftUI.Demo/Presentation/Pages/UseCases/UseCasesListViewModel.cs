using CraftUI.Demo.Application.Common.Interfaces.Infrastructure;
using CraftUI.Demo.Presentation.Common;
using Microsoft.Extensions.Logging;

namespace CraftUI.Demo.Presentation.Pages.UseCases;

public partial class UseCasesListViewModel : ViewModelBase
{   
    private readonly ILogger<UseCasesListViewModel> _logger;
    private readonly INavigationService _navigationService;
    
    public UseCasesListViewModel(
        ILogger<UseCasesListViewModel> logger,
        INavigationService navigationService)
    {
        _logger = logger;
        _navigationService = navigationService;
        
        _logger.LogInformation("Building MainPageViewModel");
    }
}