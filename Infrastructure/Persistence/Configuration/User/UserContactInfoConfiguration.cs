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
    class UserContactInfoConfiguration : IEntityTypeConfiguration<UserContactInfo>
    {
        public void Configure(EntityTypeBuilder<UserContactInfo> builder)
        {
            builder.Property(x => x.MobileNo).HasMaxLength(20);
            builder.Property(x => x.GuardianMobileNo).HasMaxLength(20);
            builder.Property(x => x.HomeLandline).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.GuardianEmail).HasMaxLength(100);

            builder
                .HasOne(x => x.UserProfile)
                .WithOne(x => x.UserContactInfo)
                .HasForeignKey<UserContactInfo>(x => x.Id);
        }
    }
}