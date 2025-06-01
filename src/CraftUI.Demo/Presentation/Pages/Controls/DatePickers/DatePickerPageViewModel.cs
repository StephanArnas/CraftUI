using CommunityToolkit.Mvvm.ComponentModel;
using CraftUI.Demo.Presentation.Common;
using Microsoft.Extensions.Logging;

namespace CraftUI.Demo.Presentation.Pages.Controls.DatePickers;

public partial class DatePickerPageViewModel : ViewModelBase
{
    private readonly ILogger<DatePickerPageViewModel> _logger;
    
    [ObservableProperty]
    private DateTime _date;
    
    [ObservableProperty]
    private DateTime? _dateNullable;
    
    [ObservableProperty]
    private DateTime? _rangeDateNullable;
    
    [ObservableProperty]
    private DateTime _minimumDate;
    
    [ObservableProperty]
    private DateTime _maximumDate;
    
    public DatePickerPageViewModel(
        ILogger<DatePickerPageViewModel> logger)    
    {
        _logger = logger;
        
        MinimumDate = DateTime.Now.AddDays(-1);
        MaximumDate = DateTime.Now.AddDays(30);

        _logger.LogInformation("Building DatePickerPageViewModel");
    }

    public override void OnAppearing()
    {
        _logger.LogInformation("OnAppearing()");
        
        Date = DateTime.Now;
        DateNullable = null;
        RangeDateNullable = null;
        
        base.OnAppearing();
    }
}