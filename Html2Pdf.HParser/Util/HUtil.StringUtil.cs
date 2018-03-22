using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Html2Pdf.HParser
{
    public static partial class HUtil
    {
        public static class StringUtil
        {
            public static readonly Regex Re_TokenComment = new Regex(@"<!--.*?-->");

            public static readonly Regex Re_TokenFirstText = new Regex(@"^[^<>]+");
            public static readonly Regex Re_TokenFirstTag = new Regex(@"^<[^<>]+>");
            
            public static readonly Regex Re_TokenTagClose = new Regex(@"</([^\s]+)\s*>");
            public static readonly Regex Re_TokenTagOpen = new Regex(@"<([^\s]+)\s?(.+)?>");
            public static readonly Regex Re_TokenTagSole = new Regex(@"<([^\s]+)\s?(.+)?/>");

            public static readonly Regex Re_TagAttributes = new Regex(@"\s*([^\s]+)\s*=\s*[""']([^""']+)[""']\s*");
            public static readonly Regex Re_TagStyles = new Regex(@"\s*([^\s]+)\s*\:\s*([^\s]+?)\s*(;|$)");



            // Text To Single Line String 
            public static string ToSingleLine(string text)
            {
                string singleLineText = text.Replace("\r\n", " ");
                singleLineText = singleLineText.Replace("\n\r", " ");
                singleLineText = singleLineText.Replace("\n", " ").Replace("\r", " ");

                return singleLineText;
            }
            
            // HTML Ampersand Sequence Character Codes
            public static string HtmlAmpersandSequenceDecode(string text)
            {
                string decodeText = text;

                Regex reSpaces = new Regex(@"\s{2,}");
                decodeText = reSpaces.Replace(decodeText, " ");


                decodeText = decodeText.Replace("&nbsp;", " ");
                //decodeText = decodeText.Replace("&NBSP;", " ");
                decodeText = decodeText.Replace("&#160;", " ");
                decodeText = decodeText.Replace("&#xa0;", " ");
                decodeText = decodeText.Replace("&#xA0;", " ");
                decodeText = decodeText.Replace("&#Xa0;", " ");
                decodeText = decodeText.Replace("&#XA0;", " ");

                decodeText = decodeText.Replace("&quot;", "\"");
                decodeText = decodeText.Replace("&QUOT;", "\"");
                decodeText = decodeText.Replace("&#34;", "\"");
                decodeText = decodeText.Replace("&#x22;", "\"");
                decodeText = decodeText.Replace("&#X22;", "\"");

                decodeText = decodeText.Replace("&amp;", "&");
                decodeText = decodeText.Replace("&AMP;", "&");
                decodeText = decodeText.Replace("&#38;", "&");
                decodeText = decodeText.Replace("&#x26;", "&");
                decodeText = decodeText.Replace("&#X26;", "&");

                decodeText = decodeText.Replace("&lt;", "<");
                decodeText = decodeText.Replace("&LT;", "<");
                decodeText = decodeText.Replace("&#60;", "<");
                decodeText = decodeText.Replace("&#x3c;", "<");
                decodeText = decodeText.Replace("&#x3C;", "<");
                decodeText = decodeText.Replace("&#X3c;", "<");
                decodeText = decodeText.Replace("&#X3C;", "<");

                decodeText = decodeText.Replace("&gt;", ">");
                decodeText = decodeText.Replace("&GT;", ">");
                decodeText = decodeText.Replace("&#62;", ">");
                decodeText = decodeText.Replace("&#x3e;", ">");
                decodeText = decodeText.Replace("&#x3E;", ">");
                decodeText = decodeText.Replace("&#X3e;", ">");
                decodeText = decodeText.Replace("&#X3E;", ">");


                decodeText = decodeText.Replace("&copy;", "©");
                decodeText = decodeText.Replace("&COPY;", "©");
                decodeText = decodeText.Replace("&#169;", "©");
                decodeText = decodeText.Replace("&#xa9;", "©");
                decodeText = decodeText.Replace("&#xA9;", "©");
                decodeText = decodeText.Replace("&#Xa9;", "©");
                decodeText = decodeText.Replace("&#XA9;", "©");

                // TODO - add other Ampersand Sequence

                return decodeText;
            }


        }
    }
}
