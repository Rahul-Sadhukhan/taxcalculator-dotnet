using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PaySpace.Calculation.Assessment.Console.Data.Entities;
using PaySpace.Calculation.Assessment.Console.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data
{
    internal class PaySpaceContext: DbContext, IPaySpaceContext
    {
        public PaySpaceContext(DbContextOptions<PaySpaceContext> options)
        : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<TaxRegime> Taxregime { get; set; }

        public async Task<string> UpdateProgressiveTax(int countryId, decimal income)
        {
            var paramCountryId = new SqlParameter
            {
                Value = countryId,
                DbType = DbType.Int32,
                ParameterName = "@CountryId",
            };

            var paramIncome = new SqlParameter
            {
                Value = income,
                DbType = DbType.Decimal,
                ParameterName = "@Income",
            };

            var paramValidationMessage = new SqlParameter
            {
                SqlDbType = SqlDbType.VarChar, 
                Size = 1000, 
                Direction = ParameterDirection.Output,
                ParameterName = "@ValidationMessage"
            };

            await Database.ExecuteSqlRawAsync(
                "EXECUTE [dbo].[UpdateProgressiveTax] @CountryId, @Income, @ValidationMessage OUTPUT",
                paramCountryId,
                paramIncome,
                paramValidationMessage).ConfigureAwait(false);

            return paramValidationMessage.Value?.ToString() ?? string.Empty;
        }

        public async Task<string> UpdateFlatRateTax (int countryId, decimal income)
        {
            var paramCountryId = new SqlParameter
            {
                Value = countryId,
                DbType = DbType.Int32,
                ParameterName = "@CountryId",
            };

            var paramIncome = new SqlParameter
            {
                Value = income,
                DbType = DbType.Decimal,
                ParameterName = "@Income",
            };

            var paramValidationMessage = new SqlParameter
            {
                SqlDbType = SqlDbType.VarChar,
                Size = 1000,
                Direction = ParameterDirection.Output,
                ParameterName = "@ValidationMessage"
            };

            await Database.ExecuteSqlRawAsync(
                "EXECUTE [dbo].[UpdateFlatRateTax] @CountryId, @Income, @ValidationMessage OUTPUT",
                paramCountryId,
                paramIncome,
                paramValidationMessage).ConfigureAwait(false);

            return paramValidationMessage.Value?.ToString() ?? string.Empty;
        }

        public async Task<string> UpdatePercentageTax(int countryId, decimal income)
        {
            var paramCountryId = new SqlParameter
            {
                Value = countryId,
                DbType = DbType.Int32,
                ParameterName = "@CountryId",
            };

            var paramIncome = new SqlParameter
            {
                Value = income,
                DbType = DbType.Decimal,
                ParameterName = "@Income",
            };

            var paramValidationMessage = new SqlParameter
            {
                SqlDbType = SqlDbType.VarChar,
                Size = 1000,
                Direction = ParameterDirection.Output,
                ParameterName = "@ValidationMessage"
            };

            await Database.ExecuteSqlRawAsync(
                "EXECUTE [dbo].[UpdatePercentageTax] @CountryId, @Income, @ValidationMessage OUTPUT",
                paramCountryId,
                paramIncome,
                paramValidationMessage).ConfigureAwait(false);

            return paramValidationMessage.Value?.ToString() ?? string.Empty;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfiguration(new CountryMapping());
            modelBuilder.ApplyConfiguration(new TaxRegimeMapping());
        }
    }
}
