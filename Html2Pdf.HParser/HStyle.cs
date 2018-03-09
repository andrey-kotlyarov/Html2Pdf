using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public struct HStyle
    {
        public HStyleType styleType;
        public string styleValue;

        public override string ToString()
        {
            string desc = "[Style]";

            desc += " name: '" + HUtil.GetStyleNameByStyleEnum(styleType) + "'";
            desc += "; value: '" + styleValue + "'";

            return desc;
        }
    }
}
