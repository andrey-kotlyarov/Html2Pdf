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
        //private IEnumerable<HToken> tokens = new List<HToken>();
        private List<HToken> tokens = new List<HToken>();


        public HDocument(string fileFullName)
        {
            text = System.IO.File.ReadAllText(fileFullName, Encoding.Default);

            // Document in one line
            text = text.Replace("\r\n", " ");
            text = text.Replace("\n\r", " ");
            text = text.Replace("\n", " ").Replace("\r", " ");

            // Remove all comments
            Regex re = new Regex("<!--.*?-->");
            text = re.Replace(text, "");


            //System.Console.Write(text);
            tokenization();
        }
        

        private void tokenization()
        {
            tokens = new List<HToken>();
            Regex reText = new Regex("^[^<>]+");
            Regex reTag = new Regex("^<[^<>]+>");

            Match mText = null;
            Match mTag = null;


            int pos = 0;
            HToken tokenPrev = null;
            bool validToken;

            HToken token = null;

            while (text.Length > 0)
            {
                validToken = false;
                mText = reText.Match(text);

                if (mText.Success)
                {
                    token = new HTokenText(pos, mText.Value);
                    text = reText.Replace(text, "");

                    validToken = true;
                }
                else
                {
                    mTag = reTag.Match(text);

                    if (mTag.Success)
                    {
                        token = new HTokenTag(pos, mTag.Value);
                        text = reTag.Replace(text, "");

                        validToken = ((token as HTokenTag).TagType != HTagType._unknown);
                    }
                    else
                    {
                        throw new HException("No valid HTML file.");
                    }
                }

                if (validToken)
                {
                    token.SetPrevToken(tokenPrev);

                    if (tokenPrev != null)
                    {
                        tokenPrev.SetNextToken(token);
                    }

                    tokens.Add(token);

                    tokenPrev = token;
                    pos++;
                }
            }

            return;
        }

        public override string ToString()
        {
            string desc = "HDocument (tokens):";

            foreach (HToken token in tokens)
            {
                desc += "\r\n" + token.ToString();
            }

            return desc;
        }

    }
}
