using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data.Repositories
{
    internal class ProgTaxCalculationRepository : ITaxCalculationRepository
    {
        private readonly IPaySpaceContext _paySpaceContext;
        public ProgTaxCalculationRepository(IPaySpaceContext paySpaceContext) { _paySpaceContext = paySpaceContext; }
        public string Type => "PROG";

        public async Task<string> UpdateCalculatedTax(int countryId, decimal income)
        {
            return await _paySpaceContext.UpdateProgressiveTax(countryId, income).ConfigureAwait(false);
        }
    }
}
