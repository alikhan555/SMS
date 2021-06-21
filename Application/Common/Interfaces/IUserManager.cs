using Application.Common.Models;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<Result<string>> CreateUserAsync(AppUser user, string password, IEnumerable<string> roles);
        Task<Result<UserAuthentication>> LoginUserAsync(string username, string password);
        string GetCurrentUserId();
        Task<AppUser> GetUserByName(string name);
        Task<AppUser> GetUserById(string id);
        Task<Result<string>> UpdateUser(AppUser user);
        int GetCurrentCampusId();
        int GetCurrentSchoolId();
        int GetNextUserSerialNo(int schoolId, int cohortId);
        string GenerateUserName(int schoolId, int cohortId, int serialNo);
        string GenerateRandomPassword();
    }
}