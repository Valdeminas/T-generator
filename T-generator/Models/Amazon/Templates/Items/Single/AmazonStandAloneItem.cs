using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T_generator.Models.Amazon.Basic;
using T_generator.Models.Amazon.Data.Dump;

namespace T_generator.Models.Amazon.Templates.Items
{
    public class AmazonStandAloneItem
    {
        public int AmazonStandAloneItemID { get; set; }

        public virtual AmazonProduct AmazonProduct { get; set; }
        public virtual AmazonCountry AmazonCountry { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<AmazonBulletPoint> AmazonBulletPoint { get; set; }
        public virtual ICollection<AmazonKeyword> AmazonKeyword { get; set; }
        public virtual ICollection<AmazonImageURL> AmazonImageURL { get; set; }
    }
}