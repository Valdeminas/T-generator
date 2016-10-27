using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T_generator.Models.Amazon.Basic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Templates.Items;

namespace T_generator.Models.Amazon.Intermediate
{
    public class AmazonChildItem : AmazonItemParent
    {
        public double Price { get; set; }
        public int Quantity { get; set; }

        public virtual AmazonSize AmazonSize { get; set; }
        public virtual AmazonColor AmazonColor { get; set; }
    }
}