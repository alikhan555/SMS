using Application.Common.Enums;
using Application.UserManagement.User.Command.CreateUser;
using Application.UserManagement.User.Command.LoginUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.API.Controllers.User
{
    public class AccountController : BaseApiController
    {
        [HttpPost("Register")]
        [Authorize(Roles = Role.SchoolOwner +","+ Role.Director)]
        public async Task<IActionResult> Register(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
        {
            return ResultHandler(await Mediator.Send(command, cancellationToken));
        }
    }
}
