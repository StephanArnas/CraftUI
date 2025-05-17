namespace CraftUI.Demo.Presentation.Common;

public class ContentPageBase : ContentPage
{
    protected override void OnAppearing()
    {
        base.OnAppearing();

        ((ViewModelBase) BindingContext)?.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        ((ViewModelBase) BindingContext)?.OnDisappearing();
    }
}