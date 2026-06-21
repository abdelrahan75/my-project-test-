using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.ResourceMatch.Commands
{
    public class DeleteResourceMatchCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteResourceMatchCommandHandler : IRequestHandler<DeleteResourceMatchCommand>
    {
        private readonly IResourceMatchRepository _repository;

        public DeleteResourceMatchCommandHandler(IResourceMatchRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteResourceMatchCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Resource match not found");

            await _repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
