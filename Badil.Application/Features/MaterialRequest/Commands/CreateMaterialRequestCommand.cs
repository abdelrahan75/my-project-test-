using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.MaterialRequest.DTOs;
using MediatR;

namespace Badil.Application.Features.MaterialRequest.Commands
{
    public class CreateMaterialRequestCommand : IRequest<MaterialRequestDto>
    {
        public string MaterialType { get; set; }
        public double TargetQuantity { get; set; }
        public double LocationPreferenceRadiusKm { get; set; }
    }

    public class CreateMaterialRequestCommandHandler : IRequestHandler<CreateMaterialRequestCommand, MaterialRequestDto>
    {
        private readonly IMaterialRequestRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateMaterialRequestCommandHandler(IMaterialRequestRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<MaterialRequestDto> Handle(CreateMaterialRequestCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            var entity = new Badil.Domain.Entity.MaterialRequest
            {
                UserId = _currentUserService.UserId.Value,
                MaterialType = request.MaterialType,
                TargetQuantity = request.TargetQuantity,
                LocationPreferenceRadiusKm = request.LocationPreferenceRadiusKm
            };

            await _repository.AddAsync(entity, cancellationToken);

            return _mapper.Map<MaterialRequestDto>(entity);
        }
    }
}
