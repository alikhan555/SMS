using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagement.Cohorts.Queries.GetCohortDetails
{
    public class GetCohortDetailsQuery : IRequest<Result<CohortDetailsDto>>
    {
        public int Id { get; set; }
    }

    class GetCohortDetailsQueryHandler : IRequestHandler<GetCohortDetailsQuery, Result<CohortDetailsDto>>
    {
        ISMSDbContext _context;
        IUserManager _userManager;

        public GetCohortDetailsQueryHandler(ISMSDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Result<CohortDetailsDto>> Handle(GetCohortDetailsQuery request, CancellationToken cancellationToken)
        {
            var cohort = _context.Cohort
                .SingleOrDefault(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted &&
                    x.Id == request.Id);

            if (cohort == null) return Result<CohortDetailsDto>.Failure(HttpStatus.NotFound, $"Cohort with id: {request.Id} not found.");

            var cohortDetailsDto = new CohortDetailsDto
            {
                Id = cohort.Id,
                Name = cohort.Name,
                Description = cohort.Description,
                EntityStatus = cohort.EntityStatus
            };

            return Result<CohortDetailsDto>.Success(cohortDetailsDto);
        }
    }
}
