using T_generator.Models.Amazon.Data.Basic;

namespace T_generator.Models.Amazon.Objects.Item.Children
{
    public class AmazonItemParent : AmazonItem
    {       
        public virtual AmazonVariation AmazonVariation { get; set; }
    }
}