# CraftUI for .NET MAUI 

![image](https://github.com/user-attachments/assets/08942e52-75a7-4515-9c58-8ecfc1504d54)

CraftUI is a design system library for .NET MAUI, giving developers a fully customizable and maintainable set of UI components, free from external dependencies and package managers. 
Just integrate the code directly into your project and shape it exactly to your needs, now and forever.

## Why no NuGet package? 

Relying on a package is always a trade-off that should be carefully considered. 
While packages provide convenience, they often introduce hidden risks:

- Popular libraries like MediatR and MassTransit eventually moved to paid models, creating unexpected costs.
- Package updates can introduce regressions or breaking changes that disrupt stable codebases.
- Some projects become abandoned, leaving you with outdated dependencies and no support.
- Version conflicts between packages can create complex dependency hell scenarios.
- You lose visibility and control over the underlying code, making debugging and customization harder.

With CraftUI, you own the code from day one. 
No hidden surprises, no lock-in, no external constraints. 
You’re free to evolve it at your own pace and ensure long-term stability and maintainability.

> 📅 I’m currently working on an article, a video, and a script that will guide you through all the steps required to start a new project with CraftUI or integrate it into an existing one. 
Estimated delivery: 06/2025.

# Controls

CraftUI provides a set of reusable UI controls to accelerate your .NET MAUI development while keeping full control over customization and code ownership.

- CfButton
- CfEntry
- CfPicker
- CfPickerPopup
- CfProgressBar
- CfCollectionPopup

## CfButton

Important properties that don't exist in a native MAUI Button control but are available in the CfButton:

- **IsLoading** Shows a progress bar at the bottom

Both Button and CfButton have two main styles available:

- **PlainPrimary(Cf)Button** - Text-only button with primary color text and transparent background
- **FilledPrimary(Cf)Button** - Solid button with primary color background and contrasting text

<table>
    <tr>
        <td><img src="https://github.com/user-attachments/assets/140d0a37-a6c5-48a8-bc78-f8831665c3c9" width="300"/></td>
        <td><img src="https://github.com/user-attachments/assets/307caa80-3aa4-49bf-87c0-3a37969ffb0d" width="300"/></td>
    </tr>
</table>

```xaml
<controls:CfButton 
    Text="Load 5s Data Demo"
    IsLoading="{Binding DemoOneCommand.Notifier.ShowLoader}"
    Command="{Binding DemoOneCommand}" />
```

## CfEntry

Important properties that don't exist in a native MAUI Entry control but are available in the CfEntry:

- **Label** Provides a text label above the input field
- **Error** Displays validation error messages
- **IsRequired** Indicates if the field requires input
- **Info** Shows additional information text below the input
- **ActionIconSource** Adds an icon button inside the entry
- **ActionIconCommand** Command executed when the action icon is tapped
- **IsLoading** Shows a loading indicator

<table>
    <tr>
        <td><img src="https://github.com/user-attachments/assets/fa48ccce-8522-4e86-b14f-72f199c5a513" width="300"/></td>
        <td><img src="https://github.com/user-attachments/assets/5162177c-5aad-45db-bbd4-d6d0e089982b" width="300"/></td>
    </tr>
</table>

```xaml
<controls:CfEntry
    Label="First name or Last name"
    Text="{Binding FullName, Mode=TwoWay}"
    Error="{Binding ValidationResult, 
        Converter={StaticResource ShowErrorConverter}, 
        ConverterParameter={x:Static page:EntryPageViewModelValidator.FullNameProperty}}"
    IsRequired="True"
    Placeholder="John Doe"
    ActionIconSource="demo_info.png" 
    ActionIconCommand="{Binding FullnameInfoCommand}"/>
```

# Articles

I wrote articles to explain how I built this library. 
If you’re curious about the backstage, have a look here.

## CraftUI library

- [MAUI (Library Part 1) Create a Custom Entry using SkiaSharp](https://www.stephanarnas.com/posts/maui-create-custom-entry-control-with-border)
- [MAUI (Library Part 2) Info & Error states with FluentValidation](https://www.stephanarnas.com/posts/maui-info-and-error-states-for-entry)
- [MAUI (Library Part 3) Loading state with Picker Label](https://www.stephanarnas.com/posts/maui-loading-state-with-custom-picker)
- [MAUI (Library Part 4) Custom Picker with Collection View and Popup](https://www.stephanarnas.com/posts/maui-custom-picker-with-collection-view-popup)
- [MAUI (Library Part 5) Extending Control Behavior with Button](https://www.stephanarnas.com/posts/maui-extendind-control-behavior-with-button)
- [MAUI (Library Part 6) Custom Button with Progress Bar](https://www.stephanarnas.com/posts/maui-custom-button-with-progress-bar)

## MAUI

- [Upgrade MAUI to .NET 9.0](https://www.stephanarnas.com/posts/upgrade-maui-dotnet-9)
