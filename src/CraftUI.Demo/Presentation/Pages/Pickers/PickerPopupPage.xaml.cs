namespace CraftUI.Demo.Presentation.Pages.Pickers;

public partial class PickerPopupPage
{
    public PickerPopupPage(PickerPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}