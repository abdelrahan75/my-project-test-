using Badil.Application.Features.MaterialRequest.Commands;
using FluentValidation;

namespace Badil.Application.Features.MaterialRequest.Validators
{
    public class UpdateMaterialRequestValidator : AbstractValidator<UpdateMaterialRequestCommand>
    {
        public UpdateMaterialRequestValidator()
        {
            RuleFor(x => x.MaterialType).NotEmpty().WithMessage("Material type is required");
            RuleFor(x => x.TargetQuantity).GreaterThan(0).WithMessage("Target quantity must be greater than 0");
        }
    }
}
