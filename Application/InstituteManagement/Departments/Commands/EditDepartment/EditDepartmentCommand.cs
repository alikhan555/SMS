using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.Institute;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Departments.Commands.EditDepartment
{
    public class EditDepartmentCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public int DepartmentNameId { get; set; }
        public int CampusId { get; set; }
    }

    public class EditDepartmentCommandHandler : IRequestHandler<EditDepartmentCommand, Result<Unit>>
    { 
        ISMSDbContext _dbContext;
        IUserManager _userManager;

        public EditDepartmentCommandHandler(IUserManager userManager, ISMSDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<Result<Unit>> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = _dbContext.Department
                .SingleOrDefault(x => 
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus == EntityStatus.Active &&
                    x.Id == request.Id);

            if (department == null) return Result<Unit>.Failure(HttpStatus.NotFound, "Department not found.");

            department.CampusId = request.CampusId;
            department.DepartmentNameId = request.DepartmentNameId;

            var entityStatus = await _dbContext.SaveChangesAsync(cancellationToken);

            if (entityStatus <= 0) return Result<Unit>.Failure(HttpStatus.BadRequest, "Department could not updated.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
