using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.Intermediate
{
    public class AmazonChildItem
    {
        public int AmazonChildItemID { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }

        public List<string> Keywords { get; set; }
        public List<string> ImageURL { get; set; }

        public int AmazonParentTemplateID { get; set; }
    }
}