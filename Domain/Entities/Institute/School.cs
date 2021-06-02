using Domain.Entities.Shared;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Institute
{
    public class School : AuditableEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Initial { get; set; }

        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        public string NTN { get; set; }



        public AppUser Owner { get; set; }
    }
}