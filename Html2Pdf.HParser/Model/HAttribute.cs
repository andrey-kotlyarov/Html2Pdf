﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public struct HAttribute
    {
        public string key;
        public string val;


        public override string ToString()
        {
            string desc = "[Attribute]";

            desc += " - key: '" + key + "'";
            desc += " - val: '" + val + "'";

            return desc;
        }
    }


    
}
