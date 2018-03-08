using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    abstract public class HToken
    {
        public int Pos { get; protected set; }
        public string Src { get; protected set; }
        public HToken PrevToken { get; set; }
        public HToken NextToken { get; set; }


        public HToken(int pos, string src)
        {
            Pos = pos;
            Src = src;

            PrevToken = null;
            NextToken = null;
        }


        public override string ToString()
        {
            string desc = "[Token " + this.GetType().Name + "] ";

            desc += "pos: " + Pos;
            //desc += ", prev: " + (PrevToken == null ? "[nil]" : "" + PrevToken.Pos);
            //desc += ", next: " + (NextToken == null ? "[nil]" : "" + NextToken.Pos);
            desc += ", src: '" + Src + "'";

            return desc;
        }
    }
}
