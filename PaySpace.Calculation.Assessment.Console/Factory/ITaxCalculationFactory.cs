using PaySpace.Calculation.Assessment.Console.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Factory
{
    internal interface ITaxCalculationFactory
    {
        ITaxCalculationRepository CreateInstance(string type);
    }
}
