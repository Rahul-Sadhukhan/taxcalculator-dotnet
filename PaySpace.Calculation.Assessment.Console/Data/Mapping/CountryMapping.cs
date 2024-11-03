using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaySpace.Calculation.Assessment.Console.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data.Mapping
{
    internal class CountryMapping: IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country", schema: "dbo");

            // Composite Key
            builder.HasKey(c=>c.CountryId); // Composite primary key
            builder.Property(c=>c.Description).HasColumnName("Description").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Code).HasColumnName("Code").HasMaxLength(3).IsRequired();
            builder.Property(c => c.TaxRegimeId).HasColumnName("TaxRegimeId").IsRequired();
            builder.HasIndex(c => c.TaxRegimeId).HasDatabaseName("FK_Country_TaxRegime");
        }
    }
}
