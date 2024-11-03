using PaySpace.Calculation.Assessment.Console.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data
{
    internal interface IPaySpaceUnitofWork
    {
        ICountryRepository CountryRepository { get; }

        ITaxRegimeRepository TaxRegimeRepository { get; }

        ITaxCalculationRepository TaxCalculationRepository { get; }

        Task<int> SaveChangesAsync();

        ITaxCalculationRepository GetTaxCalculationRepository(string type);
    }
}
