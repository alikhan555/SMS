using Application.Common.Enums;
using Application.Common.Interfaces;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seed
{
    public static class SeedIdentityData
    {
        public static async Task SeedDefaultRoles(RoleManager<AppRole> roleManager)
        {
            var developerRole = new AppRole(Role.Developer);
            var schoolOwnerRole = new AppRole(Role.SchoolOwner);

            if (roleManager.Roles.All(x => x.Name != developerRole.Name))
                await roleManager.CreateAsync(developerRole);

            if (roleManager.Roles.All(x => x.Name != schoolOwnerRole.Name))
                await roleManager.CreateAsync(schoolOwnerRole);
        }

        public static async Task SeedDefaultUser(UserManager<AppUser> userManager, IDateTime datetime)
        {
            var developerUser1 = new AppUser()
            {
                UserName = "Developer1",
                Email = "m.alikhan555@gmail.com",
                EntityStatus = EntityStatus.Active,
                Created = datetime.UtcNow,
                CreatedBy = "not configured yet"
            };

            if (userManager.Users.All(x => x.UserName != developerUser1.UserName))
            {
                var result = await userManager.CreateAsync(developerUser1, "developer");
                result = await userManager.AddToRoleAsync(developerUser1, "Developer");
            }
        }
    }
}
