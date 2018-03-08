using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;



namespace Html2Pdf.HParser
{
    public enum HStyleType
    {
        [Description("")]
        _unknown,
        color,
        [Description("font-family")]
        fontFamily,
        [Description("font-size")]
        fontSize,
        [Description("font-weight")]
        fontWeight,
        [Description("text-decoration")]
        textDecoration
    }
}
