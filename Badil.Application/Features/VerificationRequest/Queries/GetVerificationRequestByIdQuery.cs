using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.VerificationRequest.DTOs;
using MediatR;

namespace Badil.Application.Features.VerificationRequest.Queries
{
    public class GetVerificationRequestByIdQuery : IRequest<VerificationRequestDto>
    {
        public Guid Id { get; set; }
    }

    public class GetVerificationRequestByIdQueryHandler : IRequestHandler<GetVerificationRequestByIdQuery, VerificationRequestDto>
    {
        private readonly IVerificationRequestRepository _repository;
        private readonly IMapper _mapper;

        public GetVerificationRequestByIdQueryHandler(IVerificationRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VerificationRequestDto> Handle(GetVerificationRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException("Verification request not found");

            return _mapper.Map<VerificationRequestDto>(entity);
        }
    }
}
