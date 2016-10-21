using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.FinalToo
{
    public class AmazonChildListing
    {
        public int AmazonChildListingID { get; set; }

        public int AmazonParentTemplateID { get; set; }
        public int AmazonChildTemplateID { get; set; }
        public int AmazonVariationTemplateID { get; set; }
    }
}