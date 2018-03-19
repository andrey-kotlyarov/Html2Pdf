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


            public static readonly Regex Re_TagAttributes = new Regex(@"\s*([^\s]+)\s*=\s*\""([^\""]+)\""\s*");
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

                decodeText = decodeText.Replace("&amp;", "&");
                decodeText = decodeText.Replace("&quot;", "\"");
                decodeText = decodeText.Replace("&nbsp;", " ");
                decodeText = decodeText.Replace("&lt;", "<");
                decodeText = decodeText.Replace("&gt;", ">");

                // TODO - add other Ampersand Sequence
                
                return decodeText;
            }


        }
    }
}
