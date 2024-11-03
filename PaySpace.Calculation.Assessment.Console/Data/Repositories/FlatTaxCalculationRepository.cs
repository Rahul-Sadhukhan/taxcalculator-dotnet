using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data.Repositories
{
    internal class FlatTaxCalculationRepository : ITaxCalculationRepository
    {
        private readonly IPaySpaceContext _paySpaceContext;
        public FlatTaxCalculationRepository(IPaySpaceContext paySpaceContext) { _paySpaceContext = paySpaceContext; }
        public string Type => "FLAT";

        public async Task<string> UpdateCalculatedTax(int countryId, decimal income)
        {
            return await _paySpaceContext.UpdateFlatRateTax(countryId, income).ConfigureAwait(false);
        }
    }
}
