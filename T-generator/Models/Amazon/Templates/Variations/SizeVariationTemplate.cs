using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.Variations
{
    public class SizeVariationTemplate
    {
        public int SizeVariationTemplateID { get; set; }

        public string SizeMap { get; set; }
        public string SizeName { get; set; }

        public int AmazonSizeID { get; set; }
        public int AmazonMarketplaceID { get; set; }
    }
}