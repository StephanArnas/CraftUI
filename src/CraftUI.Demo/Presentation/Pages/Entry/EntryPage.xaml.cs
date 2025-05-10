namespace CraftUI.Demo.Presentation.Pages.Entry;

public partial class EntryPage
{
    public EntryPage(EntryPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}