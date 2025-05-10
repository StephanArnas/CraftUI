using SkiaSharp.Views.Maui.Controls.Hosting;
using CraftUI.Library.Maui.Controls.ProgressBars;

namespace CraftUI.Library.Maui;

public static class DependencyInjection
{
    public static MauiAppBuilder AddBlogComponents(this MauiAppBuilder builder)
    {
        builder.UseSkiaSharp();
        
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler<ProgressBar, SaProgressBarHandler>();
        });
        
        return builder;
    }
}