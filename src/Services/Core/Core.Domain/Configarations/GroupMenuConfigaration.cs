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
       public class GroupMenuConfigaration : IEntityTypeConfiguration<GroupMenu>
    {
        public void Configure(EntityTypeBuilder<GroupMenu> builder)
        {
            builder.HasKey(c => c.GrpMnuSerialID);
            builder.HasIndex(c => c.GrpMnuID).IsUnique();
            builder.Property(e => e.GrpMnuID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.GrpMnuID");

        }
    }
}
