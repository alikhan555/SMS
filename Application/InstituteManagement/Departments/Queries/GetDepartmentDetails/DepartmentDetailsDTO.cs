using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Departments.Queries.GetDepartmentDetails
{
    public class DepartmentDetailsDTO
    {
        public int Id { get; set; }
        public int DepartmentNameId { get; set; }
        public int CampusId { get; set; }
        public int EntityStatus { get; set; }
    }
}