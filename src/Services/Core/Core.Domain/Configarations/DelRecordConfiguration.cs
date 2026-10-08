using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Configarations
{
    public class DelRecordConfiguration : IEntityTypeConfiguration<DelRecord>
    {
        public void Configure(EntityTypeBuilder<DelRecord> builder)
        {
            builder.HasKey(c => c.DelRecSerialID);

            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework EAM
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
}
