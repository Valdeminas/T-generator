using System.Collections.Generic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Intermediate;

namespace T_generator.Models.Amazon.Templates.Items.Group
{
    public class AmazonChildItem
    {
        public int AmazonChildItemID { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<AmazonKeyword> AmazonKeyword { get; set; }
        public virtual ICollection<AmazonImageURL> AmazonImageURL { get; set; }

        public virtual AmazonParentTemplate AmazonParentTemplate { get; set; }
    }
}