using System.Collections.Generic;
using T_generator.Models.Amazon.Data.Dump;
using T_generator.Models.Amazon.Data.Basic;
using T_generator.Models.Amazon.Data.Intermediate;

namespace T_generator.Models.Amazon.Intermediate
{
    public class AmazonParentTemplate
    {
        public int AmazonParentTemplateID { get; set; }
        
        public virtual AmazonProduct AmazonProduct { get; set; }
        public virtual AmazonCountry AmazonCountry { get; set; }
        public virtual AmazonVariation AmazonVariation { get; set; }

        public virtual ICollection<AmazonBulletPoint> AmazonBulletPoint { get; set; }
        public virtual ICollection<AmazonKeyword> AmazonKeyword { get; set; }
        public virtual ICollection<AmazonImageURL> AmazonImageURL { get; set; }
    }
}