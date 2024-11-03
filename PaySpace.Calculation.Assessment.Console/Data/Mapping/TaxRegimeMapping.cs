using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaySpace.Calculation.Assessment.Console.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data.Mapping
{
    internal class TaxRegimeMapping : IEntityTypeConfiguration<TaxRegime>
    {
        public void Configure(EntityTypeBuilder<TaxRegime> builder)
        {
            builder.ToTable("TaxRegime", schema: "dbo");

            // Composite Key
            builder.HasKey(c => c.Id); // Composite primary key
            builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Code).HasColumnName("Code").HasMaxLength(3).IsRequired();
        }
    }
}
