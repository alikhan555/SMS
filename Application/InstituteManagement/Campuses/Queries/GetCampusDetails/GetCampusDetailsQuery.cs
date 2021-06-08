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

namespace Application.InstituteManagement.Campuses.Queries.GetCampusDetails
{
    public class GetCampusDetailsQuery : IRequest<Result<CampusDTO>>
    {
        public int Id { get; set; }
    }

    public class GetCampusDetailsQueryHandler : IRequestHandler<GetCampusDetailsQuery, Result<CampusDTO>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public GetCampusDetailsQueryHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<CampusDTO>> Handle(GetCampusDetailsQuery request, CancellationToken cancellationToken)
        {
            var campusDetails = _dbContext.Campus.SingleOrDefault(x => x.SchoolId == _userManager.GetCurrentSchoolId() && x.EntityStatus != EntityStatus.Deleted && x.Id == request.Id);

            if (campusDetails == null) return Result<CampusDTO>.Failure(HttpStatus.NotFound, $"Campus with id {request.Id} not found.");

            var campusDTO = new CampusDTO()
            {
                Id = campusDetails.Id,
                Name = campusDetails.Name,
                Address = campusDetails.Address,
                EntityStatus = campusDetails.EntityStatus
            };

            return Result<CampusDTO>.Success(campusDTO);
        }
    }
}