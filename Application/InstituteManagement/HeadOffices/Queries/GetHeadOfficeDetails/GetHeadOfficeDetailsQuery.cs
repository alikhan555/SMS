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

namespace Application.InstituteManagement.HeadOffices.Queries.GetHeadOfficeDetails
{
    public class GetHeadOfficeDetailsQuery : IRequest<Result<HeadOfficeDetailsDTO>>
    {
        public int Id { get; set; }
    }

    public class GetHeadOfficeDetailsQueryHandler : IRequestHandler<GetHeadOfficeDetailsQuery, Result<HeadOfficeDetailsDTO>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public GetHeadOfficeDetailsQueryHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<HeadOfficeDetailsDTO>> Handle(GetHeadOfficeDetailsQuery request, CancellationToken cancellationToken)
        {
            var headOfficeDetails = _dbContext.HeadOffice
                .SingleOrDefault(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus != EntityStatus.Deleted &&
                    x.Id == request.Id);

            if (headOfficeDetails == null) return Result<HeadOfficeDetailsDTO>.Failure(HttpStatus.NotFound, $"Head office with id {request.Id} not found.");

            var headOfficeDetailsDTO = new HeadOfficeDetailsDTO
            {
                Id = headOfficeDetails.Id,
                SchoolId = headOfficeDetails.SchoolId,
                IsAtCampus = headOfficeDetails.IsAtCampus,
                CampusId = headOfficeDetails.CampusId,
                Address = headOfficeDetails.Address,
                Contact1 = headOfficeDetails.Contact1,
                Contact2 = headOfficeDetails.Contact2,
                Contact3 = headOfficeDetails.Contact3,
                EntityStatus = headOfficeDetails.EntityStatus
            };

            return Result<HeadOfficeDetailsDTO>.Success(headOfficeDetailsDTO);
        }
    }
}
