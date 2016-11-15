using T_generator.Models.Amazon.Data.Basic;

namespace T_generator.Models.Amazon.Objects.Item.Children.Children
{
    public class AmazonChildItem : AmazonItemParent
    {
        public double Price { get; set; }
        public int Quantity { get; set; }

        public virtual AmazonSize AmazonSize { get; set; }
        public virtual AmazonColor AmazonColor { get; set; }
    }
}