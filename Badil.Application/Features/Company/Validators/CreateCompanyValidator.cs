using Badil.Application.Features.Company.Commands;
using FluentValidation;

namespace Badil.Application.Features.Company.Validators
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Company name is required");
            RuleFor(x => x.Sector).NotEmpty().WithMessage("Sector is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90");
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180");
            RuleFor(x => x.TaxRegistrationNumber).NotEmpty().WithMessage("Tax registration number is required");
            RuleFor(x => x.CommercialRegisterNumber).NotEmpty().WithMessage("Commercial register number is required");
        }
    }
}
