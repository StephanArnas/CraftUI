namespace CraftUI.Demo.Presentation.Pages.Controls;

public partial class ControlsList
{
    public ControlsList(ControlsListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        ItemsCollectionView.SelectedItem = null;
        base.OnAppearing();
    }
}