using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models
{
    public class AmazonColorVariation
    {
        public int AmazonColorVariationID { get; set; }
        public int AmazonProductID { get; set; }
        public string Name { get; set; }
        public string NameShort { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        public virtual AmazonProduct AmazonProduct { get; set; }

    }
}