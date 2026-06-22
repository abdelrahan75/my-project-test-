using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using MediatR;

namespace Badil.Application.Features.Message.Queries
{
    public class GetUnreadMessageCountQuery : IRequest<int>
    {
    }

    public class GetUnreadMessageCountQueryHandler : IRequestHandler<GetUnreadMessageCountQuery, int>
    {
        private readonly IMessageRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public GetUnreadMessageCountQueryHandler(IMessageRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(GetUnreadMessageCountQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                return 0;
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Count(m => m.ReceiverId == _currentUserService.UserId && !m.IsRead);
        }
    }
}
