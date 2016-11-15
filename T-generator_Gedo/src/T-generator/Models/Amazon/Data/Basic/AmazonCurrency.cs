using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace T_generator.Models.Amazon.Data.Basic
{
    public class AmazonCurrency
    {
        public int AmazonCurrencyID { get; set; }

        public string Currency { get; set; }
    }
}