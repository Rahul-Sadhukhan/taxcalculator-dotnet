using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data.Entities
{
    internal class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int TaxRegimeId { get; set; }

        public TaxRegime TaxRegime { get; set; }
    }
}
