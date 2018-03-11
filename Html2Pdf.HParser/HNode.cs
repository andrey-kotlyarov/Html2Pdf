using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public abstract class HNode
    {
        private HNode parentNode;
        public HNode ParentNode { get => parentNode; }

        public HNode()
        {
            parentNode = null;
        }

        public void SetParentNode(HNode node)
        {
            parentNode = node;
        }

        public abstract string ToStringIndent(int indent);


        public override string ToString()
        {
            string desc = "[HNode " + this.GetType().Name + "] ";

            return desc;
        }
    }
}
