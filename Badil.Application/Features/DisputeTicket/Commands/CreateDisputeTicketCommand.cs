using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.DisputeTicket.DTOs;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.DisputeTicket.Commands
{
    public class CreateDisputeTicketCommand : IRequest<DisputeTicketDto>
    {
        public Guid TransactionId { get; set; }
        public string Reason { get; set; }
    }

    public class CreateDisputeTicketCommandHandler : IRequestHandler<CreateDisputeTicketCommand, DisputeTicketDto>
    {
        private readonly IDisputeTicketRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateDisputeTicketCommandHandler(IDisputeTicketRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<DisputeTicketDto> Handle(CreateDisputeTicketCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            var entity = new Badil.Domain.Entity.DisputeTicket
            {
                TransactionId = request.TransactionId,
                RaisedByUserId = _currentUserService.UserId.Value,
                Reason = request.Reason,
                Status = DisputeStatus.open
            };

            await _repository.AddAsync(entity, cancellationToken);

            return _mapper.Map<DisputeTicketDto>(entity);
        }
    }
}
