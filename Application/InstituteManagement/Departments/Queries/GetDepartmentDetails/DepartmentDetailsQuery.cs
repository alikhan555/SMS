using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Departments.Queries.GetDepartmentDetails
{
    public class GetDepartmentDetailsQuery : IRequest<Result<DepartmentDetailsDTO>>
    {
        public int Id { get; set; }
    }

    public class GetDepartmentDetailsQueryHandler : IRequestHandler<GetDepartmentDetailsQuery, Result<DepartmentDetailsDTO>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public GetDepartmentDetailsQueryHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<DepartmentDetailsDTO>> Handle(GetDepartmentDetailsQuery request, CancellationToken cancellationToken)
        {
            var departmentDetails = _dbContext.Department
                .SingleOrDefault(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted &&
                    x.Id == request.Id);

            if (departmentDetails == null) return Result<DepartmentDetailsDTO>.Failure(HttpStatus.NotFound, "Department not found.");

            var DepartmentsDTO = new DepartmentDetailsDTO()
            {
                Id = departmentDetails.Id,
                DepartmentNameId = departmentDetails.DepartmentNameId,
                CampusId = departmentDetails.CampusId,
                EntityStatus = departmentDetails.EntityStatus
            };

            return Result<DepartmentDetailsDTO>.Success(DepartmentsDTO);
        }
    }
}
