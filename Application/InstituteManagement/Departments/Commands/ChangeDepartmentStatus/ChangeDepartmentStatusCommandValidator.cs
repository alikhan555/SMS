using Application.Common.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Departments.Commands.ChangeDepartmentStatus
{
    public class ChangeDepartmentStatusCommandValidator : AbstractValidator<ChangeDepartmentStatusCommand>
    {
        public ChangeDepartmentStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .Must(x => x == EntityStatus.Active || x == EntityStatus.InActive || x == EntityStatus.Deleted).WithMessage("Invalid status value.");
        }
    }
}
