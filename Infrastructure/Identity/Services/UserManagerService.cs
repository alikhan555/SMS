using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public UserManagerService(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager,
            TokenService tokenService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
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
                    var userAuthentication = new UserAuthentication()
                    {
                        Id = user.Id,
                        Username = username,
                        JWToken = _tokenService.CreateToken(user, await _userManager.GetRolesAsync(user))
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
    }
}