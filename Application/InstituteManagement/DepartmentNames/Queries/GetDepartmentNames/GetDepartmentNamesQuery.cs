using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.InstituteManagement.HeadOffices.Queries.GetHeadOffice;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstituteManagement.DepartmentNames.Queries.GetDepartmentNames
{
    public class GetDepartmentNamesQuery : IRequest<Result<IEnumerable<DepartmentNamesDTO>>>
    {
    }

    public class GetDepartmentNamesQueryHandler : IRequestHandler<GetDepartmentNamesQuery, Result<IEnumerable<DepartmentNamesDTO>>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public GetDepartmentNamesQueryHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<IEnumerable<DepartmentNamesDTO>>> Handle(GetDepartmentNamesQuery request, CancellationToken cancellationToken)
        {
            var departmentNames = await _dbContext.DepartmentName
                .Where(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted)
                .Select(x =>
                new DepartmentNamesDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Initial = x.Initial,
                    SchoolId = x.SchoolId,
                    EntityStatus = x.EntityStatus
                }
                ).ToListAsync(cancellationToken);

            return Result<IEnumerable<DepartmentNamesDTO>>.Success(departmentNames);
        }
    }
}
