using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.DisputeTicket.Commands
{
    public class DeleteDisputeTicketCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteDisputeTicketCommandHandler : IRequestHandler<DeleteDisputeTicketCommand>
    {
        private readonly IDisputeTicketRepository _repository;

        public DeleteDisputeTicketCommandHandler(IDisputeTicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteDisputeTicketCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Dispute ticket not found");

            await _repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
