using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.MaterialRequest.Commands
{
    public class DeleteMaterialRequestCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteMaterialRequestCommandHandler : IRequestHandler<DeleteMaterialRequestCommand>
    {
        private readonly IMaterialRequestRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteMaterialRequestCommandHandler(IMaterialRequestRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DeleteMaterialRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Material request not found");

            if (entity.UserId != _currentUserService.UserId && !_currentUserService.IsAdmin)
                throw new UnauthorizedAccessException("You are not allowed to delete this request.");

            await _repository.DeleteAsync(entity, cancellationToken);
        }
    }
}
