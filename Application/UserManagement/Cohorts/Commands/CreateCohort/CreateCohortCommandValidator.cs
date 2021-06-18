using Application.Common.Enums;
using Application.Common.Interfaces;
using FluentValidation;
using System.Linq;

namespace Application.UserManagement.Cohorts.Commands.CreateCohort
{
    public class CreateCohortCommandValidator : AbstractValidator<CreateCohortCommand>
    {
        ISMSDbContext _context;
        IUserManager _userManager;

        public CreateCohortCommandValidator(ISMSDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;

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
        
        public bool IsCohortNameUnique(string name)
        {
            return !_context.Cohort.Any(x =>
                x.SchoolId == _userManager.GetCurrentSchoolId() &&
                x.Name == name);
        }

        public bool IsCohortInitialUnique(string initial)
        {
            return !_context.Cohort.Any(x => 
                x.SchoolId == _userManager.GetCurrentSchoolId() &&
                x.Initial == initial);
        }
    }
}