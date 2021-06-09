using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.Institute;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstituteManagement.HeadOffices.Commands.CreateHeadOffice
{
    public class CreateHeadOfficeCommand : IRequest<Result<int>>
    {
        public bool IsAtCampus { get; set; }
        public int? CampusId { get; set; }
        public string Address { get; set; }
    }

    public class CreateHeadOfficeCommandHandler : IRequestHandler<CreateHeadOfficeCommand, Result<int>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public CreateHeadOfficeCommandHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Result<int>> Handle(CreateHeadOfficeCommand request, CancellationToken cancellationToken)
        {
            var headOffice = new HeadOffice()
            {
                Address = request.Address,
                IsAtCampus = request.IsAtCampus,
                CampusId = request.IsAtCampus ? request.CampusId : null,
                SchoolId = _userManager.GetCurrentSchoolId()
            };

            _dbContext.HeadOffice.Add(headOffice);
            var entityResult = await _dbContext.SaveChangesAsync(cancellationToken);

            if (entityResult <= 0) return Result<int>.Failure(HttpStatus.BadRequest, "Head Office could not created.");

            return Result<int>.Success(headOffice.Id);
        }
    }
}