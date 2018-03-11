using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public class HNodeSole : HNodeTag
    {
        public HNodeSole(HTagType tagType, IEnumerable<HAttribute> attributes, IEnumerable<HStyle> styles) : base(tagType, attributes, styles)
        {
        }

        public override string ToStringIndent(int indent)
        {
            string indentStr = new string(' ', indent);

            string desc = indentStr + "[SOLE: " + TagType + "]";
            
            return desc;
        }
    }
}
