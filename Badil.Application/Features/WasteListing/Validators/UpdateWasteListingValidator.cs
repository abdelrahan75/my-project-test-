using Badil.Application.Features.WasteListing.Commands;
using FluentValidation;

namespace Badil.Application.Features.WasteListing.Validators
{
    public class UpdateWasteListingValidator : AbstractValidator<UpdateWasteListingCommand>
    {
        public UpdateWasteListingValidator()
        {
            RuleFor(x => x.MaterialType).NotEmpty().WithMessage("Material type is required");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.SuggestedPrice).GreaterThanOrEqualTo(0).WithMessage("Suggested price cannot be negative");
        }
    }
}
