using Badil.Application.Features.Message.Commands;
using FluentValidation;

namespace Badil.Application.Features.Message.Validators
{
    public class UpdateMessageValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageValidator()
        {
            RuleFor(x => x.IsRead).NotNull().WithMessage("Read status is required");
        }
    }
}
