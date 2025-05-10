using CraftUI.Library.Maui.Common.Helpers;

namespace CraftUI.Demo;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        SetBackgroundColor(this, ResourceHelper.GetResource<Color>("Primary"));
        SetTitleColor(this, ResourceHelper.GetResource<Color>("White"));
    }
}