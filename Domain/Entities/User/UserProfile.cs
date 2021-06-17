using Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public class UserProfile : AuditableEntity
    {
        // ForegnKey AppUserID
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



        public AppUser AppUser { get; set; }
        public UserContactInfo UserContactInfo { get; set; }
        public ICollection<CohortMember> CohortMembers { get; set; }
    }
}
