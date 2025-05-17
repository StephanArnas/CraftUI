namespace CraftUI.Demo.Application.Common.Interfaces.Infrastructure;

public interface IDisplayService
{
    Task ShowPopupAsync(string title, string message, string accept = "OK");
}