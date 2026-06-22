using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.VerificationRequest.DTOs;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.Admin.Queries
{
    public class GetPendingVerificationsQuery : IRequest<List<VerificationRequestDto>>
    {
    }

    public class GetPendingVerificationsQueryHandler : IRequestHandler<GetPendingVerificationsQuery, List<VerificationRequestDto>>
    {
        private readonly IVerificationRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetPendingVerificationsQueryHandler(IVerificationRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<VerificationRequestDto>> Handle(GetPendingVerificationsQuery request, CancellationToken cancellationToken)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            var pending = all.Where(v => v.Status == VerificationStatus.Pending).ToList();
            return _mapper.Map<List<VerificationRequestDto>>(pending);
        }
    }
}
