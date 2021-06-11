using Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Institute
{
    public class Department : AuditableEntity
    {
        public int Id { get; set; }
        public int DepartmentNameId { get; set; }
        public int SchoolId { get; set; }
        public int CampusId { get; set; }

        public DepartmentName DepartmentName { get; set; }
        public Campus Campus { get; set; }
    }
}
