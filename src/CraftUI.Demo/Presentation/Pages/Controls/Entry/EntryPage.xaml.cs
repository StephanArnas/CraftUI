namespace CraftUI.Demo.Presentation.Pages.Controls.Entry;

public partial class EntryPage
{
    public EntryPage(EntryPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}