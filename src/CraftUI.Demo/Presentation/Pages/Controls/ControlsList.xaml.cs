namespace CraftUI.Demo.Presentation.Pages.Controls;

public partial class ControlsList
{
    public ControlsList(ControlsListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}