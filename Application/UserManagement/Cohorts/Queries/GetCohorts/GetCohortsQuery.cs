using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagement.Cohorts.Queries.GetCohorts
{
    public class GetCohortsQuery : IRequest<Result<List<CohortDTO>>>
    {

    }

    class GetCohortsQueryHandler : IRequestHandler<GetCohortsQuery, Result<List<CohortDTO>>>
    {
        ISMSDbContext _context;
        IUserManager _userManager;

        public GetCohortsQueryHandler(ISMSDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Result<List<CohortDTO>>> Handle(GetCohortsQuery request, CancellationToken cancellationToken)
        {
            var cohorts = await _context.Cohort
                .Where(x => 
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted)
                .Select(x => new CohortDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Initial = x.Initial,
                    EntityStatus = x.EntityStatus
                }).ToListAsync(cancellationToken);

            return Result<List<CohortDTO>>.Success(cohorts);
        }
    }
}