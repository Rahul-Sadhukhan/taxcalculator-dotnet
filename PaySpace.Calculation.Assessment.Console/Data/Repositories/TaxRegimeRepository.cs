using Microsoft.EntityFrameworkCore;
using PaySpace.Calculation.Assessment.Console.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data.Repositories
{
    internal class TaxRegimeRepository: ITaxRegimeRepository
    {
        private readonly IPaySpaceContext _paySpaceContext;
        public TaxRegimeRepository(IPaySpaceContext paySpaceContext)
        {
            _paySpaceContext = paySpaceContext;
        }
        public async Task<TaxRegime?> GetTaxRegimeByTaxRegimeId(int taxRegimeId)
        {
            return await _paySpaceContext.Taxregime.FirstOrDefaultAsync(c => c.Id== taxRegimeId).ConfigureAwait(false);
        }
    }
}
