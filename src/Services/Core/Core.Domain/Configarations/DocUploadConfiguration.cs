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
    public class DocUploadConfiguration : IEntityTypeConfiguration<DocUpload>
    {
        public void Configure(EntityTypeBuilder<DocUpload> builder)
        {
            builder.HasKey(c => c.DocUploadSerialID);


            builder.Property(c => c.ModSerialID).HasColumnName("ModuleSerialID").IsRequired();

            builder.HasIndex(c => c.ModSerialID);

            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }

}
