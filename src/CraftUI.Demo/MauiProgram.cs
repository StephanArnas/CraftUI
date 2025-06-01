using CraftUI.Demo.Infrastructure;
using CraftUI.Library.Maui;
using CraftUI.Demo.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using CraftUI.Demo.Presentation.Common;
using CraftUI.Demo.Presentation.Pages.Controls;
using CraftUI.Demo.Presentation.Pages.Controls.Buttons;
using CraftUI.Demo.Presentation.Pages.Controls.DatePickers;
using CraftUI.Demo.Presentation.Pages.Controls.Entries;
using CraftUI.Demo.Presentation.Pages.Controls.Pickers;
using CraftUI.Demo.Presentation.Pages.Controls.ProgressBars;
using CraftUI.Demo.Presentation.Pages.Settings;
using CraftUI.Demo.Presentation.Pages.UseCases;

namespace CraftUI.Demo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .AddBlogComponents()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddInfrastructure();
        builder.Services.AddServices();

        ApplyStyleCustomization();
        
        // Register your pages.
        builder.Services.AddTransient<UseCasesList, UseCasesListViewModel>();
        builder.Services.AddTransientWithShellRoute<ButtonPage, ButtonPageViewModel>(RouteConstants.ButtonPage);
        builder.Services.AddTransientWithShellRoute<EntryPage, EntryPageViewModel>(RouteConstants.EntryPage);
        builder.Services.AddTransientWithShellRoute<DatePickerPage, DatePickerPageViewModel>(RouteConstants.DatePickerPage);
        builder.Services.AddTransientWithShellRoute<PickerPage, PickerPageViewModel>(RouteConstants.PickerPage);
        builder.Services.AddTransientWithShellRoute<PickerPopupPage, PickerPageViewModel>(RouteConstants.PickerPopupPage);
        builder.Services.AddTransientWithShellRoute<MultiPickerPopupPage, PickerPageViewModel>(RouteConstants.MultiPickerPopupPage);
        builder.Services.AddTransientWithShellRoute<ProgressBarPage, ProgressBarPageViewModel>(RouteConstants.ProgressBarPage);
        
        builder.Services.AddTransient<ControlsList, ControlsListViewModel>();
        builder.Services.AddTransient<SettingsPage, SettingsPageViewModel>();

        return builder.Build();
    }
    
    private static void ApplyStyleCustomization()
    {
        EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
#if ANDROID
        // Remove the underline from the EditText
        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
        });
        
        EntryHandler.Mapper.AppendToMapping("SetUpEntry", (handler, _) =>
        {
#if ANDROID

#elif IOS || MACCATALYST
        // Remove outline from the UITextField
        handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS

#endif
        });
        
        PickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, _) =>
        {
#if ANDROID
        // Remove the underline from the Spinner (Picker)
        handler.PlatformView.Background = null;
#endif
        });

        PickerHandler.Mapper.AppendToMapping("SetUpPicker", (handler, _) =>
        {
#if ANDROID
        // Set the background to transparent
        handler.PlatformView.Background = null;
#elif IOS || MACCATALYST
        // Remove border for the UITextField (Picker)
        if (handler.PlatformView is UIKit.UITextField textField)
        {
            textField.BorderStyle = UIKit.UITextBorderStyle.None;
        }
#elif WINDOWS

#endif
        });
        
        DatePickerHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.Background = null;
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#elif IOS || MACCATALYST
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
        });
    }
}
