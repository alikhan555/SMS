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

namespace Application.InstituteManagement.DepartmentNames.Commands.CreateDepartmentName
{
    public class CreateDepartmentNameCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Initial { get; set; }
    }

    class CreateDepartmentNameCommandHandler : IRequestHandler<CreateDepartmentNameCommand, Result<int>>
{
        ISMSDbContext _dbContext;
        IUserManager _userManager;

        public CreateDepartmentNameCommandHandler(IUserManager userManager, ISMSDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<Result<int>> Handle(CreateDepartmentNameCommand request, CancellationToken cancellationToken)
        {
            var departmentName = new DepartmentName()
            {
                Name = request.Name,
                Initial = request.Initial,
                SchoolId = _userManager.GetCurrentSchoolId()
            };

            _dbContext.DepartmentName.Add(departmentName);
            var entityStatus = await _dbContext.SaveChangesAsync(cancellationToken);

            if (entityStatus <= 0) return Result<int>.Failure(HttpStatus.BadRequest, "Department name could not created.");

            return Result<int>.Success(departmentName.Id);
        }
    }
}
