using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.PCreator
{
    public class PException : Html2Pdf.Common.H2PException
    {
        public PException(string message) : base(Common.H2PExceptionCode.PdfCreatorError, message)
        {

        }
    }
}
