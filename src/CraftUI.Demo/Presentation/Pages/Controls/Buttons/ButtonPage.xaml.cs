namespace CraftUI.Demo.Presentation.Pages.Controls.Buttons;

public partial class ButtonPage
{
    public ButtonPage(ButtonPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}