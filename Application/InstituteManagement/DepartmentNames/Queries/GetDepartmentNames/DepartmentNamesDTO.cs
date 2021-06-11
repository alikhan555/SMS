using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.DepartmentNames.Queries.GetDepartmentNames
{
    public class DepartmentNamesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }
        public int SchoolId { get; set; }
        public int EntityStatus { get; set; }
    }
}