using Domain.Entities.Institute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration.Institute
{
    class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Initial).IsRequired().HasMaxLength(7);
            builder.HasIndex(x => x.Initial).IsUnique();

            builder.Property(x => x.NTN).IsRequired().HasMaxLength(7);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
