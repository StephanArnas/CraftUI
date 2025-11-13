namespace CraftUI.Maui.Core.Common;

internal static class TailwindColors
{
    private static readonly (string Suffix, float TargetL, float SatFactor)[] Steps =
    [
        ("050", 0.96f, 0.85f),
        ("100", 0.90f, 0.90f),
        ("200", 0.82f, 0.95f),
        ("300", 0.72f, 1.00f),
        ("400", 0.62f, 1.05f),
        ("500", 0.52f, 1.00f),
        ("600", 0.44f, 1.00f),
        ("700", 0.36f, 0.95f),
        ("800", 0.26f, 0.90f),
        ("900", 0.18f, 0.90f),
        ("950", 0.12f, 0.85f)
    ];

    public static IReadOnlyDictionary<string, Color> BuildPrimaryScale(Color baseColor)
    {
        RgbToHsl(baseColor, out var h, out var s, out var l);

        var dict = new Dictionary<string, Color>(StringComparer.Ordinal);
        foreach (var (suffix, targetL, satFactor) in Steps)
        {
            var s2 = Clamp01(s * satFactor);
            var c = HslToRgb(h, s2, targetL, baseColor.Alpha);
            dict.Add($"Primary{suffix}", c);
        }

        if (!dict.ContainsKey("Primary"))
        {
            dict.Add("Primary", dict["Primary500"]);
        }

        return dict;
    }

    private static float Clamp01(float v) { return v < 0 ? 0 : v > 1 ? 1 : v; }

    // Convert MAUI Color (0..1) to HSL (0..1)
    private static void RgbToHsl(Color c, out float h, out float s, out float l)
    {
        var r = (float)c.Red;
        var g = (float)c.Green;
        var b = (float)c.Blue;

        var max = MathF.Max(r, MathF.Max(g, b));
        var min = MathF.Min(r, MathF.Min(g, b));
        l = (max + min) * 0.5f;

        if (MathF.Abs(max - min) < 1e-6f)
        {
            h = 0f;
            s = 0f;
            return;
        }

        var d = max - min;
        s = l > 0.5f ? d / (2f - max - min) : d / (max + min);

        if (max == r)
        {
            h = (g - b) / d + (g < b ? 6f : 0f);
        }
        else if (max == g)
        {
            h = (b - r) / d + 2f;
        }
        else
        {
            h = (r - g) / d + 4f;
        }

        h /= 6f; // 0..1
    }

    // HSL (0..1) to MAUI Color
    private static Color HslToRgb(float h, float s, float l, float a)
    {
        if (s <= 1e-6f)
        {
            return new Color(l, l, l, a); // gris
        }

        float q = l < 0.5f ? l * (1f + s) : (l + s - l * s);
        float p = 2f * l - q;

        float r = HueToRgb(p, q, h + 1f / 3f);
        float g = HueToRgb(p, q, h);
        float b = HueToRgb(p, q, h - 1f / 3f);

        return new Color(r, g, b, a);
    }

    private static float HueToRgb(float p, float q, float t)
    {
        if (t < 0f) { t += 1f; }
        if (t > 1f) { t -= 1f; }
        if (t < 1f / 6f) { return p + (q - p) * 6f * t; }
        if (t < 1f / 2f) { return q; }
        if (t < 2f / 3f) { return p + (q - p) * (2f / 3f - t) * 6f; }
        return p;
    }
}