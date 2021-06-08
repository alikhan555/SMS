using Domain.Entities.Institute;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Campuses.Commands.EditCampus
{
    public class EditCampusCommandValidator : AbstractValidator<EditCampusCommand>
    {
        public EditCampusCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}
