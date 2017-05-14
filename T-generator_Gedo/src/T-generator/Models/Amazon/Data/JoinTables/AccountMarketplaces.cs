using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.Intermediate;

namespace T_generator.Models.Amazon.Data.JoinTables
{
    public class AccountMarketplaces
    {
        public int MarketplaceID { get; set; }
        public int AccountID { get; set; }

        public AmazonMarketplace Marketplace { get; set; }
        public AmazonAccount Account { get; set; }
    }
}
