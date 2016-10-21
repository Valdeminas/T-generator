using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace T_generator.Models
{
    public class AmazonAccount
    {
        public int AmazonAccountID { get; set; }

        public string Name { get; set; }
        public string Prefix { get; set; }
    }
}