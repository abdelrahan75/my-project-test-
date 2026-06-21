using Badil.Application.Features.Notification.Commands;
using FluentValidation;

namespace Badil.Application.Features.Notification.Validators
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty).WithMessage("UserId is required");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
        }
    }
}
