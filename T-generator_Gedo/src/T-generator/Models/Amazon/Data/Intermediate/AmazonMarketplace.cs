using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;

namespace T_generator.Models.Amazon.Data.Intermediate
{
    public class AmazonMarketplace
    {
        public int AmazonMarketplaceID { get; set; }

        public string Name { get; set; }
        public string Prefix { get; set; }

        public int AmazonCurrencyID { get; set; }

        public virtual AmazonCurrency AmazonCurrency { get; set; }
    }
}