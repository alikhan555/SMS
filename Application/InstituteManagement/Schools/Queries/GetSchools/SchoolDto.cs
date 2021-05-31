using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Schools.Queries.GetSchools
{
    public class SchoolDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Initial { get; set; }

        public Guid OwnerId { get; set; }

        public string NTN { get; set; }
    }
}
