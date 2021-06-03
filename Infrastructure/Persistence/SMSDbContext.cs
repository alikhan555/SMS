using Application.Common.Interfaces;
using Domain.Entities.Institute;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.Entities.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Entities.User;
using Application.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Persistence
{
    public class SMSDbContext : IdentityDbContext<AppUser, AppRole, string>, ISMSDbContext
    {
        private readonly IDateTime _dateTime;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SMSDbContext(DbContextOptions options, IDateTime dateTime, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<IAuditableEntity> entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = currentUserId;
                        entry.Entity.Created = _dateTime.UtcNow;
                        entry.Entity.EntityStatus = EntityStatus.Active;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = currentUserId;
                        entry.Entity.LastModified = _dateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}