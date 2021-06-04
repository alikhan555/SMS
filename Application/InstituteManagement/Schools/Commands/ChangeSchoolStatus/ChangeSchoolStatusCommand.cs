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
        public string status { get; set; }
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

            switch (request.status)
            {
                case "Active":
                    {
                        school.EntityStatus = EntityStatus.Active;
                        owner.EntityStatus = EntityStatus.Active;
                        break;
                    }
                case "InActive":
                    {
                        school.EntityStatus = EntityStatus.InActive;
                        owner.EntityStatus = EntityStatus.InActive; 
                        break;
                    }
                case "Delete":
                    {
                        school.EntityStatus = EntityStatus.Deleted;
                        owner.EntityStatus = EntityStatus.Deleted;
                        break;
                    }
                default:
                    {
                        return Result<Unit>.Failure(HttpStatus.BadRequest, $"Invalid status value.");
                    }
            }

            // this will also update School Entity
            var identityResult = await _UserManager.UpdateUser(owner);
            //var entityResult = await _context.SaveChangesAsync(cancellationToken);

            if (!identityResult.Succeeded) return Result<Unit>.Failure(HttpStatus.BadRequest, $"Failed to chnage status.");
            //if (entityResult <= 0) return Result<Unit>.Failure(HttpStatus.BadRequest, $"Failed to chnage school status.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}