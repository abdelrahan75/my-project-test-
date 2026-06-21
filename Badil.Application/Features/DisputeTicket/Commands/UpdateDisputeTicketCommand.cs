using Badil.Application.Common.Interfaces.Repositories;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.DisputeTicket.Commands
{
    public class UpdateDisputeTicketCommand : IRequest
    {
        public Guid Id { get; set; }
        public DisputeStatus Status { get; set; }
        public string AdminResolutionRemarks { get; set; }
    }

    public class UpdateDisputeTicketCommandHandler : IRequestHandler<UpdateDisputeTicketCommand>
    {
        private readonly IDisputeTicketRepository _repository;

        public UpdateDisputeTicketCommandHandler(IDisputeTicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDisputeTicketCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Dispute ticket not found");

            entity.Status = request.Status;
            entity.AdminResolutionRemarks = request.AdminResolutionRemarks;

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
