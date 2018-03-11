using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public class HNodeText: HNode
    {
        private string text;
        private string Text { get => text; }

        public HNodeText(string src)
        {
            text = HUtil.StringUtil.HtmlAmpersandSequenceDecode(src);
        }

        public override string ToStringIndent(int indent)
        {
            string indentStr = new string(' ', indent);

            string desc = indentStr + "[TEXT: " + text + "]";

            return desc;
        }
    }
}
