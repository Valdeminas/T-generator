using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.Intermediate
{
    public class AmazonParentTemplate
    {
        public int AmazonParentTemplateID { get; set; }

        public bool HasSizeTable { get; set; }
        public bool HasVariation { get; set; }

        public string Keywords { get; set; }
        public string BulletPoints { get; set; }

        public int AmazonProductID { get; set; }
        public int AmazonDesignID { get; set; }
        public int AmazonCountryID { get; set; }
    }
}