using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public class HException : Html2Pdf.Common.H2PException
    {
        public HException(string message) : base(Common.H2PExceptionCode.HtmlParserError, message)
        {

        }
    }
}
