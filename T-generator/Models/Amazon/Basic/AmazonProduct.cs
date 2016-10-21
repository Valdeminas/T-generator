using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace T_generator.Models
{
    public class AmazonProduct
    {
        public int AmazonProductID { get; set; }

        public string Name { get; set; }
        public string Prefix { get; set; }

        public string Description { get; set; }
    }
}