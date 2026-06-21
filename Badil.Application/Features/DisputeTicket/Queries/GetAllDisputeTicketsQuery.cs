using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.DisputeTicket.DTOs;
using MediatR;

namespace Badil.Application.Features.DisputeTicket.Queries
{
    public class GetAllDisputeTicketsQuery : IRequest<List<DisputeTicketDto>>
    {
    }

    public class GetAllDisputeTicketsQueryHandler : IRequestHandler<GetAllDisputeTicketsQuery, List<DisputeTicketDto>>
    {
        private readonly IDisputeTicketRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDisputeTicketsQueryHandler(IDisputeTicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DisputeTicketDto>> Handle(GetAllDisputeTicketsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<DisputeTicketDto>>(entities);
        }
    }
}
