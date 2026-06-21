using Badil.Application.Common.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badil.Application.Features.Admin.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler
    : IRequestHandler<UpdateUserRoleCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserRoleCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(
            UpdateUserRoleCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                throw new Exception("User not found");

            user.Role = request.NewRole;

            await _userRepository.UpdateAsync(user, cancellationToken);
        }
    }
}

