using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.DisputeTicket.Commands
{
    public class ResolveDisputeTicketCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Remarks { get; set; }
    }

    public class ResolveDisputeTicketCommandHandler : IRequestHandler<ResolveDisputeTicketCommand>
    {
        private readonly IDisputeTicketRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public ResolveDisputeTicketCommandHandler(IDisputeTicketRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(ResolveDisputeTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (ticket == null)
                throw new KeyNotFoundException("Dispute ticket not found");
            if (!_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("Only admins can resolve disputes");

            ticket.Status = DisputeStatus.Resolved;
            ticket.AdminResolutionRemarks = request.Remarks;
            await _repository.UpdateAsync(ticket, cancellationToken);
        }
    }
}
