using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagement.User.Command.LoginUser
{
    public class LoginUserCommand : IRequest<Result<UserAuthentication>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<UserAuthentication>>
    {
        private readonly IUserManager _userManager;

        public LoginUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<UserAuthentication>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.LoginUserAsync(request.Username, request.Password);
            if (result.Succeeded) return Result<UserAuthentication>.Success(result.Data); 
            return Result<UserAuthentication>.Failure(HttpStatus.Unauthorized, result.Errors);
        }
    }
}