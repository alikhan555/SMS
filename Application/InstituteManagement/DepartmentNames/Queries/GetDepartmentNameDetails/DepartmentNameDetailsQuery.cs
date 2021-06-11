using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstituteManagement.DepartmentNames.Queries.GetDepartmentNameDetails
{
    public class GetDepartmentNameDetailsQuery : IRequest<Result<DepartmentNameDetailsDTO>>
    {
        public int Id { get; set; }
    }

    public class GetDepartmentNameDetailsQueryHandler : IRequestHandler<GetDepartmentNameDetailsQuery, Result<DepartmentNameDetailsDTO>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public GetDepartmentNameDetailsQueryHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<DepartmentNameDetailsDTO>> Handle(GetDepartmentNameDetailsQuery request, CancellationToken cancellationToken)
        {
            var departmentNameDetails = _dbContext.DepartmentName
                .SingleOrDefault(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted &&
                    x.Id == request.Id);

            if (departmentNameDetails == null) return Result<DepartmentNameDetailsDTO>.Failure(HttpStatus.NotFound, "Department name not found.");

            var departmentNamesDTO = new DepartmentNameDetailsDTO()
            {
                Id = departmentNameDetails.Id,
                Name = departmentNameDetails.Name,
                Initial = departmentNameDetails.Initial,
                SchoolId = departmentNameDetails.SchoolId,
                EntityStatus = departmentNameDetails.EntityStatus
            };

            return Result<DepartmentNameDetailsDTO>.Success(departmentNamesDTO);
        }
    }
}
