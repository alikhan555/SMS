using Application.Common.Enums;
using Application.InstituteManagement.Campuses.Commands.ChangeCampusStatus;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Campuses.Commands
{
    public class ChangeCampusStatusCommandValidator : AbstractValidator<ChangeCampusStatusCommand>
    {
        public ChangeCampusStatusCommandValidator()
        {
            //RuleFor(x => x.Status)
            //.Must(x => x == EntityStatus.Active || x == EntityStatus.InActive || x == EntityStatus.Deleted).WithMessage("Invalid status value.");
        }
    }
}
