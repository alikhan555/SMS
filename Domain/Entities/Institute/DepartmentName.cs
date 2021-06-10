using Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Institute
{
    public class DepartmentName : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }
        public int SchoolId { get; set; }
        public int CampusId { get; set; }
    }
}
