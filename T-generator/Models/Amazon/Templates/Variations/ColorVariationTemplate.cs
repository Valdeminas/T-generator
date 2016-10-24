using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.Variations
{
    public class ColorVariationTemplate
    {
        public int ColorVariationTemplateID { get; set; }

        public string ColorMap { get; set; }
        public string ColorName { get; set; }

        public virtual AmazonColor AmazonColor { get; set; }
        public virtual AmazonMarketplace AmazonMarketplace { get; set; }
    }
}