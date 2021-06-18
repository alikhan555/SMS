using Application.Common.Interfaces;
using Application.InstituteManagement.Cohorts.Commands.EditCohort;
using FluentValidation;
using System.Linq;

namespace Application.UserManagement.Cohorts.Commands.EditCohort
{
    public class EditCohortCommandValidator : AbstractValidator<EditCohortCommand>
    {
        ISMSDbContext _context;
        IUserManager _userManager;

        public EditCohortCommandValidator(IUserManager userManager, ISMSDbContext context)
        {
            _userManager = userManager;
            _context = context;
        
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(25)
                .Must(IsCohortNameUnique).WithMessage("Cohort name must be unique.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Initial)
                .NotEmpty()
                .MaximumLength(7)
                .Must(IsCohortInitialUnique).WithMessage("Cohort initial must be unique.");
        }

        public bool IsCohortNameUnique(EditCohortCommand request, string name)
        {
            return !_context.Cohort.Any(x =>
                x.SchoolId == _userManager.GetCurrentSchoolId() &&
                x.Name == name &&
                x.Id != request.Id);
        }

        public bool IsCohortInitialUnique(EditCohortCommand request, string initial)
        {
            return !_context.Cohort.Any(x =>
                x.SchoolId == _userManager.GetCurrentSchoolId() &&
                x.Initial == initial &&
                x.Id != request.Id);
        }
    }
}