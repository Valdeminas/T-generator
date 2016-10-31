using System.Collections.Generic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Intermediate;

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