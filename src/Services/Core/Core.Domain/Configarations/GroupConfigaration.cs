using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace Core.Domain.Configarations
{
     public class GroupConfigaration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(c => c.GrpSerialID);
            builder.HasIndex(c => c.GrpID).IsUnique();
            builder.Property(e => e.GrpID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.GrpID");
        }
    }
}
