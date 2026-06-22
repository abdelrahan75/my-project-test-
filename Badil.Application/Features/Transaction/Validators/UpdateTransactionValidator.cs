using Badil.Application.Features.Transaction.Commands;
using FluentValidation;

namespace Badil.Application.Features.Transaction.Validators
{
    public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionCommand>
    {
        public UpdateTransactionValidator()
        {
            RuleFor(x => x.AgreedPrice).GreaterThan(0).WithMessage("Agreed price must be greater than 0");
        }
    }
}
