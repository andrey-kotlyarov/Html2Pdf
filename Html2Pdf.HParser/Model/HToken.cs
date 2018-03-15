using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public abstract class HToken
    {
        private int pos;
        private string src;
        private HToken prevToken;
        private HToken nextToken;

        public int Pos { get => pos; }
        public string Src { get => src; }
        public HToken PrevToken { get => prevToken; }
        public HToken NextToken { get => nextToken; }

        
        protected HNode node;
        public HNode Node { get => node; }




        protected bool nodeWasCollected;
        public bool NodeWasCollected { get => nodeWasCollected; }


        protected bool nodeReadyToCollect;
        public bool NodeReadyToCollect { get => nodeReadyToCollect; }

        


        public HToken(int pos, string src)
        {
            this.pos = pos;
            this.src = src;

            prevToken = null;
            nextToken = null;

            node = null;
            nodeWasCollected = false;
            nodeReadyToCollect = false;
        }

        protected abstract void createNode();

        public void SetPrevToken(HToken token)
        {
            prevToken = token;
        }

        public void SetNextToken(HToken token)
        {
            nextToken = token;
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
            string desc = "[HToken " + this.GetType().Name + "] ";

            desc += "- pos: " + Pos;
            desc += ", prev: " + (PrevToken == null ? "[nil]" : "" + PrevToken.Pos);
            desc += ", next: " + (NextToken == null ? "[nil]" : "" + NextToken.Pos);
            desc += ", src: '" + Src + "'";

            return desc;
        }
    }
}
