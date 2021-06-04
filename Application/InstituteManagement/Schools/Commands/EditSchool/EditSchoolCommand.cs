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

        public string OwnerEmail { get; set; }
    }

    public class EditSchoolCommandHandler : IRequestHandler<EditSchoolCommand, Result<Unit>>
    {
        ISMSDbContext _context { get; }
        IUserManager _userManager { get; }

        public EditSchoolCommandHandler(ISMSDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(EditSchoolCommand request, CancellationToken cancellationToken)
        {
            var school = _context.Schools
                .Where(x => x.EntityStatus != EntityStatus.Deleted)
                .SingleOrDefault(x => x.Id == request.Id);

            if (school == null) return Result<Unit>.Failure(HttpStatus.NotFound, $"School with id: {request.Id} not found.");

            school.Name = request.Name;
            school.Initial = request.Initial;
            school.NTN = request.NTN;

            var owner = await _userManager.GetUserById(school.OwnerId);

            if (owner.Email != request.OwnerEmail)
            {
                owner.Email = request.OwnerEmail;
                owner.UserName = request.OwnerEmail;

                var identityResult = await _userManager.UpdateUser(owner);

                if (!identityResult.Succeeded) return Result<Unit>.Failure(identityResult.Errors);
            }

            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result <= 0) return Result<Unit>.Failure(HttpStatus.BadRequest, $"Failed to update the school.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}