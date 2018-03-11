using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public class HNodeElement : HNodeContainer
    {
        public HNodeElement(HTagType tagType, IEnumerable<HAttribute> attributes, IEnumerable<HStyle> styles) : base(tagType, attributes, styles)
        {
        }




        public override string ToStringIndent(int indent)
        {
            string indentStr = new string(' ', indent);

            string desc = indentStr + "[ELEMENT: " + TagType + "]";

            foreach (HNode node in ChildNodes)
            {
                desc += "\r\n" + node.ToStringIndent(indent + 2);
            }

            return desc;
        }
        
    }
}
