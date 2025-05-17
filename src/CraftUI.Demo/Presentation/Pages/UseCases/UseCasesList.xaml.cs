namespace CraftUI.Demo.Presentation.Pages.UseCases;

public partial class UseCasesList
{
    public UseCasesList(UseCasesListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}