using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Intermediate;

namespace T_generator.Models.Amazon.Variations
{
    public class SizeVariationTemplate
    {
        public int SizeVariationTemplateID { get; set; }

        public string SizeMap { get; set; }
        public string SizeName { get; set; }

        public virtual AmazonSize AmazonSize { get; set; }
        public virtual AmazonMarketplace AmazonMarketplace { get; set; }
    }
}