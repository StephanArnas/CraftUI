namespace CraftUI.Demo.Application.Common.Interfaces.Infrastructure;

public interface INavigationService
{
    /// <summary>
    /// Performs hierarchical navigation to a specific page using a registered navigation route.
    /// Can optionally pass named route parameters to be used for processing in the destination page.
    /// </summary>
    /// <param name="route">Name of the page</param>
    /// <param name="routeParameters">Dictionary of parameters</param>
    Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null);
    
    /// <summary>
    /// Returns to the previous page in the navigation stack.
    /// </summary>
    Task GoBackAsync();
}