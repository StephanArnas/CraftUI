using FluentValidation;

namespace CraftUI.Demo.Presentation.Pages.Controls.Entry;

public class EntryPageViewModelValidator : AbstractValidator<EntryPageViewModel>
{
    public static string FullNameProperty => nameof(EntryPageViewModel.FullName);
    public static string EmailProperty => nameof(EntryPageViewModel.Email);

    public EntryPageViewModelValidator()
    {
        RuleFor(x => x.FullName)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
    }
}