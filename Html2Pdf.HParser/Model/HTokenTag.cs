using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public class HTokenTag : HToken
    {

        private bool isOpen;
        private bool isClose;
       

        public bool IsOpen { get => isOpen; }
        public bool IsClose { get => isClose; }




        private string tagName;
        private string attributesStr;
        private string stylesStr;

        private HTagType tagType;
        public HTagType TagType { get => tagType; private set => tagType = value; }

        private IEnumerable<HAttribute> attributes;
        public List<HAttribute> Attributes { get => attributes as List<HAttribute>; }

        
        private IEnumerable<HStyle> styles;
        public List<HStyle> Styles { get => styles as List<HStyle>; }


        


        public HTokenTag(int pos, string src) : base(pos, src)
        {
            isOpen = false;
            isClose = false;

            tagName = "";
            attributesStr = "";
            stylesStr = "";

            tagType = HTagType._unknown;
            attributes = new List<HAttribute>();
            styles = new List<HStyle>();

            parse();
            parseAttributes();
            parseStyles();

            createNode();
        }


        private void parse()
        {
            bool success = false;
            
            if (!success)
            {
                Match mClose = HUtil.StringUtil.Re_TokenTagClose.Match(Src);

                if (mClose.Success)
                {
                    isOpen = false;
                    isClose = true;
                    tagName = mClose.Groups[1].Value;
                    tagType = HUtil.EnumUtil.GetTagTypeByTagName(tagName);
                    attributesStr = "";

                    success = true;
                }
            }

            if (!success)
            {
                Match mOpen = HUtil.StringUtil.Re_TokenTagOpen.Match(Src);
                
                if (mOpen.Success)
                {
                    isOpen = true;
                    isClose = false;
                    tagName = mOpen.Groups[1].Value;
                    tagType = HUtil.EnumUtil.GetTagTypeByTagName(tagName);
                    attributesStr = mOpen.Groups[2].Value;

                    success = true;
                }
            }

            if (!success)
            {
                Match mOpenAndClose = HUtil.StringUtil.Re_TokenTagSole.Match(Src);

                if (mOpenAndClose.Success)
                {
                    isOpen = true;
                    isClose = true;
                    tagName = mOpenAndClose.Groups[1].Value;
                    tagType = HUtil.EnumUtil.GetTagTypeByTagName(tagName);
                    attributesStr = mOpenAndClose.Groups[2].Value;

                    success = true;
                }
            }

            if (!success) throw new HException("Incorrect tag format");

            if (IsOpen && !HUtil.TagUtil.NeedEndTag(tagType))
            {
                isClose = true;
            }
        }

        private void parseAttributes()
        {
            attributes = new List<HAttribute>();

            MatchCollection matches = HUtil.StringUtil.Re_TagAttributes.Matches(attributesStr);

            foreach (Match m in matches)
            {
                if (m.Success)
                {
                    HAttribute attr = new HAttribute()
                    {
                        key = m.Groups[1].Value,
                        val = HUtil.StringUtil.HtmlAmpersandSequenceDecode(m.Groups[2].Value)
                    };


                    (attributes as List<HAttribute>).Add(attr);

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

            MatchCollection matches = HUtil.StringUtil.Re_TagStyles.Matches(stylesStr);

            foreach (Match m in matches)
            {
                if (m.Success)
                {
                    string styleName = m.Groups[1].Value;
                    string value = m.Groups[2].Value;

                    HStyleType styleType = HUtil.EnumUtil.GetStyleTypeByStyleName(styleName);

                    if (styleType != HStyleType._unknown)
                    {
                        HStyle style = new HStyle()
                        {
                            styleType = styleType,
                            styleValue = value
                        };

                        (styles as List<HStyle>).Add(style);
                    }
                }
            }
        }


        protected override void createNode()
        {
            if (IsOpen)
            {
                if (IsClose)
                {
                    node = new HNodeSole(tagType, attributes, styles);

                    nodeWasCollected = false;
                    nodeReadyToCollect = true;
                }
                else
                {
                    node = new HNodeElement(tagType, attributes, styles);

                    nodeWasCollected = false;
                    nodeReadyToCollect = false;
                }
            }
            else
            {
                node = null;

                nodeWasCollected = false;
                nodeReadyToCollect = false;
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
