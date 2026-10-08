using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Core.Domain.Configarations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(c => c.ReportSerialID);

            builder.Property(c => c.ReportName).HasColumnName("ReportName").IsRequired();

            builder.HasIndex(c => c.ReportName);

            builder.Property(e => e.DataSet)
               .HasConversion(new StringListToJsonConverter())
               .HasColumnType("nvarchar(max)");
            // HasQueryFilter is used to define a global filter on a DbSet in Entity Framework Core
            // used for scenarios like soft deletes, multi-tenancy
            builder.HasQueryFilter(r => !r.IsDeleted);
            //Purpose: HasFilter is for defining filtered indexes at the database level
            builder.HasIndex(r => r.IsDeleted).HasFilter("[IsDeleted] = 1");
        }
    }
    public class StringListToJsonConverter : ValueConverter<List<string>, string>
    {
        public StringListToJsonConverter()
            : base(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                v => JsonSerializer.Deserialize<List<string>>(v ?? "[]", new JsonSerializerOptions()) ?? new List<string>())
        { }
    }
}
