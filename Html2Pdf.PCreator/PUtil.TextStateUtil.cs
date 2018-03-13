using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Aspose.Pdf.Text;
using Html2Pdf.HParser;


namespace Html2Pdf.PCreator
{
    public static partial class PUtil
    {
        public static class TextStateUtil
        {
            public static TextState TextState_Default()
            {
                TextState defaultTextState = new TextState();
                defaultTextState.ForegroundColor = Aspose.Pdf.Color.Black;
                defaultTextState.Font = FontRepository.FindFont("Times");
                defaultTextState.FontStyle = FontStyles.Regular;
                defaultTextState.FontSize = 13;

                defaultTextState.StrikeOut = false;
                defaultTextState.Underline = false;

                return defaultTextState;
            }


            public static TextState TextState_FromHStyles(IEnumerable<HStyle> styles)
            {
                TextState textState = new TextState();
                textState.ApplyChangesFrom(TextState_Default());

                foreach (HStyle style in styles)
                {
                    switch (style.styleType)
                    {
                        case HStyleType.color:
                            textState.ForegroundColor = GetColor(style.styleValue);
                            break;
                        case HStyleType.fontFamily:
                            textState.Font = GetFont(style.styleValue);
                            break;
                        case HStyleType.fontSize:
                            textState.FontSize = GetFontSize(style.styleValue);
                            break;
                        case HStyleType.fontWeight:
                            //
                            break;
                        case HStyleType.textDecoration:
                            SetTextDecoration(textState, style.styleValue);
                            break;
                        case HStyleType._unknown:
                        default:
                            break;
                    }
                }

                return textState;
            }


            public static Aspose.Pdf.Color GetColor(string strColor)
            {
                Aspose.Pdf.Color color = Aspose.Pdf.Color.Black;

                if (String.IsNullOrEmpty(strColor)) return color;


                if (strColor[0] == '#' && strColor.Length == 7)
                {
                    color = Aspose.Pdf.Color.FromRgb(System.Drawing.ColorTranslator.FromHtml(strColor));
                }
                else if (strColor[0] == '#' && strColor.Length == 4)
                {
                    color = Aspose.Pdf.Color.FromRgb(System.Drawing.ColorTranslator.FromHtml(strColor));
                }
                else
                {
                    try
                    {
                        color = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.FromName(strColor));
                    }
                    catch { }
                }

                return color;
            }

            public static Aspose.Pdf.Text.Font GetFont(string strFont)
            {
                Aspose.Pdf.Text.Font font = FontRepository.FindFont("Times");

                try
                {
                    font = FontRepository.FindFont(strFont, true);
                }
                catch { }

                return font;
            }

            public static float GetFontSize(string strFontSize)
            {
                float fontSize = TextState_Default().FontSize;

                Regex re = new Regex(@"^(\d*\.?\d+)(.*)$");

                Match m = re.Match(strFontSize);
                if (m.Success)
                {
                    float size = (float)Convert.ToDouble(m.Groups[1].Value);

                    switch (m.Groups[2].Value.Trim().ToLower())
                    {
                        case "em": // Relative to the font-size of the element (2em means 2 times the size of the current font)
                            fontSize = fontSize * size;
                            break;
                        //TODO
                        case "ex": // Relative to the x-height of the current font (rarely used)
                            fontSize = fontSize * size;
                            break;
                        //TODO
                        case "px": // pixels (1px = 1/96th of 1in)
                            fontSize = fontSize * size;
                            break;
                        //TODO
                        case "cm":
                            fontSize = fontSize * size;
                            break;
                        //TODO
                        case "mm":
                            fontSize = fontSize * size;
                            break;
                        //TODO
                        case "in": // inches (1in = 96px = 2.54cm)
                            fontSize = fontSize * size;
                            break;
                        case "pc": // picas (1pc = 12 pt)
                            fontSize = size * 12F;
                            break;
                        //TODO
                        case "ch": // Relative to width of the "0" (zero)
                        case "rem": // Relative to font-size of the root element
                        case "vh": // Relative to 1% of the width of the viewport*
                        case "vw": // Relative to 1% of the height of the viewport*
                        case "vmin": // Relative to 1% of viewport's* smaller dimension
                        case "vmax": // Relative to 1% of viewport's* larger dimension
                            fontSize = size;
                            break;
                        case "pt": // points (1pt = 1/72 of 1in)
                        default:
                            fontSize = size;
                            break;
                    }
                }
                else
                {
                    switch (strFontSize.Trim().ToLower())
                    {
                        case "medium":
                            fontSize = fontSize * 1.00F;
                            break;
                        case "xx-small":
                            fontSize = fontSize * 1.00F;
                            break;
                        case "x-small":
                            fontSize = fontSize * 1.00F;
                            break;
                        case "small":
                            fontSize = fontSize * 1.00F;
                            break;
                        case "large":
                            fontSize = fontSize * 1.00F;
                            break;
                        case "x-large":
                            fontSize = fontSize * 1.00F;
                            break;
                        case "xx-large":
                            fontSize = fontSize * 1.00F;
                            break;
                        case "smaller":
                            fontSize = fontSize * 1.00F;
                            break;
                        case "larger":
                            fontSize = fontSize * 1.00F;
                            break;
                        default:
                            break;
                    }
                }



                return fontSize;
            }

            public static void SetTextDecoration(TextState textState, string strTextDecoration)
            {

            }

        }
    }
}
