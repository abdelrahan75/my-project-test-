using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.ResourceMatch.DTOs;
using MediatR;

namespace Badil.Application.Features.ResourceMatch.Commands
{
    public class CreateResourceMatchCommand : IRequest<ResourceMatchDto>
    {
        public Guid ListingId { get; set; }
        public Guid RequestId { get; set; }
        public double SemanticCompatibilityScore { get; set; }
        public double DistanceKm { get; set; }
    }

    public class CreateResourceMatchCommandHandler : IRequestHandler<CreateResourceMatchCommand, ResourceMatchDto>
    {
        private readonly IResourceMatchRepository _repository;
        private readonly IMapper _mapper;

        public CreateResourceMatchCommandHandler(IResourceMatchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResourceMatchDto> Handle(CreateResourceMatchCommand request, CancellationToken cancellationToken)
        {
            var entity = new Badil.Domain.Entity.ResourceMatch
            {
                ListingId = request.ListingId,
                RequestId = request.RequestId,
                SemanticCompatibilityScore = request.SemanticCompatibilityScore,
                DistanceKm = request.DistanceKm
            };

            await _repository.AddAsync(entity, cancellationToken);

            return _mapper.Map<ResourceMatchDto>(entity);
        }
    }
}
