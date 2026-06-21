using Badil.Application.Features.Transaction.Commands;
using FluentValidation;

namespace Badil.Application.Features.Transaction.Validators
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.ListingId).NotEqual(Guid.Empty).WithMessage("ListingId is required");
            RuleFor(x => x.SellerId).NotEqual(Guid.Empty).WithMessage("SellerId is required");
            RuleFor(x => x.AgreedPrice).GreaterThan(0).WithMessage("Agreed price must be greater than 0");
        }
    }
}
