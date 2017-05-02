using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.JoinTables;

namespace T_generator.Models.Amazon.Data.Intermediate
{
    public class AmazonProductColor
    {
        public int AmazonProductColorID { get; set; }

        public string Name { get; set; }
        [Range(0, 1)]
        public decimal Opacity { get; set; }
        public string DesignURL { get; set; }


        [Display(Name = "Product")]
        public int AmazonProductID { get; set; }
        public virtual AmazonProduct AmazonProduct { get; set; }

    }
}