using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Maui;
using CraftUI.Maui.Core.Common;
using CraftUI.Maui.Core.Common.Extensions;
using CraftUI.Maui.Core.Controls.ProgressBars;
using Microsoft.Maui.Controls.Shapes;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Microsoft.Maui.Handlers;

namespace CraftUI.Maui.Core;

public static class DependencyInjection
{
    public static MauiAppBuilder UseMauiCraftUi(this MauiAppBuilder builder)
    {
        builder
            .UseSkiaSharp()
            .UseMauiCommunityToolkit(ConfigurePopup)
            .ConfigureMauiHandlers(handlers =>
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

    private static void ConfigurePopup(Options options)
    {
        options.SetPopupDefaults(new DefaultPopupSettings
        {
            CanBeDismissedByTappingOutsideOfPopup = true,
            Margin = 0,
            Padding = 0
        });

        options.SetPopupOptionsDefaults(new DefaultPopupOptionsSettings
        {
            CanBeDismissedByTappingOutsideOfPopup = true,
            Shape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(8),
                StrokeThickness = 0
            }
        });
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
        
        EnsurePrimaryScale(app.Resources);
    }
    
    private static void EnsurePrimaryScale(ResourceDictionary root)
    {
        if (!TryFindPrimaryBase(root, out var basePrimary))
        {
            basePrimary = Color.FromArgb("#47c599");
        }
        
        var palette = TailwindColors.BuildPrimaryScale(basePrimary);

        foreach (var kvp in palette)
        {
            // Ne surtout pas écraser ce qui existe déjà
            if (!root.ContainsKey(kvp.Key))
            {
                root.Add(kvp.Key, kvp.Value);
            }
        }
    }
    
    private static bool TryFindPrimaryBase(ResourceDictionary rd, out Color color)
    {
        // Recherche directe dans le dico racine
        if (rd.TryGetColor("Primary", out color))
        {
            return true;
        }
        if (rd.TryGetColor("Primary500", out color))
        {
            return true;
        }

        // Recherche dans les MergedDictionaries (ordre de merge -> priorité naturelle)
        foreach (var md in rd.MergedDictionaries)
        {
            if (md.TryGetColor("Primary", out color))
            {
                return true;
            }
            if (md.TryGetColor("Primary500", out color))
            {
                return true;
            }
        }

        color = default!;
        return false;
    }
}
