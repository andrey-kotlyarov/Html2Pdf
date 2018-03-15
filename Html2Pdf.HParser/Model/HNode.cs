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


        private HNode prevNode;
        public HNode PrevNode { get => prevNode; }

        private HNode nextNode;
        public HNode NextNode { get => nextNode; }


        public HNode()
        {
            parentNode = null;

            prevNode = null;
            nextNode = null;
        }

        public void SetParentNode(HNode node)
        {
            parentNode = node;
        }

        public void SetPrevNode(HNode node)
        {
            prevNode = node;
        }

        public void SetNextNode(HNode node)
        {
            nextNode = node;
        }

        public abstract string ToStringIndent(int indent);


        public override string ToString()
        {
            string desc = "[HNode " + this.GetType().Name + "] ";

            desc += " - parent: " + (parentNode != null ? parentNode.GetType().Name : "[nil]");
            desc += " - prev: " + (prevNode != null ? prevNode.GetType().Name : "[nil]");
            desc += " - next: " + (nextNode != null ? nextNode.GetType().Name : "[nil]");

            return desc;
        }
    }
}
