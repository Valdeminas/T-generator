using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace T_generator.Models
{
    public class AmazonAccount
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string SKUPrefix { get; set; }
    }
}