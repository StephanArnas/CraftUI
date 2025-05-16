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

## Button

CraftUI provides developers with two predefined button styles, ensuring visual consistency while keeping full flexibility for customization.
- FilledPrimaryButton
- PlainPrimaryButton

A convenient IsLoading property is also available to easily manage loading states directly from your view model.

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
