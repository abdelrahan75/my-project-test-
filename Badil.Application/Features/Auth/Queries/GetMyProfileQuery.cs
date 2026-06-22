using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Badil.Application.Features.Auth.Queries
{
    public class GetMyProfileQuery : IRequest<LoginResponse>
    {
    }

    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, LoginResponse>
    {
        private readonly UserManager<Domain.Entity.AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetMyProfileQueryHandler(UserManager<Domain.Entity.AppUser> userManager, IMapper mapper, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<LoginResponse> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId is null)
                throw new UnauthorizedAccessException("User is not authenticated.");
            var user = await _userManager.FindByIdAsync(_currentUserService.UserId.Value.ToString());
            if (user == null)
                throw new KeyNotFoundException("User not found");
            return _mapper.Map<LoginResponse>(user);
        }
    }
}
