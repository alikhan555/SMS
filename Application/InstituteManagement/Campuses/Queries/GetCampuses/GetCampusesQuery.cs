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

namespace Application.InstituteManagement.Campuses.Queries.GetCampuses
{
    public class GetCampusesQuery : IRequest<Result<List<CampusDTO>>>
    {
    }

    public class GetCampusesQueryHandler : IRequestHandler<GetCampusesQuery, Result<List<CampusDTO>>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public GetCampusesQueryHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<List<CampusDTO>>> Handle(GetCampusesQuery request, CancellationToken cancellationToken)
        {
            var campuses = await _dbContext.Campus
                .Where(x => x.EntityStatus != EntityStatus.Deleted && x.SchoolId == _userManager.GetCurrentSchoolId())
                .Select(x => new CampusDTO() { 
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    EntityStatus = x.EntityStatus
                })
                .ToListAsync(cancellationToken);

            return Result<List<CampusDTO>>.Success(campuses);
        }
    }
}