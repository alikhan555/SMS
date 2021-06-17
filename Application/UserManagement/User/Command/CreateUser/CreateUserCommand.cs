using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagement.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest<Result<string>>
    {
        //public string Email { get; set; } no required
        //public string Username { get; set; } auto generate
        //public string Password { get; set; } generate randomly

        public bool IsAbleToLogin { get; set; }
        public List<string> Roles { get; set; }

        public string Id { get; set; }
        public int SerialNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GuardianName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Cnic { get; set; }
        public string PhotoPath { get; set; }
        public int SchoolId { get; set; }
        public int CampusId { get; set; }
        public string Qualification { get; set; }

        public string MobileNo { get; set; }
        public string GuardianMobileNo { get; set; }
        public string HomeLandline { get; set; }
        public string Email { get; set; }
        public string GuardianEmail { get; set; }

        public List<int> CohortIds { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
    {
        readonly ISMSDbContext _context;
        readonly IUserManager _userManager;
        readonly IDateTime _datetime;

        public CreateUserCommandHandler(ISMSDbContext context, IUserManager userManager, IDateTime datetime)
        {
            _context = context;
            _userManager = userManager;
            _datetime = datetime;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var password = "RendomPassword";

            var user = new AppUser()
            {
                UserName = "Configuration Required",
                Email = request.Email,
                IsAbleToLogin = request.IsAbleToLogin,

                UserProfile = new UserProfile()
                {
                    SerialNo = 0, //"Configuration Required";
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    GuardianName = request.GuardianName,
                    Address = request.Address,
                    Gender = request.Gender,
                    DateOfBirth = request.DateOfBirth,
                    Cnic = request.Cnic,
                    PhotoPath = "Configuration Required",
                    SchoolId = _userManager.GetCurrentSchoolId(),
                    CampusId = request.CampusId,
                    Qualification = request.Qualification,

                    UserContactInfo = new UserContactInfo()
                    {
                        MobileNo = request.MobileNo,
                        GuardianMobileNo = request.GuardianMobileNo,
                        HomeLandline = request.HomeLandline,
                        Email = request.Email,
                        GuardianEmail = request.GuardianEmail
                    },

                    CohortMembers = request.CohortIds.Select(x =>
                        new CohortMember()
                        {
                            CohortId = x
                        })
                    .ToList()
                }
            };

            var identityResult = await _userManager.CreateUserAsync(user, password, request.Roles);

            if (identityResult.Errors != null && identityResult.Errors.Count() > 0)
                return Result<string>.Failure(HttpStatus.BadRequest, identityResult.Errors);

            return Result<string>.Success("not implemented.");
        }
    }

}