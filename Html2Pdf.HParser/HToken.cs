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


        protected HNode node;
        public HNode Node { get => node;/* private set;*/ }




        protected bool nodeWasCollected;
        public bool NodeWasCollected { get => nodeWasCollected; }


        protected bool nodeReadyToCollect;
        public bool NodeReadyToCollect { get => nodeReadyToCollect; }

        /*
        private bool childNodesWereCollected;
        public bool ChildNodesWereCollected { get => childNodesWereCollected; }
        */

        public HToken(int pos, string src)
        {
            Pos = pos;
            Src = src;

            PrevToken = null;
            NextToken = null;

            node = null;
            nodeWasCollected = false;
        }

        protected abstract void createNode();

        public void SetPrevToken(HToken token)
        {
            PrevToken = token;
        }

        public void SetNextToken(HToken token)
        {
            NextToken = token;
        }

        public void CollectNode()
        {
            nodeWasCollected = true;
        }

        public void ReadyToCollectNode()
        {
            nodeReadyToCollect = true;

        }
        

        public override string ToString()
        {
            string desc = "[Token " + this.GetType().Name + "] ";

            desc += "pos: " + Pos;
            desc += ", prev: " + (PrevToken == null ? "[nil]" : "" + PrevToken.Pos);
            desc += ", next: " + (NextToken == null ? "[nil]" : "" + NextToken.Pos);
            desc += ", src: '" + Src + "'";

            return desc;
        }
    }
}
