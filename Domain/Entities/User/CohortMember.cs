using Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public class CohortMember : AuditableEntity
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public int CohortId { get; set; }
        public int SerialNo { get; set; }



        public Cohort Cohort { get; set; }
        public UserProfile Member { get; set; }
    }
}