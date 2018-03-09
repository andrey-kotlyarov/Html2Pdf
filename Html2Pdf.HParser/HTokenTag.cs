using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public class HTokenTag : HToken
    {
        public bool IsOpen { get; protected set; }
        public bool IsClose { get; protected set; }




        private string tagName;
        private string attributesStr;
        private string stylesStr;

        private HTagType tagType;
        public HTagType TagType { get => tagType; private set => tagType = value; }

        private List<HAttribute> attributes;
        public List<HAttribute> Attributes { get => attributes; }

        
        private List<HStyle> styles;
        public List<HStyle> Styles { get => styles; }


        


        public HTokenTag(int pos, string src) : base(pos, src)
        {
            // TODO
            IsOpen = false;
            IsClose = false;

            tagName = "";
            attributesStr = "";
            stylesStr = "";

            tagType = HTagType._unknown;
            attributes = new List<HAttribute>();
            styles = new List<HStyle>();

            parse();
            parseAttributes();
            parseStyles();
        }


        private void parse()
        {
            Regex reClose = new Regex(@"</([^\s]+)\s*>");
            Regex reOpen = new Regex(@"<([^\s]+)\s?(.+)?>");
            Regex reOpenAndClose = new Regex(@"<([^\s]+)\s?(.+)?/>");

            bool success = false;


            if (!success)
            {
                Match mClose = reClose.Match(Src);
                if (mClose.Success)
                {
                    IsOpen = false;
                    IsClose = true;
                    tagName = mClose.Groups[1].Value;
                    tagType = HUtil.GetTagTypeByTagName(tagName);
                    attributesStr = "";

                    success = true;
                }
            }

            if (!success)
            {
                Match mOpen = reOpen.Match(Src);
                if (mOpen.Success)
                {
                    IsOpen = true;
                    IsClose = false;
                    tagName = mOpen.Groups[1].Value;
                    tagType = HUtil.GetTagTypeByTagName(tagName);
                    attributesStr = mOpen.Groups[2].Value;

                    success = true;
                }
            }

            if (!success)
            {
                Match mOpenAndClose = reOpenAndClose.Match(Src);
                if (mOpenAndClose.Success)
                {
                    IsOpen = true;
                    IsClose = true;
                    tagName = mOpenAndClose.Groups[1].Value;
                    tagType = HUtil.GetTagTypeByTagName(tagName);
                    attributesStr = mOpenAndClose.Groups[2].Value;

                    success = true;
                }
            }

            if (!success) throw new HException("Incorrect tag format");

            if (IsOpen && !HUtil.NeedEndTag(tagType))
            {
                IsClose = true;
            }
        }

        private void parseAttributes()
        {
            attributes = new List<HAttribute>();
            
            Regex re = new Regex(@"\s*([^\s]+)\s*=\s*\""([^\""]+)\""\s*");
            MatchCollection matches = re.Matches(attributesStr);

            foreach (Match m in matches)
            {
                if (m.Success)
                {
                    HAttribute attr = new HAttribute();
                    attr.key = m.Groups[1].Value;
                    attr.val = m.Groups[2].Value;

                    attributes.Add(attr);

                    if (attr.key.ToLower() == "style")
                    {
                        stylesStr = attr.val;
                    }
                }
            }
        }

        private void parseStyles()
        {
            styles = new List<HStyle>();

            Regex re = new Regex(@"\s*([^\s]+)\s*\:\s*([^\s]+?)\s*(;|$)");
            MatchCollection matches = re.Matches(stylesStr);

            foreach (Match m in matches)
            {
                if (m.Success)
                {
                    string styleName = m.Groups[1].Value;
                    string val = m.Groups[2].Value;

                    HStyleType styleType = HUtil.GetStyleTypeByStyleName(styleName);

                    if (styleType != HStyleType._unknown)
                    {
                        HStyle style = new HStyle();
                        style.styleType = styleType;
                        style.styleValue = val;

                        styles.Add(style);
                    }
                }
            }
        }


        public override string ToString()
        {
            string desc = base.ToString();

            desc += "\r\n tagType: " + tagType;
            desc += "\r\n tagName: " + tagName;
            desc += "\r\n isOpen: " + IsOpen;
            desc += "\r\n isClose: " + IsClose;

            if (IsOpen)
            {
                desc += "\r\n attributesStr: " + attributesStr;
                desc += "\r\n stylesStr: " + stylesStr;

                desc += "\r\n [Attributes]:";
                foreach (HAttribute attr in attributes)
                {
                    desc += "\r\n  " + attr.ToString();
                }

                desc += "\r\n [Styles]:";
                foreach (HStyle st in styles)
                {
                    desc += "\r\n  " + st.ToString();
                }
            }

            return desc;
        }
    }
}
