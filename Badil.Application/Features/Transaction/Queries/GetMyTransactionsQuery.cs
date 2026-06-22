using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Transaction.DTOs;
using MediatR;

namespace Badil.Application.Features.Transaction.Queries
{
    public class GetMyTransactionsQuery : IRequest<List<TransactionDto>>
    {
    }

    public class GetMyTransactionsQueryHandler : IRequestHandler<GetMyTransactionsQuery, List<TransactionDto>>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetMyTransactionsQueryHandler(ITransactionRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<TransactionDto>> Handle(GetMyTransactionsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");
            var all = await _repository.GetAllAsync(cancellationToken);
            var mine = all.Where(t => t.BuyerId == _currentUserService.UserId || t.SellerId == _currentUserService.UserId).ToList();
            return _mapper.Map<List<TransactionDto>>(mine);
        }
    }
}
