using Badil.Application.Features.Notification.Commands;
using FluentValidation;

namespace Badil.Application.Features.Notification.Validators
{
    public class UpdateNotificationValidator : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationValidator()
        {
            RuleFor(x => x.IsRead).NotNull().WithMessage("Read status is required");
        }
    }
}
