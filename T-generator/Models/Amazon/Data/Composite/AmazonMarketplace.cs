using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T_generator.Models.Amazon.Basic;

namespace T_generator.Models.Amazon
{
    public class AmazonMarketplace
    {
        private int AmazonMarketplaceID { get; set; }

        private string Name { get; set; }
        private string Prefix { get; set; }

        public virtual AmazonCurrency AmazonCurrency { get; set; }
    }
}