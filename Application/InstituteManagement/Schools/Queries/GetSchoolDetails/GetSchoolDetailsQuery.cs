using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Schools.Queries.GetSchoolDetails
{
    public class GetSchoolDetailsQuery : IRequest<Result<SchoolDetailsDto>>
    {
        public int Id { get; set; }
    }

    class GetSchoolDetailsQueryHandler : IRequestHandler<GetSchoolDetailsQuery, Result<SchoolDetailsDto>>
    {
        ISMSDbContext _context;

        public GetSchoolDetailsQueryHandler(ISMSDbContext context)
        {
            _context = context;
        }

        public async Task<Result<SchoolDetailsDto>> Handle(GetSchoolDetailsQuery request, CancellationToken cancellationToken)
        {
            var school = await _context.Schools.FindAsync(request.Id);

            if (school == null) return Result<SchoolDetailsDto>.Failure(HttpStatus.NotFound, $"School with id: {request.Id} not found.");

            var schoolDetailsDto = new SchoolDetailsDto
            {
                Id = school.Id,
                Name = school.Name,
                Initial = school.Initial,
                OwnerId = school.OwnerId,
                NTN = school.NTN,
                EntityStatus = school.EntityStatus
            };

            return Result<SchoolDetailsDto>.Success(schoolDetailsDto);
        }
    }
}
