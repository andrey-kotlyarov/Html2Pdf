using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public static partial class HUtil
    {
        public static class TagUtil
        {
            public static bool NeedEndTag(HTagType tagType)
            {
                switch (tagType)
                {
                    case HTagType.br:
                    case HTagType.img:
                    case HTagType.input:
                    case HTagType._unknown:
                        return false;
                    default:
                        return true;
                }
            }


            //TEMPORARY DEBUG
            public static bool IsInlineTag(HTagType tagType)
            {
                switch (tagType)
                {
                    case HTagType.span:
                    case HTagType.br:
                    case HTagType.a:
                    case HTagType.img:
                    case HTagType.input:
                    case HTagType.button:
                        return true;
                    case HTagType._unknown:
                    default:
                        return false;
                }
            }
            //TEMPORARY DEBUG
            public static bool IsBlockTag(HTagType tagType)
            {
                switch (tagType)
                {
                    case HTagType.div:
                    case HTagType.form:
                    case HTagType.p:
                        return true;
                    case HTagType._unknown:
                    default:
                        return false;
                }
            }



            public static string AttributesToString(HNodeTag node)
            {
                string desc = "{Attributes: ";

                foreach (HAttribute attribute in node.Attributes)
                {
                    desc +=  attribute.ToString();
                    desc += "; ";
                }

                desc += "}";

                return desc;
            }

            public static string StylesToString(HNodeTag node)
            {
                string desc = "{Styles: ";

                foreach (HStyle style in node.Styles)
                {
                    desc += style.ToString();
                    desc += "; ";
                }

                desc += "}";

                return desc;
            }

        }
    }
}
