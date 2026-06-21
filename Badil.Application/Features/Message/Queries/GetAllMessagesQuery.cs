using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.Message.DTOs;
using MediatR;

namespace Badil.Application.Features.Message.Queries
{
    public class GetAllMessagesQuery : IRequest<List<MessageDto>>
    {
    }

    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, List<MessageDto>>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;

        public GetAllMessagesQueryHandler(IMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MessageDto>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<MessageDto>>(entities);
        }
    }
}
