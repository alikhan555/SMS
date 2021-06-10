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

namespace Application.InstituteManagement.DepartmentNames.Commands.EditDepartmentName
{
    public class EditDepartmentNameCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }
        public int CampusId { get; set; }
    }

    public class EditDepartmentNameCommandHandler : IRequestHandler<EditDepartmentNameCommand, Result<Unit>>
    { 
        ISMSDbContext _dbContext;
        IUserManager _userManager;

        public EditDepartmentNameCommandHandler(IUserManager userManager, ISMSDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<Result<Unit>> Handle(EditDepartmentNameCommand request, CancellationToken cancellationToken)
        {
            var departmentName = _dbContext.DepartmentName
                .SingleOrDefault(x => 
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus == EntityStatus.Active &&
                    x.Id == request.Id);

            if (departmentName == null) return Result<Unit>.Failure(HttpStatus.NotFound, "Department name not found.");

            departmentName.Name = request.Name;
            departmentName.Initial = request.Initial;
            departmentName.CampusId = request.CampusId;

            var entityStatus = await _dbContext.SaveChangesAsync(cancellationToken);

            if (entityStatus <= 0) return Result<Unit>.Failure(HttpStatus.BadRequest, "Department name could not updated.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
