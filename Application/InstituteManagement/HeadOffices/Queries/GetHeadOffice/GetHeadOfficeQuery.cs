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

namespace Application.InstituteManagement.HeadOffices.Queries.GetHeadOffice
{
    public class GetHeadOfficesQuery : IRequest<Result<IEnumerable<HeadOfficesDTO>>>
    {
    }

    public class GetHeadOfficeQueryHandler : IRequestHandler<GetHeadOfficesQuery, Result<IEnumerable<HeadOfficesDTO>>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;


        public GetHeadOfficeQueryHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<IEnumerable<HeadOfficesDTO>>> Handle(GetHeadOfficesQuery request, CancellationToken cancellationToken)
        {
            var headOffices = await _dbContext.HeadOffice
                .Where(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted)
                .Select(x => 
                new HeadOfficesDTO()
                {
                    Id = x.Id,
                    SchoolId = x.SchoolId,
                    IsAtCampus = x.IsAtCampus,
                    CampusId = x.CampusId,
                    Address = x.Address,
                    Contact1 = x.Contact1,
                    Contact2 = x.Contact2,
                    Contact3 = x.Contact3,
                    EntityStatus = x.EntityStatus
                }
                ).ToListAsync(cancellationToken);

            return Result<IEnumerable<HeadOfficesDTO>>.Success(headOffices);
        }
    }
}
