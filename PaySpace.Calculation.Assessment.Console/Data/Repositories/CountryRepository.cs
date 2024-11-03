using Microsoft.EntityFrameworkCore;
using PaySpace.Calculation.Assessment.Console.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data.Repositories
{
    internal class CountryRepository : ICountryRepository
    {
        private readonly IPaySpaceContext _paySpaceContext;
        public CountryRepository(IPaySpaceContext paySpaceContext)
        {
            _paySpaceContext = paySpaceContext;
        }
        public async Task<Country?> GetCountryDataByCountryName(string countryName)
        {
            var data=_paySpaceContext.Countries.ToList();
            return await _paySpaceContext.Countries.FirstOrDefaultAsync(c => c.Description.ToLower()==countryName.ToLower()).ConfigureAwait(false);
        }
    }
}
