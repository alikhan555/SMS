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
    class HeadOfficeConfiguration : IEntityTypeConfiguration<HeadOffice>
    {
        public void Configure(EntityTypeBuilder<HeadOffice> builder)
        {
            builder.Property(x => x.Address).IsRequired();
        }
    }
}
