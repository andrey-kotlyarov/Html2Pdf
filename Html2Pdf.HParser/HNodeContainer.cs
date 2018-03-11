using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public abstract class HNodeContainer: HNodeTag
    {
        private IEnumerable<HNode> childNodes;

        public List<HNode> ChildNodes { get => childNodes as List<HNode>; set => childNodes = value; }


        public HNodeContainer(HTagType tagType, IEnumerable<HAttribute> attributes, IEnumerable<HStyle> styles) : base(tagType, attributes, styles)
        {
            childNodes = new List<HNode>();
        }


        public void AppendChildNode(HNode node)
        {
            (childNodes as List<HNode>).Add(node);
        }
        public void PrependChildNode(HNode node)
        {
            (childNodes as List<HNode>).Insert(0, node);
        }

    }
}
