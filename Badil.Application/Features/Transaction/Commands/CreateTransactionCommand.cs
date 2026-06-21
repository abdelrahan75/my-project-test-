using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Transaction.DTOs;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.Transaction.Commands
{
    public class CreateTransactionCommand : IRequest<TransactionDto>
    {
        public Guid ListingId { get; set; }
        public Guid SellerId { get; set; }
        public decimal AgreedPrice { get; set; }
        public bool IsSampleRequest { get; set; }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateTransactionCommandHandler(ITransactionRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            var entity = new Badil.Domain.Entity.Transaction
            {
                ListingId = request.ListingId,
                BuyerId = _currentUserService.UserId.Value,
                SellerId = request.SellerId,
                AgreedPrice = request.AgreedPrice,
                IsSampleRequest = request.IsSampleRequest,
                EscrowState = EscrowStatus.AwaitingDeposit
            };

            await _repository.AddAsync(entity, cancellationToken);

            return _mapper.Map<TransactionDto>(entity);
        }
    }
}
