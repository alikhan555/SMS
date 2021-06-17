using Domain.Entities.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public class AppUser : IdentityUser, IAuditableEntity
    {
        public int SchoolId { get; set; }
        public int CampusId { get; set; }
        public bool IsAbleToLogin { get; set; }

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public int EntityStatus { get; set; }



        public UserProfile UserProfile { get; set; }
    }
}
