using Badil.Application.Features.Admin.Commands.CreateAdmin;
using Badil.Application.Features.Admin.Commands.DeleteUser;
using Badil.Application.Features.Admin.Commands.UpdateUserRole;
using Badil.Application.Features.Admin.Queries.GetAdminDashboard;
using Badil.Application.Features.Admin.Queries.GetAllUsers;
using Badil.Application.Features.Admin.Queries;
using Badil.Application.Features.Auth.Commands;
using Badil.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Badil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("users/{id}/role")]
        public async Task<IActionResult> UpdateUserRole(Guid id, [FromBody] UpdateUserRoleCommand command)
        {
            try
            {
                command.UserId = id;
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteUserCommand { UserId = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var dashboard = await _mediator.Send(new GetAdminDashboardQuery());
            return Ok(dashboard);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("verification-requests")]
        public async Task<IActionResult> GetPendingVerifications()
        {
            var result = await _mediator.Send(new GetPendingVerificationsQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("disputes")]
        public async Task<IActionResult> GetOpenDisputes()
        {
            var result = await _mediator.Send(new GetOpenDisputesQuery());
            return Ok(result);
        }
    }
}
