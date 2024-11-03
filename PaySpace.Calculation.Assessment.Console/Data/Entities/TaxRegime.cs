﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Data.Entities
{
    internal class TaxRegime
    {
        [Key]
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<Country> Countries { get; set; }
    }
}
