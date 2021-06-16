using Application.Common.Interfaces;
using FluentValidation;

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
                .MaximumLength(25);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}