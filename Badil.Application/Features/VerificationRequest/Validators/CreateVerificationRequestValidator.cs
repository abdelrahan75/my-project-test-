using Badil.Application.Features.VerificationRequest.Commands;
using FluentValidation;

namespace Badil.Application.Features.VerificationRequest.Validators
{
    public class CreateVerificationRequestValidator : AbstractValidator<CreateVerificationRequestCommand>
    {
        public CreateVerificationRequestValidator()
        {
            RuleFor(x => x.CompanyId).NotEqual(Guid.Empty).WithMessage("CompanyId is required");
            RuleFor(x => x.DocumentUrls).NotEmpty().WithMessage("At least one document is required");
        }
    }
}
