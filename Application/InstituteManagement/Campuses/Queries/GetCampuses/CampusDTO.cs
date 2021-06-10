using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.Campuses.Queries.GetCampuses
{
    public class CampusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int EntityStatus { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Contact3 { get; set; }
    }
}
