using CommunityToolkit.Maui.Core;

namespace CraftUI.Demo.Application.Common.Interfaces.Infrastructure;

public interface IToastService
{
    Task ShowAsync(string message, ToastDuration duration = ToastDuration.Short, double textSize = 14.0);
}