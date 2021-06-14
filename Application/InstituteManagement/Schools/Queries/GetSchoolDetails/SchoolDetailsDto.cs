using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Schools.Queries.GetSchoolDetails
{
    public class SchoolDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }
        public string OwnerId { get; set; }
        public string OwnerEmail { get; set; }
        public string NTN { get; set; }
        public int EntityStatus { get; set; }
    }
}
