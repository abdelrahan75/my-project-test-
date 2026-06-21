using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.VerificationRequest.DTOs;
using MediatR;

namespace Badil.Application.Features.VerificationRequest.Queries
{
    public class GetAllVerificationRequestsQuery : IRequest<List<VerificationRequestDto>>
    {
    }

    public class GetAllVerificationRequestsQueryHandler : IRequestHandler<GetAllVerificationRequestsQuery, List<VerificationRequestDto>>
    {
        private readonly IVerificationRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetAllVerificationRequestsQueryHandler(IVerificationRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<VerificationRequestDto>> Handle(GetAllVerificationRequestsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<VerificationRequestDto>>(entities);
        }
    }
}
