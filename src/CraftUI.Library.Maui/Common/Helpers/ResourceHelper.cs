namespace CraftUI.Library.Maui.Common.Helpers;

public static class ResourceHelper
{
    public static T GetResource<T>(string key)
    {
        if (Application.Current!.Resources.TryGetValue(key, out var value))
        {
            return (T)value;
        }

        throw new InvalidOperationException($"key {key} not found in the resource dictionary");
    }
    
    public static Color GetThemeColor(string lightKey, string darkKey)
    {
        return Application.Current!.RequestedTheme switch
        {
            AppTheme.Dark => GetResource<Color>(darkKey),
            AppTheme.Light => GetResource<Color>(lightKey),
            _ => GetResource<Color>(lightKey)
        };
    }
}