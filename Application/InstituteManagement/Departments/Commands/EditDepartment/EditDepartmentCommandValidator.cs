using Application.Common.Enums;
using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Departments.Commands.EditDepartment
{
    public class EditDepartmentCommandValidator : AbstractValidator<EditDepartmentCommand>
    {
        readonly ISMSDbContext _dbContext;
        readonly IUserManager _userManager;

        public EditDepartmentCommandValidator(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;

            RuleFor(x => x.DepartmentNameId)
                .Must(IsDepartmentNameIdValid).WithMessage("Department name Id is not valid.");

            RuleFor(x => x.CampusId)
                .Must(IsCampusIdValid).WithMessage("Campus Id is not valid.");
        }

        public bool IsDepartmentNameIdValid(int departmentNameId)
        {
            return _dbContext.DepartmentName.Any(x => x.SchoolId == _userManager.GetCurrentSchoolId() && x.EntityStatus == EntityStatus.Active && x.Id == departmentNameId);
        }

        public bool IsCampusIdValid(int campusId)
        {
            return _dbContext.Campus.Any(x => x.SchoolId == _userManager.GetCurrentSchoolId() && x.EntityStatus == EntityStatus.Active && x.Id == campusId);
        }
    }
}