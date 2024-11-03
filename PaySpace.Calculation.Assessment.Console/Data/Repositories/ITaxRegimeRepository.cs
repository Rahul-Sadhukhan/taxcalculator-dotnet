using PaySpace.Calculation.Assessment.Console.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data.Repositories
{
    internal interface ITaxRegimeRepository
    {
        Task<TaxRegime?> GetTaxRegimeByTaxRegimeId(int taxRegimeId);
    }
}
