using Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public class UserContactInfo : AuditableEntity
    {
        public string Id { get; set; }
        public string MobileNo { get; set; }
        public string GuardianMobileNo { get; set; }
        public string HomeLandline { get; set; }
        public string Email { get; set; }
        public string GuardianEmail { get; set; }
    }
}
