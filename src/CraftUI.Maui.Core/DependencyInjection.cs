using System.Diagnostics.CodeAnalysis;
using CraftUI.Maui.Core.Controls.ProgressBars;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Microsoft.Maui.Handlers;

namespace CraftUI.Maui.Core;

public static class DependencyInjection
{
    public static MauiAppBuilder UseMauiCraftUi(this MauiAppBuilder builder)
    {
        builder.UseSkiaSharp();

        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler<ProgressBar, CfProgressBarHandler>();

            ApplicationHandler.Mapper.AppendToMapping("CraftUIThemeMerge", (_, view) =>
            {
                if (view is Application app)
                {
#pragma warning disable IL2026 // This call is safe: CraftUiResourceMerger preserves required types and is only invoked at runtime on the app instance.
                    CraftUiResourceMerger.MergeInto(app);
#pragma warning restore IL2026
                }
            });
        });

        return builder;
    }
}

internal static class CraftUiResourceMerger
{
    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
    public static void MergeInto(Application app)
    {
        var markerAssembly = typeof(IMauiMarker).Assembly;
        ArgumentNullException.ThrowIfNull(markerAssembly);

        var types = markerAssembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && typeof(ResourceDictionary).IsAssignableFrom(t))
            // Optional: only pick dictionaries that live in a Resources* namespace to avoid accidental merges
            .Where(t => t.Namespace != null && t.Namespace.Contains("Resources", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        foreach (var type in types)
        {
            var alreadyMerged = app.Resources.MergedDictionaries.Any(d => d.GetType() == type);
            if (!alreadyMerged)
            {
                var dict = (ResourceDictionary)Activator.CreateInstance(type)!;
                app.Resources.MergedDictionaries.Add(dict);
            }
        }
    }
}