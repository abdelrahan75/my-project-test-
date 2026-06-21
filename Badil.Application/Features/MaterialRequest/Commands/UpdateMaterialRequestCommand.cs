using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.MaterialRequest.Commands
{
    public class UpdateMaterialRequestCommand : IRequest
    {
        public Guid Id { get; set; }
        public string MaterialType { get; set; }
        public double TargetQuantity { get; set; }
        public double LocationPreferenceRadiusKm { get; set; }
    }

    public class UpdateMaterialRequestCommandHandler : IRequestHandler<UpdateMaterialRequestCommand>
    {
        private readonly IMaterialRequestRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateMaterialRequestCommandHandler(IMaterialRequestRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(UpdateMaterialRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Material request not found");

            if (entity.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to modify this request.");

            entity.MaterialType = request.MaterialType;
            entity.TargetQuantity = request.TargetQuantity;
            entity.LocationPreferenceRadiusKm = request.LocationPreferenceRadiusKm;

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
