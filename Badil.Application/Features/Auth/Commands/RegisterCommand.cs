using AutoMapper;
using Badil.Application.Common.Interfaces;
using Badil.Application.Features.Auth.DTOs;
using Badil.Domain.Entity;
using Badil.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Badil.Application.Features.Auth.Commands
{
    public class RegisterCommand : RegisterRequest, IRequest<LoginResponse>
    {
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, LoginResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public RegisterCommandHandler(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AppUser>(request);
            user.UserName = request.Email;
            user.IsActive = true;
            user.Role = UserRole.User;

            var result = await _userManager.CreateAsync(user, request.Password!);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User registration failed: {errors}");
            }

            var token = _tokenService.GenerateJwtToken(user);
            
            var response = _mapper.Map<LoginResponse>(user);
            response.Token = token;

            return response;
        }
    }
}
