using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.InstituteManagement.Departments.Commands.CreateDepartment;
using FluentValidation;
using System.Linq;

namespace Application.InstituteManagement.DepartmentNames.Commands.CreateDepartmentName
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        readonly ISMSDbContext _dbContext;
        readonly IUserManager _userManager;

        public CreateDepartmentCommandValidator(ISMSDbContext dbContext, IUserManager userManager)
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