using CraftUI.Demo.Application.Common.Interfaces.Infrastructure;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace CraftUI.Demo.Infrastructure.Toasts;

public class ToastService : IToastService
{
    public Task ShowAsync(string message, ToastDuration duration = ToastDuration.Short, double textSize = 14.0)
    {
        return Toast.Make(message, duration, textSize).Show();
    }
}