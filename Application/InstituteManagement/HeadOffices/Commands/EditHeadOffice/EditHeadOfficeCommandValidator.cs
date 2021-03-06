using Application.Common.Enums;
using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.HeadOffices.Commands.EditHeadOffice
{
    public class EditHeadOfficeCommandValidator : AbstractValidator<EditHeadOfficeCommand>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public EditHeadOfficeCommandValidator(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;

            RuleFor(x => x.Address)
                .NotEmpty();

            RuleFor(x => x.Contact1)
                .MaximumLength(15);

            RuleFor(x => x.Contact2)
                .MaximumLength(15);

            RuleFor(x => x.Contact3)
                .MaximumLength(15);

            RuleFor(x => x.CampusId)
                .NotEmpty()
                .Must(IsCampusIdValid).WithMessage("Campus Id is not valid.")
                .When(x => x.IsAtCampus);
        }

        public bool IsCampusIdValid(int? campusId)
        {
            return _dbContext.Campus.Any(x => x.SchoolId == _userManager.GetCurrentSchoolId() && x.EntityStatus == EntityStatus.Active && x.Id == campusId);
        }
    }
}
