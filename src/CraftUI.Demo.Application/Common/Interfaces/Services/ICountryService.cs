using CraftUI.Demo.Application.Countries;

namespace CraftUI.Demo.Application.Common.Interfaces.Services;

public interface ICountryService
{
    Task<IReadOnlyList<CountryVm>> GetAllCountriesAsync(CancellationToken cancellationToken = default);
}