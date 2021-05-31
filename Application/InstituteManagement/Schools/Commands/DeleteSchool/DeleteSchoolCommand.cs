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

namespace Application.InstituteManagement.Schools.Commands.DeleteSchool
{
    public class DeleteSchoolCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
    }

    class DeleteSchoolCommandHandler : IRequestHandler<DeleteSchoolCommand, Result<Unit>>
    {
        ISMSDbContext _context;

        public DeleteSchoolCommandHandler(ISMSDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(DeleteSchoolCommand request, CancellationToken cancellationToken)
        {
            var school = await _context.Schools.FindAsync(request.Id);

            if (school == null) return Result<Unit>.Failure(HttpStatus.NotFound, $"School with Id {request.Id} not found.");

            school.EntityStatus = EntityStatus.Deleted;

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result <= 0) return Result<Unit>.Failure(HttpStatus.BadRequest, $"Failed to delete the school.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
