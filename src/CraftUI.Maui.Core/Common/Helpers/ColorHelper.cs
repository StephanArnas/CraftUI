namespace CraftUI.Maui.Core.Common.Helpers;

public static class ColorHelper
{
    public static Color GetContrastingTextColor(Color backgroundColor)
    {
        var luminance = 0.299 * backgroundColor.Red + 0.587 * backgroundColor.Green + 0.114 * backgroundColor.Blue;
        return luminance > 0.5 ? Colors.Black : Colors.White;
    }
}