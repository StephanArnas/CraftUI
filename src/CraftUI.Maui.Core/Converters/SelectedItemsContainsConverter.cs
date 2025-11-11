using System.Globalization;
using CraftUI.Maui.Core.Popups;

namespace CraftUI.Maui.Core.Converters;

public class SelectedItemsContainsConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return false;
        }
        
        if (parameter is not CfCollectionMultiSelectionPopup popup)
        {
            return false;
        }
        
        return popup.SelectedItems?.Contains(value) == true;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}