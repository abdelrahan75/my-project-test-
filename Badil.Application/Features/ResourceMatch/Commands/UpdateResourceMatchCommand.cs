using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.ResourceMatch.Commands
{
    public class UpdateResourceMatchCommand : IRequest
    {
        public Guid Id { get; set; }
        public double SemanticCompatibilityScore { get; set; }
        public double DistanceKm { get; set; }
    }

    public class UpdateResourceMatchCommandHandler : IRequestHandler<UpdateResourceMatchCommand>
    {
        private readonly IResourceMatchRepository _repository;

        public UpdateResourceMatchCommandHandler(IResourceMatchRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateResourceMatchCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Resource match not found");

            entity.SemanticCompatibilityScore = request.SemanticCompatibilityScore;
            entity.DistanceKm = request.DistanceKm;

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
