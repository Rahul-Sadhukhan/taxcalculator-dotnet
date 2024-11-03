using Microsoft.EntityFrameworkCore;
using PaySpace.Calculation.Assessment.Console.Data.Repositories;
using PaySpace.Calculation.Assessment.Console.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data
{
    internal class PaySpaceUnitofWork : IPaySpaceUnitofWork
    {
        private readonly Lazy<ICountryRepository> _countryRepository;
        private readonly Lazy<ITaxRegimeRepository> _taxRegimeRepository;
        private readonly Lazy<ITaxCalculationRepository> _taxCalculationRepository;
        private readonly PaySpaceContext _paySpaceContext;
        private readonly ITaxCalculationFactory _taxCalculationFactory;
        public PaySpaceUnitofWork(Func<IPaySpaceContext, ICountryRepository> countryRepository,
            Func<IPaySpaceContext, ITaxRegimeRepository> taxRegimeRepository,
            Func<IPaySpaceContext, ITaxCalculationRepository> taxCalculationRepository,
            Func<ITaxCalculationRepository, ITaxCalculationFactory> taxCalculationFactory,
            PaySpaceContext paySpaceContext)
        {
            _paySpaceContext = paySpaceContext;
            _countryRepository = new Lazy<ICountryRepository>(() => countryRepository(_paySpaceContext));
            _taxRegimeRepository = new Lazy<ITaxRegimeRepository>(() => taxRegimeRepository(_paySpaceContext));
            _taxCalculationRepository = new Lazy<ITaxCalculationRepository>(() => taxCalculationRepository(_paySpaceContext));
            _taxCalculationFactory= taxCalculationFactory(_taxCalculationRepository.Value);
        }
        public IPaySpaceContext PaySpaceContext => _paySpaceContext;
        public ICountryRepository CountryRepository => _countryRepository.Value;

        public ITaxRegimeRepository TaxRegimeRepository => _taxRegimeRepository.Value;

        public ITaxCalculationRepository TaxCalculationRepository => _taxCalculationRepository.Value;
        public async Task<int> SaveChangesAsync()
        {
            return await _paySpaceContext.SaveChangesAsync();
        }

        public ITaxCalculationRepository GetTaxCalculationRepository(string type)
        {
            return _taxCalculationFactory.CreateInstance(type);
        }

    }
}
