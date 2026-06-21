using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.ResourceMatch.DTOs;
using MediatR;

namespace Badil.Application.Features.ResourceMatch.Queries
{
    public class GetResourceMatchByIdQuery : IRequest<ResourceMatchDto>
    {
        public Guid Id { get; set; }
    }

    public class GetResourceMatchByIdQueryHandler : IRequestHandler<GetResourceMatchByIdQuery, ResourceMatchDto>
    {
        private readonly IResourceMatchRepository _repository;
        private readonly IMapper _mapper;

        public GetResourceMatchByIdQueryHandler(IResourceMatchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResourceMatchDto> Handle(GetResourceMatchByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Resource match not found");

            return _mapper.Map<ResourceMatchDto>(entity);
        }
    }
}
