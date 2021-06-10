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

namespace Application.InstituteManagement.Schools.Commands.ChangeSchoolStatus
{
    public class ChangeSchoolStatusCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public int status { get; set; }
    }

    class ChangeSchoolStatusCommandHandler : IRequestHandler<ChangeSchoolStatusCommand, Result<Unit>>
    {
        ISMSDbContext _context;
        IUserManager _UserManager;

        public ChangeSchoolStatusCommandHandler(ISMSDbContext context, IUserManager userManager)
        {
            _context = context;
            _UserManager = userManager;
        }

        public async Task<Result<Unit>> Handle(ChangeSchoolStatusCommand request, CancellationToken cancellationToken)
        {
            var school = await _context.Schools.FindAsync(request.Id);
            if (school == null) return Result<Unit>.Failure(HttpStatus.NotFound, $"School with Id {request.Id} not found.");

            var owner = await _UserManager.GetUserById(school.OwnerId);
            if (owner == null) return Result<Unit>.Failure(HttpStatus.NotFound, $"School owner not found.");

            school.EntityStatus = request.status;
            owner.EntityStatus = request.status;

            // this will also update School Entity
            var identityResult = await _UserManager.UpdateUser(owner);

            if (!identityResult.Succeeded) return Result<Unit>.Failure(HttpStatus.BadRequest, $"Failed to chnage status.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}