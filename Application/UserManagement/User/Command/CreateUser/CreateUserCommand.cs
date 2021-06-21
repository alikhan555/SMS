using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagement.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest<Result<string>>
    {
        public bool IsAbleToLogin { get; set; }
        public List<string> Roles { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GuardianName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Cnic { get; set; }
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
            var currentSchoolId = _userManager.GetCurrentSchoolId();
            var nextSerialNo = _userManager.GetNextUserSerialNo(currentSchoolId, request.CohortIds.First()) + 1;
            var userName = _userManager.GenerateUserName(currentSchoolId, request.CohortIds.First(), nextSerialNo);
            var password = _userManager.GenerateRandomPassword();

            var user = new AppUser()
            {
                UserName = userName,
                Email = request.Email,
                IsAbleToLogin = request.IsAbleToLogin,
                SchoolId = currentSchoolId,
                CampusId = request.CampusId,

                UserProfile = new UserProfile()
                {
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
                            CohortId = x,
                            SerialNo = nextSerialNo
                        })
                    .ToList()
                }
            };

            var identityResult = await _userManager.CreateUserAsync(user, password, request.Roles);

            if (identityResult.Errors != null && identityResult.Errors.Count() > 0)
                return Result<string>.Failure(HttpStatus.BadRequest, identityResult.Errors);

            return Result<string>.Success(user.Id);
        }
    }
}