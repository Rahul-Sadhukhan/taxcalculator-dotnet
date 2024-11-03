using Microsoft.EntityFrameworkCore;
using PaySpace.Calculation.Assessment.Console.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data
{
    internal interface IPaySpaceContext : IDisposable
    {
        DbSet<Country> Countries { get; set; }
        DbSet<TaxRegime> Taxregime { get; set; }

        Task<string> UpdatePercentageTax(int countryId, decimal income);
        Task<string> UpdateFlatRateTax(int countryId, decimal income);

        Task<string> UpdateProgressiveTax(int countryId, decimal income);
    }
}
