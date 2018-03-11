using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public class HDocument
    {
        private string text = String.Empty;

        private IEnumerable<HToken> tokens = new List<HToken>();
        public List<HToken> Tokens { get => tokens as List<HToken>; }

        private HNode rootNode;
        public HNode RootNode { get => rootNode; }



        public HDocument(string fileFullName)
        {
            text = System.IO.File.ReadAllText(fileFullName, Encoding.Default);

            // Document in one line
            /*
            text = text.Replace("\r\n", " ");
            text = text.Replace("\n\r", " ");
            text = text.Replace("\n", " ").Replace("\r", " ");
            */
            text = HUtil.StringUtil.ToSingleLine(text);

            // Remove all comments
            //Regex re = new Regex("<!--.*?-->");
            text = HUtil.StringUtil.Re_TokenComment.Replace(text, "");


            //System.Console.Write(text);
            tokenization();
            nodenization();
        }
        

        private void tokenization()
        {
            tokens = new List<HToken>();
            //Regex reText = new Regex("^[^<>]+");
            //Regex reTag = new Regex("^<[^<>]+>");

            Match mText = null;
            Match mTag = null;


            int pos = 0;
            HToken tokenPrev = null;
            bool validToken;

            HToken token = null;

            while (text.Length > 0)
            {
                validToken = false;
                mText = HUtil.StringUtil.Re_TokenFirstText.Match(text);

                if (mText.Success)
                {
                    token = new HTokenText(pos, mText.Value);
                    //text = reText.Replace(text, "");
                    text = HUtil.StringUtil.Re_TokenFirstText.Replace(text, "");

                    validToken = true;
                }
                else
                {
                    mTag = HUtil.StringUtil.Re_TokenFirstTag.Match(text);

                    if (mTag.Success)
                    {
                        token = new HTokenTag(pos, mTag.Value);
                        //text = reTag.Replace(text, "");
                        text = HUtil.StringUtil.Re_TokenFirstTag.Replace(text, "");

                        validToken = ((token as HTokenTag).TagType != HTagType._unknown);
                    }
                    else
                    {
                        throw new HException("No valid HTML file. (1)");
                    }
                }

                if (validToken)
                {
                    token.SetPrevToken(tokenPrev);

                    if (tokenPrev != null)
                    {
                        tokenPrev.SetNextToken(token);
                    }

                    (tokens as List<HToken>).Add(token);

                    tokenPrev = token;
                    pos++;
                }
            }

            return;
        }


        /*
        private void nodenization()
        {
            rootNode = null;

            while (true)
            {

                HTagType currentTagType = HTagType._unknown;
                HTokenTag currentOpenToken = null;
                HTokenTag currentCloseToken = null;
                List<HNode> childNodes = new List<HNode>();

                foreach (HToken token in tokens)
                {
                    if ((token is HTokenTag) && (token as HTokenTag).IsClose && !(token as HTokenTag).IsOpen && !token.NodeWasCollected)
                    {
                        currentCloseToken = (token as HTokenTag);
                        currentTagType = currentCloseToken.TagType;
                        break;
                    }
                }


                if (currentCloseToken != null)
                {
                    currentCloseToken.CollectNode();

                    HToken prevToken = null;

                    do
                    {
                        prevToken = (prevToken != null ? prevToken.PrevToken : currentCloseToken.PrevToken);
                        if (prevToken == null) throw new HException("No valid HTML file. (2)");

                        if (prevToken.NodeWasCollected)
                        {
                            if (prevToken.Node != null)
                            {
                                childNodes.Add(prevToken.Node);
                            }
                        }
                        else if ((prevToken is HTokenTag) && (prevToken as HTokenTag).IsOpen && !(prevToken as HTokenTag).IsClose && !prevToken.NodeWasCollected && (prevToken as HTokenTag).TagType == currentTagType)
                        {
                            currentOpenToken = (prevToken as HTokenTag);

                            if (currentOpenToken.Node != null && (currentOpenToken.Node is HNodeElement))
                            {
                                (currentOpenToken.Node as HNodeElement).ChildNodes = childNodes;
                            }
                            currentOpenToken.CollectNode();

                        }
                        else
                        {
                            throw new HException("No valid HTML file. (3)");
                        }

                    }
                    while (currentOpenToken != null);



                    //rootNode = xxx;
                }
                else
                {
                    break;
                }







                





            }
            //TODO
        }
        */



        private void nodenization()
        {
            rootNode = null;

            while (true)
            {

                HTagType currentTagType = HTagType._unknown;
                HTokenTag currentOpenToken = null;
                HTokenTag currentCloseToken = null;
                List<HNode> childNodes = new List<HNode>();

                foreach (HToken token in tokens)
                {
                    if ((token is HTokenTag) && (token as HTokenTag).IsClose && !(token as HTokenTag).IsOpen && !token.NodeWasCollected)
                    {
                        currentCloseToken = (token as HTokenTag);
                        currentTagType = currentCloseToken.TagType;
                        break;
                    }
                }


                if (currentCloseToken != null)
                {
                    currentCloseToken.ReadyToCollectNode();
                    currentCloseToken.CollectNode();
                    

                    HToken prevToken = null;

                    do
                    {
                        prevToken = (prevToken != null ? prevToken.PrevToken : currentCloseToken.PrevToken);
                        if (prevToken == null) throw new HException("No valid HTML file. (2)");



                        if ((prevToken is HTokenTag) && (prevToken as HTokenTag).IsOpen && !(prevToken as HTokenTag).IsClose && !prevToken.NodeWasCollected && !prevToken.NodeReadyToCollect && (prevToken as HTokenTag).TagType == currentTagType)
                        {
                            currentOpenToken = (prevToken as HTokenTag);
                            if (currentOpenToken.Node != null && (currentOpenToken.Node is HNodeElement))
                            {
                                (currentOpenToken.Node as HNodeElement).ChildNodes = childNodes;
                            }

                            currentOpenToken.ReadyToCollectNode();
                        }
                        else if (prevToken.NodeReadyToCollect && !prevToken.NodeWasCollected)
                        {
                            if (prevToken.Node != null)
                            {
                                //childNodes.Add(prevToken.Node);
                                childNodes.Insert(0, prevToken.Node);
                            }
                            prevToken.CollectNode();
                        }
                        else if (prevToken.NodeWasCollected)
                        {

                        }
                        else
                        {
                            throw new HException("No valid HTML file. (3)");
                        }

                        
                    }
                    while (currentOpenToken == null);


                    if  (currentOpenToken != null && currentOpenToken.Node != null && (currentOpenToken.Node is HNodeElement))
                    {
                        foreach (HNode node in (currentOpenToken.Node as HNodeElement).ChildNodes)
                        {
                            node.SetParentNode(currentOpenToken.Node);
                        }

                        this.rootNode = currentOpenToken.Node;
                    }
                }
                else
                {
                    break;
                }

            }
        }


        public override string ToString()
        {
            string desc = "HDocument (tokens):";

            foreach (HToken token in tokens)
            {
                desc += "\r\n" + token.ToString();
            }


            if (rootNode != null)
            {
                desc += "\r\n\r\nNODE's TREE:\r\n";
                desc += rootNode.ToStringIndent(0);
            }

            return desc;
        }

    }
}
