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
        public string GetCurrentUserId();
    }
}