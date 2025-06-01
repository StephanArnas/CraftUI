namespace CraftUI.Demo.Presentation.Pages.Controls.Pickers;

public partial class MultiPickerPopupPage
{
    public MultiPickerPopupPage(PickerPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}