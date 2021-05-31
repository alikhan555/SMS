using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Schools.Commands.EditSchool
{
    public class EditSchoolCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Initial { get; set; }

        public string NTN { get; set; }
    }

    public class EditSchoolCommandHandler : IRequestHandler<EditSchoolCommand, Result<Unit>>
    {
        ISMSDbContext _context { get; }

        public EditSchoolCommandHandler(ISMSDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(EditSchoolCommand request, CancellationToken cancellationToken)
        {
            var school = await _context.Schools.FindAsync(request.Id);

            if (school == null) return Result<Unit>.Failure(HttpStatus.NotFound, $"School with id: {request.Id} not found.");

            //if(_context.Schools.Any(x => x.Id != request.Id && x.Name == request.Name)) 
            //    return Result<Unit>.Failure(HttpStatus.BadRequest, $"School Name must be unique.");

            //if (_context.Schools.Any(x => x.Id != request.Id && x.Name == request.Name))
            //    return Result<Unit>.Failure(HttpStatus.BadRequest, $"School initial must be unique.");

            //if (_context.Schools.Any(x => x.Id != request.Id && x.Name == request.Name))
            //    return Result<Unit>.Failure(HttpStatus.BadRequest, $"School NTN. must be unique.");

            school.Name = request.Name;
            school.Initial = request.Initial;
            school.NTN = request.NTN;

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result <= 0) return Result<Unit>.Failure(HttpStatus.BadRequest, $"Failed to update the school.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}