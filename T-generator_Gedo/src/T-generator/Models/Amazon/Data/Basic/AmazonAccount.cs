using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models.Amazon.Data.Intermediate;

namespace T_generator.Models.Amazon.Data.Basic
{
    public class AmazonAccount
    {
        public int AmazonAccountID { get; set; }

        public string Name { get; set; }
        public string Prefix { get; set; }

        [Display(Name = "Marketplace")]
        public int AmazonMarketplaceID { get; set; }

        public virtual AmazonMarketplace AmazonMarketplace { get; set; }
    }
}