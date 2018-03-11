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
            public static bool NeedEndTag(HTagType tagEnum)
            {
                switch (tagEnum)
                {
                    case HTagType.img:
                    case HTagType.input:
                    case HTagType._unknown:
                        return false;
                    default:
                        return true;
                }
            }
        }
    }
}
