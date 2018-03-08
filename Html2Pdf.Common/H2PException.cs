using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.Common
{
    abstract public class H2PException : Exception
    {
        public H2PExceptionCode Code { get; private set; }

        public H2PException(H2PExceptionCode code, string mess) : base(mess)
        {
            Code = code;
        }
    }
}
