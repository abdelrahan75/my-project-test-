using Badil.Application.Features.Auth.DTOs;
using MediatR;


namespace Badil.Application.Features.Admin.Commands.CreateAdmin
{
    public class CreateAdminCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
