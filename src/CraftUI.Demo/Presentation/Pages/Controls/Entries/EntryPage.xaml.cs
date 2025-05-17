namespace CraftUI.Demo.Presentation.Pages.Controls.Entries;

public partial class EntryPage
{
    public EntryPage(EntryPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}