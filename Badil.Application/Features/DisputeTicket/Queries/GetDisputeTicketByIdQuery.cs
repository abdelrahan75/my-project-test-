using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.DisputeTicket.DTOs;
using MediatR;

namespace Badil.Application.Features.DisputeTicket.Queries
{
    public class GetDisputeTicketByIdQuery : IRequest<DisputeTicketDto>
    {
        public Guid Id { get; set; }
    }

    public class GetDisputeTicketByIdQueryHandler : IRequestHandler<GetDisputeTicketByIdQuery, DisputeTicketDto>
    {
        private readonly IDisputeTicketRepository _repository;
        private readonly IMapper _mapper;

        public GetDisputeTicketByIdQueryHandler(IDisputeTicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DisputeTicketDto> Handle(GetDisputeTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Dispute ticket not found");

            return _mapper.Map<DisputeTicketDto>(entity);
        }
    }
}
