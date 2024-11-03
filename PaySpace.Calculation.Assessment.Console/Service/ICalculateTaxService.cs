using PaySpace.Calculation.Assessment.Console.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Service
{
    internal interface ICalculateTaxService
    {
        Task<UpdateTaxCalculationResponse> UpdateCalculatedTax(string countryName, decimal income);
    }
}
