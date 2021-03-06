using Domain.Entities.Institute;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ISMSDbContext
    {
        public DbSet<School> Schools { get; set; }
        public DbSet<Campus> Campus { get; set; }
        public DbSet<HeadOffice> HeadOffice { get; set; }
        public DbSet<DepartmentName> DepartmentName { get; set; }
        public DbSet<Department> Department { get; set; }

        
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}