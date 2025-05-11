namespace CraftUI.Demo.Presentation.Pages.Settings;

public partial class SettingsPage
{
    public SettingsPage(SettingsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}