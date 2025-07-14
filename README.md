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
Estimated delivery: 08/2025.

# Controls

| MAUI Supported platforms   |
|----------------------------|
| :heavy_check_mark: Android |
| :heavy_check_mark: iOS     |

CraftUI provides a set of reusable UI controls to accelerate your .NET MAUI development while keeping full control over customization and code ownership.

- [CfButton](#cfbutton)
- [CfEntry](#cfentry)
- [CfPicker](#cfpicker)
- [CfPickerPopup](#cfpickerpopup)
- [CfMultiPickerPopup](#cfmultipickerpopup)
- [CfProgressBar](#cfprogressbar)
- [CfDatePicker](#cfdatepicker)
- [CfCollectionSingleSelectionPopup](#cfcollectionsingleselectionpopup)
- [CfCollectionMultiSelectionPopup](#cfcollectionmultiselectionpopup)

## CfButton

Key properties available in the CfButton:

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

Key properties available in the CfEntry:

- **Label** Provides a text label above the input field
- **Error** Displays validation error messages
- **IsRequired** Indicates if the field requires input
- **Info** Shows additional information text below the input
- **ActionIconSource** Adds an icon button inside the entry
- **ActionIconCommand** Command executed when the action icon is tapped
- **IsLoading** Shows a loading indicator when data is being fetched

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
    ActionIconSource="demo_info.png" 
    ActionIconCommand="{Binding FullnameInfoCommand}"/>
```

## CfPicker

Key properties available in the CfPicker:

- **Label** Provides a text label above the picker field
- **SelectedItem** Two-way binding to the selected item from ItemsSource
- **ItemsSource** The collection of items to display in the picker
- **ItemDisplay** Defines which property of the ItemsSource objects should be displayed
- **SelectionChangedCommand** Command executed when the picker is tapped or an item is selected
- **IsLoading** Shows a loading indicator when data is being fetched

<table>
    <tr>
        <td><img src="https://github.com/user-attachments/assets/12c6f83c-9cf8-4891-9f56-81abc4a89cf1" width="300"/></td>
        <td><img src="https://github.com/user-attachments/assets/572369df-ad78-41d4-999a-2ea657d0e7a9" width="300"/></td>
    </tr>
</table>

```xaml
<controls:CfPicker
    Label="Country"
    SelectedItem="{Binding Country}"
    ItemsSource="{Binding CountriesLoader.Result}" 
    ItemDisplay="{x:Static page:PickerPageViewModel.CountryDisplayProperty}"
    SelectionChangedCommand="{Binding CountrySelectedCommand}"
    IsLoading="{Binding CountriesLoader.ShowLoader}" />
```

## CfPickerPopup

- **Label** Provides a text label above the picker field
- **SelectedItem** Two-way binding to the selected item from ItemsSource
- **ItemsSource** The collection of items to display in the picker
- **ItemDisplay** Defines which property of the ItemsSource objects should be displayed
- **SelectionChangedCommand** Command executed when the picker is tapped or an item is selected
- **IsLoading** Shows a loading indicator when data is being fetched
- **DefaultValue** Sets a default value for the picker when no item is selected

<table>
    <tr>
        <td><img src="https://github.com/user-attachments/assets/c2405fd4-abb7-4475-a0b0-e9b93a0efd94" width="300"/></td>
        <td><img src="https://github.com/user-attachments/assets/78c6451e-8ec1-4706-81fd-277599ba1055" width="300"/></td>
    </tr>
</table>

```xaml
<controls:CfPickerPopup
    Label="Country"
    SelectedItem="{Binding Country}"
    ItemsSource="{Binding CountriesLoader.Result}" 
    ItemDisplay="{x:Static page:PickerPageViewModel.CountryDisplayProperty}"
    SelectionChangedCommand="{Binding CountrySelectedCommand}"
    IsLoading="{Binding CountriesLoader.ShowLoader}"/>
```

## CfMultiPickerPopup

- **Label** Provides a text label above the picker field
- **SelectedItems** The collection of selected items (two-way bindable)
- **ItemsSource** The collection of items to display in the picker
- **ItemDisplay** Defines which property of the ItemsSource objects should be displayed
- **SelectionChangedCommand** Command executed when the picker is tapped or an item is selected
- **IsLoading** Shows a loading indicator when data is being fetched

<table>
    <tr>
        <td><img src="https://github.com/user-attachments/assets/c2405fd4-abb7-4475-a0b0-e9b93a0efd94" width="300"/></td>
        <td><img src="https://github.com/user-attachments/assets/78c6451e-8ec1-4706-81fd-277599ba1055" width="300"/></td>
    </tr>
</table>

```xaml
<controls:CfMultiPickerPopup
    Label="Country"
    SelectedItems="{Binding SelectedCountries, Mode=TwoWay}"
    SelectionChangedCommand="{Binding CountriesChangedCommand}"
    ItemsSource="{Binding CountriesLoader.Result}" 
    ItemDisplay="{x:Static page:PickerPageViewModel.CountryDisplayProperty}"
    IsLoading="{Binding CountriesLoader.ShowLoader}"
    />
```

## CfProgressBar

Key properties available in the CfProgressBar:

- **Progress** Float value between 0.0 and 1.0 that represents the progress level
- **ProgressColor** Defines the color of the progress indicator
- **BaseColor** Sets the background color of the progress bar
- **UseGradient** Boolean flag to enable a gradient effect on the progress indicator
- **GradientColor** Defines the start color for the gradient (when UseGradient is true)
- **RoundCaps** Boolean flag to enable rounded ends on the progress bar
- **UseRange** Boolean flag to display a specific range rather than progress from left to right
- **LowerRangeValue** When UseRange is true, defines the start position of the highlighted range (0-1)
- **UpperRangeValue** When UseRange is true, defines the end position of the highlighted range (0-1)

<table>
    <tr>
        <td><img src="https://github.com/user-attachments/assets/d37dfe66-b00c-4c8e-ad10-4257569212f8" width="300"/></td>
        <td><img src="https://github.com/user-attachments/assets/d2df7fc9-83c4-47eb-8db6-dc5978a3eb08" width="300"/></td>
    </tr>
</table>

```xaml
<controls:CfProgressBar
    Progress="{Binding CurrentProgress}"
    UseGradient="True"
    GradientColor="{StaticResource Primary200}"
    ProgressColor="{StaticResource Primary600}"
    RoundCaps="True"
    HeightRequest="20"
    Style="{StaticResource ProgressBarStyle}" />
```

## CfDatePicker

Key properties available in the CfDatePicker:

- **PlaceHolder**: Text displayed when no date is selected, the default value I used is "/ . / . /".
- **NullableDate**: The selected date that can be null, allowing the field to be cleared.
- **Format**: Defines how the selected date is displayed (e.g., "dd/MM/yyyy").
- **MinimumDate**: Sets the earliest selectable date in the picker.
- **MaximumDate**: Sets the latest selectable date in the picker.
- **ShowClearButton**: Determines whether the clear (X) button is visible to reset the date.

<table>
    <tr>
        <td><img src="https://github.com/user-attachments/assets/b6fdc20c-0ebb-4676-a528-132f1db4b499" width="300"/></td>
        <td><img src="https://github.com/user-attachments/assets/77cbbe66-5384-4c17-ab86-1fef0725388c" width="300"/></td>
    </tr>
</table>

```xaml
<controls:CfDatePicker 
    Label="Select a date (with min/max)"
    NullableDate="{Binding RangeDateNullable}"
    MinimumDate="{Binding MinimumDate}"
    MaximumDate="{Binding MaximumDate}" 
    ShowClearButton="True"
    Info="Pick a date between yesterday dans 30 days ahead."/>
```

## CfCollectionSingleSelectionPopup

CfCollectionSingleSelectionPopup is a custom Popup control that displays a list of selectable items in a bottom-sheet style interface where only one item can be selected at a time.

- **Title** Sets the header text displayed at the top of the popup
- **ItemsSource** The collection of items to display in the popup list
- **SelectedItem** The currently selected item (two-way bindable)
- **ItemDisplay** Defines which property of the items should be displayed as text

> This component forms the foundation for the **CfPickerPopup** control and is used throughout the application to provide consistent selection experiences. Additional implementation examples can be found in the Use Cases section of the Demo app.

## CfCollectionMultiSelectionPopup

CfCollectionSingleSelectionPopup is a custom Popup control that displays a list of selectable items in a bottom-sheet style interface where multiple items can be selected.

- **Title** Sets the header text displayed at the top of the popup
- **ItemsSource** The collection of items to display in the popup list
- **SelectedItems** The collection of selected items (two-way bindable)
- **ItemDisplay** Defines which property of the items should be displayed as text

> This component forms the foundation for the **CfMultiPickerPopup** control and is used throughout the application to provide consistent selection experiences. Additional implementation examples can be found in the Use Cases section of the Demo app.

# Articles

I wrote articles to explain how I built this library. 
If you’re curious about the backstage, have a look below.

## CraftUI library

- [MAUI (Library Part 1) Create a Custom Entry using SkiaSharp](https://www.stephanarnas.com/posts/maui-create-custom-entry-control-with-border)
- [MAUI (Library Part 2) Info & Error states with FluentValidation](https://www.stephanarnas.com/posts/maui-info-and-error-states-for-entry)
- [MAUI (Library Part 3) Loading state with Picker Label](https://www.stephanarnas.com/posts/maui-loading-state-with-custom-picker)
- [MAUI (Library Part 4) Custom Picker with Collection View and Popup](https://www.stephanarnas.com/posts/maui-custom-picker-with-collection-view-popup)
- [MAUI (Library Part 5) Extending Control Behavior with Button](https://www.stephanarnas.com/posts/maui-extendind-control-behavior-with-button)
- [MAUI (Library Part 6) Custom Button with Progress Bar](https://www.stephanarnas.com/posts/maui-custom-button-with-progress-bar)
- [MAUI (CraftUI Part 7) Custom Date Picker Nullable](https://www.stephanarnas.com/posts/maui-date-picker-nullable)
- [MAUI (CraftUI Part 8) Custom Multi Selection Picker](https://www.stephanarnas.com/posts/maui-custom-multi-selection-picker)

## MAUI

- [Upgrade MAUI to .NET 9.0](https://www.stephanarnas.com/posts/upgrade-maui-dotnet-9)
