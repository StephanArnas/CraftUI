namespace CraftUI.Library.Maui.MarkupExtensions;

public static class GutterSystem
{
    public static double WidthScreen => DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density;
    public static double HeightScreen => DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density;

    private const double DefaultGutter = 2;
    private const double DefaultLeftSmallGutter = 1;
    private const double DefaultRightSmallGutter = 0.5;

    public const double SmallSpaceValue = 8;
    public const double DefaultSpaceValue = 16;
    public const double LargeSpaceValue = 32;

    public static Thickness GetGutter(double left, double top, double right, double bottom)
    {
        return new Thickness(left * DefaultSpaceValue, top * DefaultSpaceValue, right * DefaultSpaceValue, bottom * DefaultSpaceValue);
    }

    public static double GetSpacing(double value)
    {
        return value * DefaultSpaceValue;
    }

    public static double GetHorizontalMarge(GutterType type, bool isLeft)
    {
        var value = 0D;

        if ((type & GutterType.Default) == GutterType.Default)
        {
            value += DefaultGutter;
        }

        if ((type & GutterType.Small) == GutterType.Small)
        {
            value += isLeft ? DefaultLeftSmallGutter : DefaultRightSmallGutter;
        }

        if ((type & GutterType.Device) == GutterType.Device)
        {
            value += isLeft ? DefaultLeftSmallGutter + 0.5 : DefaultGutter;
        }

        return value;
    }


    public static double GetVerticalMarge(GutterType type, bool isTop)
    {
        var value = 0D;

        if ((type & GutterType.VerticalSmall) == GutterType.VerticalSmall)
        {
            value += DefaultLeftSmallGutter;
        }

        if ((type & GutterType.VerticalDefault) == GutterType.VerticalDefault)
        {
            value += DefaultGutter;
        }

        if (isTop == false)
        {
            if ((type & GutterType.VerticalSmallBottom) == GutterType.VerticalSmallBottom)
            {
                value += DefaultLeftSmallGutter;
            }

            if ((type & GutterType.VerticalDefaultBottom) == GutterType.VerticalDefaultBottom)
            {
                value += DefaultGutter;
            }
        }

        if ((type & GutterType.Device) == GutterType.Device)
        {
            value += DefaultRightSmallGutter;
        }

        return value;
    }
}

[Flags]
public enum GutterType
{
    None = 0,
    Default = 0x001,
    Small = 0x002,

    VerticalSmall = 0x004,
    VerticalDefault = 0x008,

    VerticalSmallBottom = 0x010,
    VerticalDefaultBottom = 0x020,

    Device = 0x040,
    iOS = 0x80,

    DefaultHeader = Default | VerticalSmallBottom,

    Large = Default | Small,
    DefaultSmall = Default | VerticalSmall,
    LargeSmall = Large | VerticalSmall,
}

[AcceptEmptyServiceProvider]
[ContentProperty("All")]
public class GutterExtension : IMarkupExtension
{
    public GutterType Type { get; set; }

    public double All { get; set; }

    public double? Top { get; set; }

    public double? Bottom { get; set; }

    public double? Right { get; set; }

    public double? Left { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            if ((Type & GutterType.iOS) == GutterType.iOS)
            {
                var thickness = GetThickness();
                return new Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
            }
        }

        return GetThickness();
    }

    private Thickness GetThickness()
    {
        if (Type != GutterType.None)
        {
            Left ??= GutterSystem.GetHorizontalMarge(Type, true);
            Right ??= GutterSystem.GetHorizontalMarge(Type, false);
            Top ??= GutterSystem.GetVerticalMarge(Type, true);
            Bottom ??= GutterSystem.GetVerticalMarge(Type, false);
        }

        return GutterSystem.GetGutter(Left ?? All,
                                      Top ?? All,
                                      Right ?? All,
                                      Bottom ?? All);
    }
}