using Badil.Application.Features.VerificationRequest.Commands;
using FluentValidation;

namespace Badil.Application.Features.VerificationRequest.Validators
{
    public class UpdateVerificationRequestValidator : AbstractValidator<UpdateVerificationRequestCommand>
    {
        public UpdateVerificationRequestValidator()
        {
            RuleFor(x => x.Status).IsInEnum().WithMessage("Invalid verification status");
        }
    }
}
