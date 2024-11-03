using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data.Repositories
{
    internal class PercTaxCalculationRepository : ITaxCalculationRepository
    {
        private readonly IPaySpaceContext _paySpaceContext;
        public PercTaxCalculationRepository(IPaySpaceContext paySpaceContext) { _paySpaceContext = paySpaceContext; }
        public string Type => "PERC";

        public async Task<string> UpdateCalculatedTax(int countryId, decimal income)
        {
            return await _paySpaceContext.UpdatePercentageTax(countryId, income).ConfigureAwait(false);
        }
    }
}
