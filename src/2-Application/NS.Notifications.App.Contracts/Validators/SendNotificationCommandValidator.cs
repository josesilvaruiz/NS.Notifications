using FluentValidation;
using NS.Notifications.App.Contracts.Commands;

namespace NS.Notifications.App.Contracts.Validators;

public class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.Message).NotEmpty().WithMessage("Message is required.");
        RuleFor(x => x.Channel).IsInEnum().WithMessage("Invalid notification channel.");
    }
}
