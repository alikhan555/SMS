using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.InstituteManagement.HeadOffices.Commands.EditHeadOffice
{
    public class EditHeadOfficeCommand : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
        public bool IsAtCampus { get; set; }
        public int? CampusId { get; set; }
        public string Address { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Contact3 { get; set; }
    }

    public class EditHeadOfficeCommandHandler : IRequestHandler<EditHeadOfficeCommand, Result<Unit>>
    {
        private readonly ISMSDbContext _dbContext;
        private readonly IUserManager _userManager;

        public EditHeadOfficeCommandHandler(ISMSDbContext dbContext, IUserManager userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        
        public async Task<Result<Unit>> Handle(EditHeadOfficeCommand request, CancellationToken cancellationToken)
        {
            var headOffice = _dbContext.HeadOffice
                .SingleOrDefault(x =>
                    x.SchoolId == _userManager.GetCurrentSchoolId() &&
                    x.EntityStatus == EntityStatus.Active &&
                    x.Id == request.Id);

            if (headOffice == null) return Result<Unit>.Failure(HttpStatus.NotFound, $"Head office with id : {request.Id} not found.");

            headOffice.Address = request.Address;
            headOffice.Contact1 = request.Contact1;
            headOffice.Contact2 = request.Contact2;
            headOffice.Contact3 = request.Contact3;
            headOffice.IsAtCampus = request.IsAtCampus;
            headOffice.CampusId = request.IsAtCampus ? request.CampusId : null;

            var entityResult = await _dbContext.SaveChangesAsync(cancellationToken);

            if (entityResult <= 0) return Result<Unit>.Failure(HttpStatus.BadRequest, "Failed to update the head office.");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
