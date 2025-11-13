using CraftUI.Demo.Infrastructure;
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
using CraftUI.Maui.Core;
using Microsoft.Maui.Controls.Shapes;

namespace CraftUI.Demo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCraftUi()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services
            .AddInfrastructure()
            .AddServices();

        ApplyStyleCustomization();

        // Register your pages.
        builder.Services
            .AddTransient<UseCasesList, UseCasesListViewModel>()
            .AddTransient<ControlsList, ControlsListViewModel>()
            .AddTransient<SettingsPage, SettingsPageViewModel>()
            .AddTransientWithShellRoute<ButtonPage, ButtonPageViewModel>(RouteConstants.ButtonPage)
            .AddTransientWithShellRoute<EntryPage, EntryPageViewModel>(RouteConstants.EntryPage)
            .AddTransientWithShellRoute<DatePickerPage, DatePickerPageViewModel>(RouteConstants.DatePickerPage)
            .AddTransientWithShellRoute<PickerPage, PickerPageViewModel>(RouteConstants.PickerPage)
            .AddTransientWithShellRoute<PickerPopupPage, PickerPageViewModel>(RouteConstants.PickerPopupPage)
            .AddTransientWithShellRoute<MultiPickerPopupPage, PickerPageViewModel>(RouteConstants.MultiPickerPopupPage)
            .AddTransientWithShellRoute<ProgressBarPage, ProgressBarPageViewModel>(RouteConstants.ProgressBarPage);

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
#elif IOS || MACCATALYST
            //handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            //handler.PlatformView.Layer.BorderWidth = 0;
            //handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
        });
    }
}