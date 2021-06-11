
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Campuses.Commands.CreateCampus
{
    public class CreateCampusCommandValidator : AbstractValidator<CreateCampusCommand>
    {
        public CreateCampusCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Address)
                .NotEmpty();
        }
    }
}