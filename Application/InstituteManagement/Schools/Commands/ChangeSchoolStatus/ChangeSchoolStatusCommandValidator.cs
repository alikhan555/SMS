using Application.Common.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Schools.Commands.ChangeSchoolStatus
{
    public class ChangeSchoolStatusCommandValidator : AbstractValidator<ChangeSchoolStatusCommand>
    {
        public ChangeSchoolStatusCommandValidator()
        {
            RuleFor(x => x.status)
                .NotEmpty()
                .Must(x => x == EntityStatus.Active || x == EntityStatus.InActive || x == EntityStatus.Deleted).WithMessage("Invalid status value.");
        }
    }
}
