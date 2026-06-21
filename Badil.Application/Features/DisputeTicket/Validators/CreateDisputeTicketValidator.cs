using Badil.Application.Features.DisputeTicket.Commands;
using FluentValidation;

namespace Badil.Application.Features.DisputeTicket.Validators
{
    public class CreateDisputeTicketValidator : AbstractValidator<CreateDisputeTicketCommand>
    {
        public CreateDisputeTicketValidator()
        {
            RuleFor(x => x.TransactionId).NotEqual(Guid.Empty).WithMessage("TransactionId is required");
            RuleFor(x => x.Reason).NotEmpty().WithMessage("Reason is required");
        }
    }
}
