using Application.Common.Enums;
using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.DepartmentNames.Commands.EditDepartmentName
{
    public class EditDepartmentNameCommandValidator : AbstractValidator<EditDepartmentNameCommand>
    {
        readonly ISMSDbContext _dbContext;
        readonly IUserManager _userManager;

        public EditDepartmentNameCommandValidator(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;

            RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(50);

            RuleFor(x => x.Initial)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}