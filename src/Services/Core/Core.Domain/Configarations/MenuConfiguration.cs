using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Configarations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(c =>   c.MnuSerialID);
            builder.HasIndex(c => c.MnuSerialID).IsUnique();
         //   builder.Property(e => e.MnuID).HasDefaultValueSql("SELECT NEXT VALUE FOR dbo.MNUID");
            builder.HasIndex(c => c.MnuID).IsUnique();
        }
    }
}
