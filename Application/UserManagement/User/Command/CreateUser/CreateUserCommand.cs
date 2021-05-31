using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagement.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest<Result<string>>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
    {
        readonly ISMSDbContext _context;
        readonly IUserManager _userManager;
        readonly IDateTime _datetime;

        public CreateUserCommandHandler(ISMSDbContext context, IUserManager userManager, IDateTime datetime)
        {
            _context = context;
            _userManager = userManager;
            _datetime = datetime;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser()
            {
                UserName = request.Username,
                Email = request.Email,
                EntityStatus = EntityStatus.Active,
                Created = _datetime.UtcNow,
                CreatedBy = "not configurerd"
            };

            var result = await _userManager.CreateUserAsync(user, request.Password, request.Roles);
            
            if (result.Errors != null && result.Errors.Count() > 0) 
                return Result<string>.Failure(HttpStatus.BadRequest, result.Errors);

            return Result<string>.Success(result.Data);
        }
    }

}