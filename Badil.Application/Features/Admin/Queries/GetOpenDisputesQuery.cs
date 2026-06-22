using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.DisputeTicket.DTOs;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.Admin.Queries
{
    public class GetOpenDisputesQuery : IRequest<List<DisputeTicketDto>>
    {
    }

    public class GetOpenDisputesQueryHandler : IRequestHandler<GetOpenDisputesQuery, List<DisputeTicketDto>>
    {
        private readonly IDisputeTicketRepository _repository;
        private readonly IMapper _mapper;

        public GetOpenDisputesQueryHandler(IDisputeTicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DisputeTicketDto>> Handle(GetOpenDisputesQuery request, CancellationToken cancellationToken)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            var open = all.Where(d => d.Status != DisputeStatus.Resolved).ToList();
            return _mapper.Map<List<DisputeTicketDto>>(open);
        }
    }
}
