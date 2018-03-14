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

        public List<HNode> ChildNodes { get => childNodes as List<HNode>; }


        public HNodeContainer(HTagType tagType, IEnumerable<HAttribute> attributes, IEnumerable<HStyle> styles) : base(tagType, attributes, styles)
        {
            childNodes = new List<HNode>();
        }


        public void SetChildNodes(IEnumerable<HNode> childNodes)
        {
            this.childNodes = childNodes;
        }

        /*
        public void AppendChildNode(HNode node)
        {
            (childNodes as List<HNode>).Add(node);
        }
        public void PrependChildNode(HNode node)
        {
            (childNodes as List<HNode>).Insert(0, node);
        }
        */

        public void ClearSpaceChildNodes()
        {
            /*
            bool nextNodeIsInline = false;
            
            for (int i = ChildNodes.Count - 1; i >= 0; i--)
            {
                if (ChildNodes[i] is HNodeText && (ChildNodes[i] as HNodeText).Text == " ")
                {
                    if (!nextNodeIsInline)
                    {
                        (childNodes as List<HNode>).RemoveAt(i);
                    }
                }
                else if (ChildNodes[i] is HNodeContainer)
                {
                    (ChildNodes[i] as HNodeContainer).ClearSpaceChildNodes();
                    if ((ChildNodes[i] is HNodeTag) && HUtil.TagUtil.IsInlineTag((ChildNodes[i] as HNodeTag).TagType))
                    {
                        nextNodeIsInline = true;
                    }
                    else if (ChildNodes[i] is HNodeText)
                    {
                        nextNodeIsInline = true;
                    }
                    else
                    {
                        nextNodeIsInline = false;
                    }
                }
            }
            */

            bool nextNodeIsInline = false;
            int lastSpaceIndex = -1;

            for (int i = ChildNodes.Count - 1; i >= 0; i--)
            {
                if (ChildNodes[i] is HNodeText && (ChildNodes[i] as HNodeText).Text == " ")
                {
                    if (!nextNodeIsInline)
                    {
                        (childNodes as List<HNode>).RemoveAt(i);
                    }
                    else
                    {
                        lastSpaceIndex = i;
                    }
                }
                else if (ChildNodes[i] is HNodeContainer)
                {
                    (ChildNodes[i] as HNodeContainer).ClearSpaceChildNodes();
                    if ((ChildNodes[i] is HNodeTag) && HUtil.TagUtil.IsInlineTag((ChildNodes[i] as HNodeTag).TagType))
                    {
                        nextNodeIsInline = true;
                    }
                    else if (ChildNodes[i] is HNodeText)
                    {
                        nextNodeIsInline = true;
                    }
                    else if ((ChildNodes[i] is HNodeTag) && HUtil.TagUtil.IsBlockTag((ChildNodes[i] as HNodeTag).TagType))
                    {
                        if (lastSpaceIndex - 1 == i)
                        {
                            (childNodes as List<HNode>).RemoveAt(lastSpaceIndex);
                            lastSpaceIndex = -1;
                        }
                        nextNodeIsInline = false;
                    }
                    else
                    {
                        nextNodeIsInline = false;
                    }
                }
            }







            /*
            //
            //
            //
            List<int> indexesToRemove = new List<int>();


            bool prevNodeIsBlock = false;

            for (int i = 0; i < ChildNodes.Count - 1; i++)
            {
                if (ChildNodes[i] is HNodeText && (ChildNodes[i] as HNodeText).Text == " ")
                {
                    if (prevNodeIsBlock)
                    {
                        indexesToRemove.Add(i);
                    }
                }
                else if (ChildNodes[i] is HNodeContainer)
                {
                    (ChildNodes[i] as HNodeContainer).ClearSpaceChildNodes();
                    if ((ChildNodes[i] is HNodeTag) && HUtil.TagUtil.IsBlockTag((ChildNodes[i] as HNodeTag).TagType))
                    {
                        prevNodeIsBlock = true;
                    }
                    else
                    {
                        prevNodeIsBlock = false;
                    }
                }
            }


            foreach (int indexToRemove in indexesToRemove)
            {
                (childNodes as List<HNode>).RemoveAt(indexToRemove);
            }
            //
            //
            //
            */
        }

    }
}
