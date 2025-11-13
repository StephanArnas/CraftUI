using CraftUI.Maui.Core.Common.Helpers;
using CraftUI.Maui.Core.Common.Resources;

namespace CraftUI.Maui.Core.Controls.Button;

public partial class CfButton
{
    private void ApplyPlainStyle()
    {
        var primaryColor = Application.Current?.Resources[ColorResources.Primary] as Color ?? Color.FromArgb("#47c599");

        _button.BackgroundColor = BackgroundColor ?? primaryColor;
        _button.TextColor = TextColor ?? ColorHelper.GetContrastingTextColor(primaryColor);
        _button.BorderColor = Colors.Transparent;
        _button.BorderWidth = 0;
    }

    private void ApplyOutlinedStyle()
    {
        var surfaceColor = Application.Current?.Resources[ColorResources.Surface] as Color ?? Colors.White;
        var primaryColor = Application.Current?.Resources[ColorResources.Primary] as Color ?? Color.FromArgb("#47c599");
    
        _button.BackgroundColor = BackgroundColor ?? surfaceColor;
        _button.TextColor = TextColor ?? primaryColor;
        _button.BorderColor = primaryColor;
        _button.BorderWidth = 2;
    }
}