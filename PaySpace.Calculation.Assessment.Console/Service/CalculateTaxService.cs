using PaySpace.Calculation.Assessment.Console.Data;
using PaySpace.Calculation.Assessment.Console.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Service
{
    internal class CalculateTaxService : ICalculateTaxService
    {
        private readonly IPaySpaceUnitofWork _paySpaceUnitofWork;
        public CalculateTaxService(IPaySpaceUnitofWork paySpaceUnitofWork)
        {
            _paySpaceUnitofWork = paySpaceUnitofWork;
        }

        public async Task<UpdateTaxCalculationResponse> UpdateCalculatedTax(string countryName, decimal income)
        {
            string message = "";
            var country = await _paySpaceUnitofWork.CountryRepository.GetCountryDataByCountryName(countryName).ConfigureAwait(false);
            if (country != null)
            {
                var taxRegime = await _paySpaceUnitofWork.TaxRegimeRepository.GetTaxRegimeByTaxRegimeId(country.TaxRegimeId).ConfigureAwait(false);
                if (taxRegime != null)
                {
                    message = await _paySpaceUnitofWork.GetTaxCalculationRepository(taxRegime.Code).UpdateCalculatedTax(country.CountryId, income).ConfigureAwait(false);
                    if (string.IsNullOrEmpty(message))
                    {
                        message = "Yayy..Done!";
                    }
                }
            }
            else
            {
                message = "Please set tax regime for your country.";
            }
            return new UpdateTaxCalculationResponse { Message = message };
        }
    }
}
