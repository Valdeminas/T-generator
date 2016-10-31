using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Intermediate;

namespace T_generator.Models.Amazon.Variations
{
    public class ColorVariationTemplate
    {
        public int ColorVariationTemplateID { get; set; }

        public string ColorMap { get; set; }
        public string ColorName { get; set; }

        public virtual AmazonColor AmazonColor { get; set; }
        public virtual AmazonMarketplace AmazonMarketplace { get; set; }
    }
}