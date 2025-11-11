namespace CraftUI.Maui.Core.Controls.DatePicker;

/// <summary>
/// This control exists to avoid the issue of DatePicker setting the date today when the date is set today.
/// Source: https://github.com/dotnet/maui/issues/13156
/// </summary>
public class CfDatePickerInternal : Microsoft.Maui.Controls.DatePicker, IDatePicker
{
    DateTime IDatePicker.Date
    {
        get => Date;
        set
        {
            if (value.Equals(DateTime.Today.Date))
            {
                Date = value.AddDays(-1);
            }
            
            Date = value;
            OnPropertyChanged();
        }
    }
}