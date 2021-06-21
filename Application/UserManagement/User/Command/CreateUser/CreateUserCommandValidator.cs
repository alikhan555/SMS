using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserManagement.User.Command.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            //RuleFor(x => x.Username).NotEmpty();
            //RuleFor(x => x.Email).NotEmpty().EmailAddress();
            //RuleFor(x => x.Roles).Must(roles => roles != null && roles.Count > 0).WithMessage("User must have one or more roles.")
            //    .ForEach(role =>
            //    {
            //        role.NotEmpty().WithMessage("Role must not be empty.");
            //    });
            //RuleFor(x => x.Password).NotEmpty();
        }
    }
}
