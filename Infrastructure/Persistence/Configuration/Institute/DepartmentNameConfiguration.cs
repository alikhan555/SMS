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
    class DepartmentNameConfiguration : IEntityTypeConfiguration<DepartmentName>
    {
        public void Configure(EntityTypeBuilder<DepartmentName> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Initial).IsRequired().HasMaxLength(10);
        }
    }
}
