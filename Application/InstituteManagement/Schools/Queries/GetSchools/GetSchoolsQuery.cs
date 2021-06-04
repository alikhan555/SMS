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

namespace Application.InstituteManagement.Schools.Queries.GetSchools
{
    public class GetSchoolsQuery : IRequest<Result<List<SchoolDto>>>
    {

    }

    class GetSchoolsQueryHandler : IRequestHandler<GetSchoolsQuery, Result<List<SchoolDto>>>
    {
        ISMSDbContext _context;

        public GetSchoolsQueryHandler(ISMSDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<SchoolDto>>> Handle(GetSchoolsQuery request, CancellationToken cancellationToken)
        {
            var schools = await _context.Schools
                .Where(x => x.EntityStatus != Common.Enums.EntityStatus.InActive)
                .Select(x => new SchoolDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Initial = x.Initial,
                    NTN = x.NTN,
                    OwnerId = x.OwnerId,
                    EntityStatus = x.EntityStatus
                }).ToListAsync();

            return Result<List<SchoolDto>>.Success(schools);
        }
    }
}