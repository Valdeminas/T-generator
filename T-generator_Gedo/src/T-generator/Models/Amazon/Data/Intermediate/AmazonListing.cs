using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.JoinTables;

namespace T_generator.Models.Amazon.Data.Intermediate
{
    public class AmazonListing
    {
        public int AmazonListingID { get; set; }
        public string DesignURL { get; set; }


        [Display(Name = "Product")]
        public int AmazonProductID { get; set; }
        public virtual AmazonProduct AmazonProduct { get; set; }

        [Display(Name = "Color")]
        public int AmazonProductColorID { get; set; }
        public virtual AmazonProductColor AmazonProductColor { get; set; }

        [Display(Name = "Design")]
        public int AmazonDesignID { get; set; }
        public virtual AmazonDesign AmazonDesign { get; set; }


        public int? Top { get; set; }
        public int? Left { get; set; }
        public int? Right { get; set; }
        public int? Bot { get; set; }



    }
}