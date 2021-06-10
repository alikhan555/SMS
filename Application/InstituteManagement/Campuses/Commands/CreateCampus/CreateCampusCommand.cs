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

namespace Application.InstituteManagement.Campuses.Commands.CreateCampus
{
    public class CreateCampusCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Contact3 { get; set; }
    }

    public class CreateCampusCommandHandler : IRequestHandler<CreateCampusCommand, Result<int>>
    {
        readonly IUserManager _userMannager;
        readonly ISMSDbContext _dbContext;

        public CreateCampusCommandHandler(
            IUserManager userManager, 
            ISMSDbContext dbContext)
        {
            _userMannager = userManager;
            _dbContext = dbContext;
        }

        public async Task<Result<int>> Handle(CreateCampusCommand request, CancellationToken cancellationToken)
        {
            var campus = new Campus()
            {
                Name = request.Name,
                Address = request.Address,
                Contact1 = request.Contact1,
                Contact2 = request.Contact2,
                Contact3 = request.Contact3,
                SchoolId = _userMannager.GetCurrentSchoolId(),
            };

            _dbContext.Campus.Add(campus);

            var entityResult = await _dbContext.SaveChangesAsync(cancellationToken);

            if (entityResult <= 0) return Result<int>.Failure(HttpStatus.BadRequest, "Campus could not registered.");

            return Result<int>.Success(campus.Id);
        }
    }
}