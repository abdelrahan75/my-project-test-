using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.VerificationRequest.DTOs;
using MediatR;

namespace Badil.Application.Features.VerificationRequest.Queries
{
    public class GetMyVerificationRequestsQuery : IRequest<List<VerificationRequestDto>>
    {
    }

    public class GetMyVerificationRequestsQueryHandler : IRequestHandler<GetMyVerificationRequestsQuery, List<VerificationRequestDto>>
    {
        private readonly IVerificationRequestRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetMyVerificationRequestsQueryHandler(IVerificationRequestRepository repository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<VerificationRequestDto>> Handle(GetMyVerificationRequestsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");
            var all = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<VerificationRequestDto>>(all.ToList());
        }
    }
}
