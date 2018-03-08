using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public class HTokenText : HToken
    {
        //public string Text { get; protected set; }
        public HTokenText(int pos, string src) : base(pos, src)
        {
        }
    }
}
