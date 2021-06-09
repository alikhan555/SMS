using Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Institute
{
    public class HeadOffice : AuditableEntity
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public bool IsAtCampus { get; set; }
        public int? CampusId { get; set; }
        public string Address { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Contact3 { get; set; }

        public School School { get; set; }
        public Campus Campus { get; set; }
    }
}