using CraftUI.Demo.Application.Common.Interfaces.Infrastructure;

namespace CraftUI.Demo.Infrastructure.Displays;

public class DisplayService : IDisplayService
{
    public Task ShowPopupAsync(string title, string message, string accept = "OK")
    {
        return Microsoft.Maui.Controls.Application.Current!.Windows[0].Page!.DisplayAlert(title, message, accept);
    }
}