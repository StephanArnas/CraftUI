namespace CraftUI.Demo.Presentation.Pages.Controls.Pickers;

public partial class PickerPopupPage
{
    public PickerPopupPage(PickerPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}