using Foundation;

namespace CraftUI.Demo;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp()
    {
        // Fix for iOS line height issue on latest XCode version.
        // https://github.com/dotnet/maui/issues/30609
        Microsoft.Maui.Handlers.LabelHandler.Mapper.ModifyMapping(nameof(ILabel.LineHeight),
            (handler, label, action) =>
            {
                // Skip line height mapping if the value would cause an exception
                if (label.LineHeight > 0)
                {
                    action(handler, label);
                }
            });

        return MauiProgram.CreateMauiApp();
    }
}