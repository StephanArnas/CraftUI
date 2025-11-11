namespace CraftUI.Maui.Core.Common;

public static class ScreenSize
{
    public static double WidthScreen => DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density;
    public static double HeightScreen => DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density;
}