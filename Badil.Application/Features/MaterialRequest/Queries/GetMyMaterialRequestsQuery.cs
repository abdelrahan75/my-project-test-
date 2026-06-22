using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.MaterialRequest.DTOs;
using MediatR;

namespace Badil.Application.Features.MaterialRequest.Queries
{
    public class GetMyMaterialRequestsQuery : IRequest<List<MaterialRequestDto>>
    {
    }

    public class GetMyMaterialRequestsQueryHandler : IRequestHandler<GetMyMaterialRequestsQuery, List<MaterialRequestDto>>
    {
        private readonly IMaterialRequestRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetMyMaterialRequestsQueryHandler(IMaterialRequestRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<MaterialRequestDto>> Handle(GetMyMaterialRequestsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");
            var all = await _repository.GetAllAsync(cancellationToken);
            var mine = all.Where(r => r.UserId == _currentUserService.UserId).ToList();
            return _mapper.Map<List<MaterialRequestDto>>(mine);
        }
    }
}
