using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration.User
{
    class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            //builder.HasOne<AppUser>(x => x.AppUser).WithOne(x => x.UserProfile).HasForeignKey<AppUser>(x => x.).HasForeignKey<>



            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastModified).IsRequired().HasMaxLength(50);
            builder.Property(x => x.GuardianName).HasMaxLength(50);
            builder.Property(x => x.Address);
            builder.Property(x => x.Gender).IsRequired().HasMaxLength(10);
            builder.Property(x => x.DateOfBirth).HasColumnType("Date");
            builder.Property(x => x.Cnic).HasMaxLength(15);
            builder.Property(x => x.Qualification).HasMaxLength(50);
        }
    }
}
