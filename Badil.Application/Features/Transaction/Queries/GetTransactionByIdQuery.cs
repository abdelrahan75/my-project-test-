using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Transaction.DTOs;
using MediatR;

namespace Badil.Application.Features.Transaction.Queries
{
    public class GetTransactionByIdQuery : IRequest<TransactionDto>
    {
        public Guid Id { get; set; }
    }

    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Transaction not found");

            return _mapper.Map<TransactionDto>(entity);
        }
    }
}
