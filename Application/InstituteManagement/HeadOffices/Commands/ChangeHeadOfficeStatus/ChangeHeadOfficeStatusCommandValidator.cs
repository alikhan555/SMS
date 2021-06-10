using Application.Common.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.HeadOffices.Commands.ChangeHeadOfficeStatus
{
    public class ChangeHeadOfficeStatusCommandValidator : AbstractValidator<ChangeHeadOfficeStatusCommand>
    {
        public ChangeHeadOfficeStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .Must(x => x == EntityStatus.Active || x == EntityStatus.InActive || x == EntityStatus.Deleted).WithMessage("Invalid status value.");
        }
    }
}
