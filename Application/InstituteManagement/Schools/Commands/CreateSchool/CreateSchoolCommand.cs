using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Common.Enums;
using Domain.Entities.Institute;

namespace Application.InstituteManagement.Schools.Commands.CreateSchool
{
    public class CreateSchoolCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }

        public string Initial { get; set; }

        public string NTN { get; set; }
    }

    public class CreateSchoolCommandHandler : IRequestHandler<CreateSchoolCommand, Result<int>>
    {
        private ISMSDbContext _context { get; }

        public CreateSchoolCommandHandler(ISMSDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            //List<string> validationErrors = new List<string>();

            //if (_context.Schools.Any(x => x.Name == request.Name))
            //    validationErrors.Add("School Name must be unique.");

            //if (_context.Schools.Any(x => x.Initial == request.Initial))
            //    validationErrors.Add("School initial must be unique.");

            //if (_context.Schools.Any(x => x.NTN == request.NTN))
            //    validationErrors.Add("School NTN. must be unique.");

            //if (validationErrors.Count > 0) 
            //{
            //    return Result<Unit>.Failure(HttpStatus.BadRequest, validationErrors);
            //}
            //else
            //{
            //    var school = new School()
            //    {
            //        Name = request.Name,
            //        Initial = request.Initial,
            //        NTN = request.NTN
            //    };

            //    _context.Schools.Add(school);
            //    var result = await _context.SaveChangesAsync(cancellationToken);

            //    if (result <= 0) return Result<Unit>.Failure(HttpStatus.BadRequest, "Failed to save the school.");

            //    return Result<Unit>.Success(Unit.Value);
            //}

            var school = new School()
            {
                Name = request.Name,
                Initial = request.Initial,
                NTN = request.NTN
            };

            _context.Schools.Add(school);
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result <= 0) return Result<int>.Failure(HttpStatus.BadRequest, "Failed to save the school.");

            return Result<int>.Success(school.Id);
        }
    }
}