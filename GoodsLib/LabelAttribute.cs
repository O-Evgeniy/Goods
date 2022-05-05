using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsLib
{
    public class LabelAttribute : Attribute
    {
        public string Label { get; set; }

        public LabelAttribute(string label)
        {
            Label = label;
        }
    }
}
