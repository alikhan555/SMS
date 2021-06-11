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

namespace Application.InstituteManagement.DepartmentNames.Commands.ChangeDepartmentNameStatus
{
    public class ChangeDepartmentNameStatusCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }

    public class ChangeDepartmentNameStatusCommandHandler : IRequestHandler<ChangeDepartmentNameStatusCommand, Result<Unit>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public ChangeDepartmentNameStatusCommandHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(ChangeDepartmentNameStatusCommand request, CancellationToken cancellationToken)
        {
            var departmentName = _dbContext.DepartmentName
                .SingleOrDefault(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted &&
                    x.Id == request.Id);

            if (departmentName == null) return Result<Unit>.Failure(HttpStatus.NotFound, $"Department name not found.");

            departmentName.EntityStatus = request.Status;

            var entityResult = await _dbContext.SaveChangesAsync(cancellationToken);

            if (entityResult <= 0) Result<Unit>.Failure(HttpStatus.BadRequest, $"Failed to change status.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
