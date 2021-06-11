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

namespace Application.InstituteManagement.Departments.Commands.ChangeDepartmentStatus
{
    public class ChangeDepartmentStatusCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }

    public class ChangeDepartmentStatusCommandHandler : IRequestHandler<ChangeDepartmentStatusCommand, Result<Unit>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public ChangeDepartmentStatusCommandHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(ChangeDepartmentStatusCommand request, CancellationToken cancellationToken)
        {
            var department = _dbContext.Department
                .SingleOrDefault(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted &&
                    x.Id == request.Id);

            if (department == null) return Result<Unit>.Failure(HttpStatus.NotFound, $"Department not found.");

            department.EntityStatus = request.Status;

            var entityResult = await _dbContext.SaveChangesAsync(cancellationToken);

            if (entityResult <= 0) Result<Unit>.Failure(HttpStatus.BadRequest, $"Failed to change status.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
