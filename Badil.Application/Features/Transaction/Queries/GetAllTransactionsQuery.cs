using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Transaction.DTOs;
using MediatR;

namespace Badil.Application.Features.Transaction.Queries
{
    public class GetAllTransactionsQuery : IRequest<List<TransactionDto>>
    {
    }

    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<TransactionDto>>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTransactionsQueryHandler(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<TransactionDto>>(entities);
        }
    }
}
