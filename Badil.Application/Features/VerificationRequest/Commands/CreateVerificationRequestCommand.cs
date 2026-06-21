using AutoMapper;
using Badil.Application.Common.Interfaces.Repositories;
using Badil.Application.Features.VerificationRequest.DTOs;
using Badil.Domain.Enum;
using MediatR;

namespace Badil.Application.Features.VerificationRequest.Commands
{
    public class CreateVerificationRequestCommand : IRequest<VerificationRequestDto>
    {
        public Guid CompanyId { get; set; }
        public List<string> DocumentUrls { get; set; } = new();
    }

    public class CreateVerificationRequestCommandHandler : IRequestHandler<CreateVerificationRequestCommand, VerificationRequestDto>
    {
        private readonly IVerificationRequestRepository _repository;
        private readonly IMapper _mapper;

        public CreateVerificationRequestCommandHandler(IVerificationRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VerificationRequestDto> Handle(CreateVerificationRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = new Badil.Domain.Entity.VerificationRequest
            {
                CompanyId = request.CompanyId,
                DocumentUrls = request.DocumentUrls,
                Status = VerificationStatus.Pending
            };

            await _repository.AddAsync(entity, cancellationToken);

            return _mapper.Map<VerificationRequestDto>(entity);
        }
    }
}
