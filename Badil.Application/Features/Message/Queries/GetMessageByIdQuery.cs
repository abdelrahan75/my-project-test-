using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Message.DTOs;
using MediatR;

namespace Badil.Application.Features.Message.Queries
{
    public class GetMessageByIdQuery : IRequest<MessageDto>
    {
        public Guid Id { get; set; }
    }

    public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, MessageDto>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;

        public GetMessageByIdQueryHandler(IMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MessageDto> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Message not found");

            return _mapper.Map<MessageDto>(entity);
        }
    }
}
