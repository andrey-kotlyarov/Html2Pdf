using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public abstract class HToken
    {
        public int Pos { get; protected set; }
        public string Src { get; protected set; }
        public HToken PrevToken { get; private set; }
        public HToken NextToken { get; private set; }


        public HNode Node { get; private set; }
        public bool NodeWasCollected { get; private set; }



        public HToken(int pos, string src)
        {
            Pos = pos;
            Src = src;

            PrevToken = null;
            NextToken = null;

            Node = null;
            NodeWasCollected = false;
        }

        public void SetPrevToken(HToken token)
        {
            PrevToken = token;
        }

        public void SetNextToken(HToken token)
        {
            NextToken = token;
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
