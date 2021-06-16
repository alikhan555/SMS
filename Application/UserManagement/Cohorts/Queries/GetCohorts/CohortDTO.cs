using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserManagement.Cohorts.Queries.GetCohorts
{
    public class CohortDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EntityStatus { get; set; }
    }
}