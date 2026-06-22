using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Message.DTOs;
using MediatR;

namespace Badil.Application.Features.Message.Queries
{
    public class GetConversationQuery : IRequest<List<MessageDto>>
    {
        public Guid OtherUserId { get; set; }
    }

    public class GetConversationQueryHandler : IRequestHandler<GetConversationQuery, List<MessageDto>>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetConversationQueryHandler(IMessageRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<MessageDto>> Handle(GetConversationQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");
            var all = await _repository.GetAllAsync(cancellationToken);
            var conversation = all.Where(m =>
                (m.SenderId == _currentUserService.UserId && m.ReceiverId == request.OtherUserId) ||
                (m.SenderId == request.OtherUserId && m.ReceiverId == _currentUserService.UserId))
                .OrderBy(m => m.CreatedAt)
                .ToList();
            return _mapper.Map<List<MessageDto>>(conversation);
        }
    }
}
