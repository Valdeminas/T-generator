using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using T_generator.Models.Amazon.Basic;
using T_generator.Models.Amazon.Data.Dump;

namespace T_generator.Models
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