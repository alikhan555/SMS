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

namespace Application.InstituteManagement.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<Result<int>>
    {
        public int DepartmentNameId { get; set; }
        public int CampusId { get; set; }
    }

    class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<int>>
    {
        ISMSDbContext _dbContext;
        IUserManager _userManager;

        public CreateDepartmentCommandHandler(IUserManager userManager, ISMSDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<Result<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = new Department()
            {
                DepartmentNameId = request.DepartmentNameId,
                CampusId = request.CampusId,
                SchoolId = _userManager.GetCurrentSchoolId()
            };

            _dbContext.Department.Add(department);
            var entityStatus = await _dbContext.SaveChangesAsync(cancellationToken);

            if (entityStatus <= 0) return Result<int>.Failure(HttpStatus.BadRequest, "Department could not created.");

            return Result<int>.Success(department.Id);
        }
    }
}
