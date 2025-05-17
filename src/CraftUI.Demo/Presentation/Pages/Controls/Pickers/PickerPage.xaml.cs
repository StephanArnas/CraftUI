namespace CraftUI.Demo.Presentation.Pages.Controls.Pickers;

public partial class PickerPage
{
    public PickerPage(PickerPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}