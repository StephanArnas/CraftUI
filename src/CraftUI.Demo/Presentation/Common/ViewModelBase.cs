using CommunityToolkit.Mvvm.ComponentModel;

namespace CraftUI.Demo.Presentation.Common;

public  abstract class ViewModelBase : ObservableObject, IQueryAttributable
{
    protected bool IsAppearingFromDetailPage;

    protected ViewModelBase() { }

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        query.Clear();
    }

    public virtual void OnAppearing()
    {
        IsAppearingFromDetailPage = false;
    }

    public virtual void OnDisappearing() { }
}