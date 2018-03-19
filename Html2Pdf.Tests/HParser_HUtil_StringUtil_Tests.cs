using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Html2Pdf.HParser;


namespace Html2Pdf.Tests
{
    [TestClass]
    public class HParser_HUtil_StringUtil_Tests
    {
        [TestMethod]
        public void HtmlAmpersandSequenceDecode_1()
        {
            // arrange
            string str = "&lt;&amp;&gt;";
            string expected = "<&>";

            // act
            string actual = HUtil.StringUtil.HtmlAmpersandSequenceDecode(str);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HtmlAmpersandSequenceDecode_2()
        {
            // arrange
            string str = "&quot; &nbsp; &quot;";
            string expected = @"""   """;

            // act
            string actual = HUtil.StringUtil.HtmlAmpersandSequenceDecode(str);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
