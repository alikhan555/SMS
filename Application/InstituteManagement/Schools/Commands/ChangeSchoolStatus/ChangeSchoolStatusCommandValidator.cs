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
                .Must(x => x == "Active" || x == "InActive" || x == "Deleted").WithMessage("Invalid status value.");
        }

        //public bool IsValidStatus(string status)
        //{

        //}
    }
}
