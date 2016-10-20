using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace T_generator.Models
{
    public enum Template
    {
        Clothing, Bag, Case, Laptop, Pillow, Shoes, Mugs, Flops
    }

    public enum Size
    {
        XSmall , Small, Medium, Large, XLarge, XXLarge
    }

    public class AmazonProduct
    {
        public int AmazonProductID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string Type { get; set; }
        public string TypeShort { get; set; }
        public string BrowseNode { get; set; }
        public string DepartmentName { get; set; }
        public string StaticSearchtags { get; set; }
        public string BulletPoints { get; set; }
        public Template? Template { get; set; }
        public Size? Size { get; set; }

        public virtual ICollection<AmazonColorVariation> AmazonColorVariations { get; set; }
    }
}