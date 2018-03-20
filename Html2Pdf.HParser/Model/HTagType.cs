using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;


namespace Html2Pdf.HParser
{
    public enum HTagType
    {
        [Description("")]
        _unknown,
        html,
        head,
        title,
        body,
        p,
        div,
        span,
        br,
        b,
        i,
        a,
        img,
        form,
        input,
        button
    }
}
