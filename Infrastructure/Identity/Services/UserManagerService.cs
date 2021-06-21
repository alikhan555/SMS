using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    class UserManagerService : IUserManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISMSDbContext _dbContext;

        public UserManagerService(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager,
            TokenService tokenService,
            IHttpContextAccessor httpContextAccessor, 
            ISMSDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<Result<string>> CreateUserAsync(AppUser newUser, string password, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByNameAsync(newUser.UserName);
            if (user != null) return Result<string>.Failure($"Username '{newUser.UserName}' is already taken.");

            var result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRolesAsync(newUser, roles);
                if (result.Succeeded) return Result<string>.Success(newUser.Id);
            }

            return Result<string>.Failure(result.Errors.Select(x => x.Description));
        }

        public async Task<Result<UserAuthentication>> LoginUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user != null && user.EntityStatus == EntityStatus.Active)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var userAuthentication = new UserAuthentication()
                    {
                        Id = user.Id,
                        Username = username,
                        Roles = roles,
                        JWToken = _tokenService.CreateToken(user, roles)
                    };

                    return Result<UserAuthentication>.Success(userAuthentication);
                }
            }

            return Result<UserAuthentication>.Failure("Username or password is invalid.");
        }

        public string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }

        public async Task<AppUser> GetUserByName(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Result<string>> UpdateUser(AppUser user)
        {
            var identityResult = await _userManager.UpdateAsync(user);

            if (identityResult.Succeeded) return Result<string>.Success();
            else return Result<string>.Failure(identityResult.Errors.Select(x => x.Description));
        }

        public int GetCurrentSchoolId()
        {
            var schoolId = _httpContextAccessor.HttpContext.User.FindFirst("SchoolId").Value;
            return int.Parse(schoolId);
        }

        public int GetCurrentCampusId()
        {
            var campusId = _httpContextAccessor.HttpContext.User.FindFirst("CampusId").Value;
            return int.Parse(campusId);
        }

        public int GetNextUserSerialNo(int schoolId, int cohortId)
        {
            var serialNo = _dbContext.CohortMember
                .Include(x => x.Cohort)
                .Where(x => x.Cohort.SchoolId == schoolId && x.Cohort.Id == cohortId)
                .Max(x => (int?)x.SerialNo) ?? 0;

            return serialNo;
        }

        public string GenerateUserName(int schoolId, int cohortId, int serialNo)
        {
            var schoolInitial = _dbContext.Schools.Single(x => x.Id == schoolId).Initial;
            var cohortInitial = _dbContext.Cohort.Single(x => x.Id == cohortId).Initial;

            return $"{schoolInitial}-{cohortInitial}-{serialNo}";
        }

        public string GenerateRandomPassword()
        {
            var smallAlpha = "abcdefghijklmnopqrstuvwxyz";
            var capitalAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var number = "1234567890";
            var specialChar = "!@#$%^&*()_";

            var random = new Random();
            string password = $"{smallAlpha[random.Next(0, 25)]}{smallAlpha[random.Next(0, 25)]}{capitalAlpha[random.Next(0, 25)]}{capitalAlpha[random.Next(0, 25)]}{specialChar[random.Next(0, 10)]}{number[random.Next(0, 10)]}{number[random.Next(0, 9)]}{number[random.Next(0, 9)]}{number[random.Next(0, 9)]}";

            return password;
        }
    }
}