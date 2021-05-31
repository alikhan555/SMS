using Application.Common.Interfaces;
using FluentValidation;
using System.Linq;

namespace Application.InstituteManagement.Schools.Commands.CreateSchool
{
    public class CreateSchoolCommandValidator : AbstractValidator<CreateSchoolCommand>
    {
        ISMSDbContext _context;

        public CreateSchoolCommandValidator(ISMSDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .Must(IsSchoolNameUnique).WithMessage("School name must be unique.");

            RuleFor(x => x.Initial)
                .NotEmpty()
                .MaximumLength(7)
                .Must(IsSchoolInitialUnique).WithMessage("School initial must be unique.");

            RuleFor(x => x.NTN)
                .NotEmpty()
                .Length(7, 7).WithMessage("NTN must be 7 character")
                .Must(IsSchoolNTNUnique).WithMessage("School NTN. must be unique.");
        }

        public bool IsSchoolNameUnique(string name)
        {
            return !_context.Schools.Any(x => x.Name == name);
        }

        public bool IsSchoolInitialUnique(string initial)
        {
            return !_context.Schools.Any(x => x.Initial == initial);
        }

        public bool IsSchoolNTNUnique(string ntn)
        {
            return !_context.Schools.Any(x => x.NTN == ntn);
        }
    }
}