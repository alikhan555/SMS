using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Schools.Commands.EditSchool
{
    public class EditSchoolCommandValidator : AbstractValidator<EditSchoolCommand>
    {
        ISMSDbContext _context;
        IUserManager _userManager;

        public EditSchoolCommandValidator(ISMSDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;

            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .Must(IsSchoolNameUnique).WithMessage("School name must be unique.");

            RuleFor(x => x.Initial)
                .NotEmpty()
                .MaximumLength(7)
                .Must(IsSchoolInitialUnique).WithMessage("School initial must be unique.");

            RuleFor(x => x.NTN)
                .NotEmpty()
                .Length(7, 7).WithMessage("NTN must be 7 character")
                .Must(IsSchoolNTNUnique).WithMessage("School NTN. must be unique.");

            RuleFor(x => x.OwnerEmail)
                .NotEmpty()
                .EmailAddress()
                .Must(IsOwnerEmailUnique).WithMessage("Email address is already taken.");
            _userManager = userManager;
        }

        public bool IsSchoolNameUnique(EditSchoolCommand model, string name)
        {
            return !_context.Schools.Any(x => x.Id != model.Id && x.Name == name);
        }

        public bool IsSchoolInitialUnique(EditSchoolCommand model, string initial)
        {
            return !_context.Schools.Any(x => x.Id != model.Id && x.Initial == initial);
        }

        public bool IsSchoolNTNUnique(EditSchoolCommand model, string ntn)
        {
            return !_context.Schools.Any(x => x.Id != model.Id && x.NTN == ntn);
        }

        public bool IsOwnerEmailUnique(EditSchoolCommand model, string ownerEmail)
        {
            var user = _userManager.GetUserByName(ownerEmail).GetAwaiter().GetResult();

            if (user == null)
            {
                return true;
            }
            else if (user.SchoolId == model.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}