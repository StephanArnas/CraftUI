using CraftUI.Maui.Controls.ProgressBars;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace CraftUI.Maui;

public static class DependencyInjection
{
    public static MauiAppBuilder UseMauiCraftUi(this MauiAppBuilder builder)
    {
        builder.UseSkiaSharp();
        
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler<ProgressBar, CfProgressBarHandler>();
        });
        
        return builder;
    }
}