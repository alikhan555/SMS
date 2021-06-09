using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InstituteManagement.HeadOffices.Queries.GetHeadOffice
{
    public class HeadOfficesDTO
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public bool IsAtCampus { get; set; }
        public int? CampusId { get; set; }
        public string Address { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Contact3 { get; set; }
        public int EntityStatus { get; set; }
    }
}