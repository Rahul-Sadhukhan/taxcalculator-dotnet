using PaySpace.Calculation.Assessment.Console.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Factory
{
    internal class TaxCalculationFactory : ITaxCalculationFactory
    {
        private readonly IEnumerable<ITaxCalculationRepository> _taxCalculationRepository;

        public TaxCalculationFactory(IEnumerable<ITaxCalculationRepository> taxCalculationRepository)
        {
            _taxCalculationRepository = taxCalculationRepository;
        }

        public ITaxCalculationRepository CreateInstance(string type)
        {
            return _taxCalculationRepository.FirstOrDefault(g => g.Type == type)
                ?? throw new InvalidOperationException($"Tax regime of type {type} not found.");
        }
    }
}
