using Application.Common.Enums;
using FluentValidation;

namespace Application.UserManagement.Cohorts.Commands.ChangeCohortStatus
{
    public class ChangeCohortStatusCommandValidator : AbstractValidator<ChangeCohortStatusCommand>
    {
        public ChangeCohortStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .Must(x => x == EntityStatus.Active || x == EntityStatus.InActive || x == EntityStatus.Deleted).WithMessage("Invalid status value.");
        }
    }
}