using Domain.Entities.Institute;
using Domain.Entities.User;
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
        // Institute Info
        public DbSet<School> Schools { get; set; }
        public DbSet<Campus> Campus { get; set; }
        public DbSet<HeadOffice> HeadOffice { get; set; }
        public DbSet<DepartmentName> DepartmentName { get; set; }
        public DbSet<Department> Department { get; set; }
        
        // User Info
        public DbSet<Cohort> Cohort { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserContactInfo> UserContactInfo { get; set; }
        public DbSet<CohortMember> CohortMember { get; set; }

        
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}