using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_generator.Models.Amazon.Templates
{
    enum Relation{
        Parent, Child
    }

    public class AmazonVariationTemplate
    {
        public int AmazonVariationTemplateID { get; set; }

        public Relation Relation { get; set; }

        public int VariationThemeID { get; set; }
    }
}