namespace CraftUI.Demo.Presentation.Pages.Controls.ProgressBars;

public partial class ProgressBarPage
{
    public ProgressBarPage(ProgressBarPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}