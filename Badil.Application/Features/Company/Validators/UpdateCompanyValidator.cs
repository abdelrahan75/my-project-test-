using Badil.Application.Features.Company.Commands;
using FluentValidation;

namespace Badil.Application.Features.Company.Validators
{
    public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Company name is required");
            RuleFor(x => x.Sector).NotEmpty().WithMessage("Sector is required");
        }
    }
}
