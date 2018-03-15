using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public class HTokenText : HToken
    {
        public HTokenText(int pos, string src) : base(pos, src)
        {
            createNode();
        }

        protected override void createNode()
        {
            node = new HNodeText(Src);

            nodeWasCollected = false;
            nodeReadyToCollect = true;
        }
    }
}
