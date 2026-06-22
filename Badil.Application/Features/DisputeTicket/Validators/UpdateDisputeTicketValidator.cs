using Badil.Application.Features.DisputeTicket.Commands;
using FluentValidation;

namespace Badil.Application.Features.DisputeTicket.Validators
{
    public class UpdateDisputeTicketValidator : AbstractValidator<UpdateDisputeTicketCommand>
    {
        public UpdateDisputeTicketValidator()
        {
            RuleFor(x => x.AdminResolutionRemarks).NotEmpty().WithMessage("Resolution remarks are required");
        }
    }
}
