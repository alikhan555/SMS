using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Departments.Queries.GetDepartment
{
    public class GetDepartmentsQuery : IRequest<Result<IEnumerable<DepartmentsDTO>>>
    {
    }

    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, Result<IEnumerable<DepartmentsDTO>>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public GetDepartmentsQueryHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<IEnumerable<DepartmentsDTO>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var Departments = await _dbContext.Department
                .Where(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted)
                .Select(x =>
                new DepartmentsDTO()
                {
                    Id = x.Id,
                    CampusId = x.CampusId,
                    DepartmentNameId = x.DepartmentNameId,
                    EntityStatus = x.EntityStatus
                })
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<DepartmentsDTO>>.Success(Departments);
        }
    }
}
