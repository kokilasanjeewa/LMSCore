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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(c => c.UserID).IsUnique();
            builder.HasOne(u => u.Group)
                   .WithMany(s => s.Users)
                   .HasForeignKey(u => u.GrpSerialID)
                   .IsRequired(false);

            builder.HasMany(u => u.MenuPermissions)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserSerialID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Companies)
                   .WithOne(c => c.User)
                   .HasForeignKey(c => c.UserSerialID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

