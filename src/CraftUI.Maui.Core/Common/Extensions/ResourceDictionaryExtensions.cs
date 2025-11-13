namespace CraftUI.Maui.Core.Common.Extensions;

internal static class ResourceDictionaryExtensions
{
    public static bool TryGetColor(this ResourceDictionary rd, string key, out Color color)
    {
        if (rd.TryGetValue(key, out var obj) && obj is Color c)
        {
            color = c;
            return true;
        }

        color = default!;
        return false;
    }

    public static void TryAddColor(this ResourceDictionary rd, string key, Color color)
    {
        if (!rd.ContainsKey(key))
        {
            rd.Add(key, color);
        }
    }
}