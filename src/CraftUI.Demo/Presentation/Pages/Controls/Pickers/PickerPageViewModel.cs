using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CraftUI.Demo.Application.Cities;
using CraftUI.Demo.Application.Common.Interfaces.Services;
using CraftUI.Demo.Application.Countries;
using CraftUI.Demo.Presentation.Common;
using Microsoft.Extensions.Logging;
using Sharpnado.TaskLoaderView;

namespace CraftUI.Demo.Presentation.Pages.Controls.Pickers;

public partial class PickerPageViewModel : ViewModelBase
{
    private readonly ILogger<PickerPageViewModel> _logger;
    private readonly ICityService _cityService;
    private readonly ICountryService _countryService;
    
    [ObservableProperty]
    private CountryVm? _country;
    
    [ObservableProperty]
    private CityVm? _city;
    
    public static string CountryDisplayProperty => nameof(CountryVm.Name);
    public static string CityDisplayProperty => nameof(CityVm.Name);

    public TaskLoaderNotifier<IReadOnlyCollection<CountryVm>> CountriesLoader { get; } = new();
    public TaskLoaderNotifier<IReadOnlyCollection<CityVm>> CitiesLoader { get; } = new();

    public PickerPageViewModel(
        ILogger<PickerPageViewModel> logger,
        ICityService cityService,
        ICountryService countryService)
    {
        _logger = logger;
        _cityService = cityService;
        _countryService = countryService;
        
        _logger.LogInformation("Building LabelPageViewModel");
    }
    
    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _logger.LogInformation("ApplyQueryAttributes( query: {Query} )", query);
        
        base.ApplyQueryAttributes(query);
    }

    public override void OnAppearing()
    {
        _logger.LogInformation("OnAppearing()");

        if (CountriesLoader.IsNotStarted)
        {
            CountriesLoader.Load(_ => LoadCountriesAsync());
        }
        
        base.OnAppearing();
    }

    public override void OnDisappearing()
    {
        _logger.LogInformation("OnDisappearing()");
        
        base.OnDisappearing();
    }
    
    private async Task<IReadOnlyCollection<CountryVm>> LoadCountriesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("LoadCountriesAsync()");

        var domainResult = await _countryService.GetAllCountriesAsync(cancellationToken);
        _logger.LogInformation("Items loaded: {Count}", domainResult.Count);

        return domainResult;
    }

    private async Task<IReadOnlyCollection<CityVm>> LoadCitiesByCountry(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("LoadCitiesByCountry()");

        if (Country is null)
        {
            // Add a toast to enhance the user experience.
            return Array.Empty<CityVm>();
        }
        
        var domainResult = await _cityService.GetCitiesAsync(Country.Id, cancellationToken);
        _logger.LogInformation("Items loaded: {Count}", domainResult.Count);
        
        return domainResult;
    }

    [RelayCommand]
    private Task CountrySelected()
    {
        _logger.LogInformation("CountrySelected()");

        City = null;
        CitiesLoader.Reset();
        CitiesLoader.Load(_ => LoadCitiesByCountry());
        
        return Task.CompletedTask;
    }
    
    [RelayCommand]
    private Task Reset()
    {
        _logger.LogInformation("Reset()");
        
        Country = null;
        City = null;
        CitiesLoader.Reset();
        
        return Task.CompletedTask;
    }
}