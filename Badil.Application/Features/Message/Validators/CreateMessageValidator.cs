using Badil.Application.Features.Message.Commands;
using FluentValidation;

namespace Badil.Application.Features.Message.Validators
{
    public class CreateMessageValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageValidator()
        {
            RuleFor(x => x.ReceiverId).NotEqual(Guid.Empty).WithMessage("Receiver is required");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Message content is required");
        }
    }
}
