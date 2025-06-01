namespace CraftUI.Demo.Presentation.Pages.Controls.DatePickers;

public partial class DatePickerPage
{
    public DatePickerPage(DatePickerPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}