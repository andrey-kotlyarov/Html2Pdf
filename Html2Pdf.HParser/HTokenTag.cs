using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public class HTokenTag : HToken
    {
        public HTagType tagType { get; private set; }


        public bool IsOpen { get; protected set; }
        public bool IsClose { get; protected set; }

        
        private List<HAttribute> attributes;
        public List<HAttribute> Attributes { get => attributes; }

        
        private List<HStyle> styles;
        public List<HStyle> Styles { get => styles; }


        private string tagName;
        private string attributesStr;


        public HTokenTag(int pos, string src) : base(pos, src)
        {
            // TODO
            IsOpen = false;
            IsClose = false;

            tagType = HTagType._unknown;
            attributes = new List<HAttribute>();
            styles = new List<HStyle>();

            parse();
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

        public override string ToString()
        {
            string desc = base.ToString();

            desc += "\r\n tagType: " + tagType;
            desc += "\r\n tagName: " + tagName;
            desc += "\r\n isOpen: " + IsOpen;
            desc += "\r\n isClose: " + IsClose;
            desc += "\r\n attributesStr: " + attributesStr;
            

            return desc;
        }
    }
}
