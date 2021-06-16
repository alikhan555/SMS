using Application.InstituteManagement.Cohorts.Commands.EditCohort;
using FluentValidation;


namespace Application.UserManagement.Cohorts.Commands.EditCohort
{
    public class EditCohortCommandValidator : AbstractValidator<EditCohortCommand>
    {
        public EditCohortCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}