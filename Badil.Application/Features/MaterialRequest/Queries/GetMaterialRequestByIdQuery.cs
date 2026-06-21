using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.MaterialRequest.DTOs;
using MediatR;

namespace Badil.Application.Features.MaterialRequest.Queries
{
    public class GetMaterialRequestByIdQuery : IRequest<MaterialRequestDto>
    {
        public Guid Id { get; set; }
    }

    public class GetMaterialRequestByIdQueryHandler : IRequestHandler<GetMaterialRequestByIdQuery, MaterialRequestDto>
    {
        private readonly IMaterialRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetMaterialRequestByIdQueryHandler(IMaterialRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MaterialRequestDto> Handle(GetMaterialRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Material request not found");

            return _mapper.Map<MaterialRequestDto>(entity);
        }
    }
}
