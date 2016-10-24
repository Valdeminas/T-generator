using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.Templates.Items
{
    public class AmazonStandAloneItem
    {
        public int AmazonStandAloneItemID { get; set; }

        public int AmazonMarketplaceID { get; set; }
        public int AmazonProductID { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }

        public List<string> Keywords { get; set; }
        public List<string> BulletPoints { get; set; }
        public List<string> ImageURL { get; set; }

        public int AmazonCountryID { get; set; }

    }
}