using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T_generator.Resources.DataStructures;

namespace T_generator.Models.Amazon.Intermediate
{
    public class AmazonParentTemplate
    {
        public int AmazonParentTemplateID { get; set; }
       
        public List<string> BulletPoints { get; set; }     

        public int AmazonProductID { get; set; }
        public int AmazonCountryID { get; set; }
        public int AmazonVariationID { get; set; }

        public virtual PersistableStringCollection test { get; set; }

        public List<string> Keywords { get; set; }
        public List<string> ImageURL { get; set; }
    }
}