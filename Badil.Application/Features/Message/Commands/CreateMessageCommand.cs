using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Message.DTOs;
using MediatR;

namespace Badil.Application.Features.Message.Commands
{
    public class CreateMessageCommand : IRequest<MessageDto>
    {
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
    }

    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageDto>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateMessageCommandHandler(IMessageRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<MessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");

            var entity = new Badil.Domain.Entity.Message
            {
                SenderId = _currentUserService.UserId.Value,
                ReceiverId = request.ReceiverId,
                Content = request.Content,
                IsRead = false
            };

            await _repository.AddAsync(entity, cancellationToken);

            return _mapper.Map<MessageDto>(entity);
        }
    }
}
