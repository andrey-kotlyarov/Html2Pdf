using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Html2Pdf.HParser;



namespace Html2Pdf.Tests
{
    [TestClass]
    public class HParser_HUtil_EnumUtil
    {

        /*
        // TagType(s)
        p
        div
        span
        br
        a
        img
        input
        */

        
        [TestMethod]
        public void GetTagTypeByTagName_P()
        {
            // arrange
            string tagName = "P";
            HTagType expected = HTagType.p;

            // act
            HTagType actual = HUtil.EnumUtil.GetTagTypeByTagName(tagName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTagTypeByTagName_DIV()
        {
            // arrange
            string tagName = "div";
            HTagType expected = HTagType.div;

            // act
            HTagType actual = HUtil.EnumUtil.GetTagTypeByTagName(tagName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTagTypeByTagName_SPAN()
        {
            // arrange
            string tagName = "span";
            HTagType expected = HTagType.span;

            // act
            HTagType actual = HUtil.EnumUtil.GetTagTypeByTagName(tagName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTagTypeByTagName_BR()
        {
            // arrange
            string tagName = "br";
            HTagType expected = HTagType.br;

            // act
            HTagType actual = HUtil.EnumUtil.GetTagTypeByTagName(tagName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTagTypeByTagName_A()
        {
            // arrange
            string tagName = "a";
            HTagType expected = HTagType.a;

            // act
            HTagType actual = HUtil.EnumUtil.GetTagTypeByTagName(tagName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTagTypeByTagName_IMG()
        {
            // arrange
            string tagName = "IMG";
            HTagType expected = HTagType.img;

            // act
            HTagType actual = HUtil.EnumUtil.GetTagTypeByTagName(tagName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTagTypeByTagName_INPUT()
        {
            // arrange
            string tagName = "input";
            HTagType expected = HTagType.input;

            // act
            HTagType actual = HUtil.EnumUtil.GetTagTypeByTagName(tagName);

            // assert
            Assert.AreEqual(expected, actual);
        }







        /*
        // StyleType(s)
        color
        font-family
        font-size
        font-weight
        text-decoration
        */

        [TestMethod]
        public void GetStyleTypeByStyleName_COLOR()
        {
            // arrange
            string styleName = "color";
            HStyleType expected = HStyleType.color;

            // act
            HStyleType actual = HUtil.EnumUtil.GetStyleTypeByStyleName(styleName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStyleTypeByStyleName_FontFamily()
        {
            // arrange
            string styleName = "font-family";
            HStyleType expected = HStyleType.fontFamily;

            // act
            HStyleType actual = HUtil.EnumUtil.GetStyleTypeByStyleName(styleName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStyleTypeByStyleName_FontSize()
        {
            // arrange
            string styleName = "font-size";
            HStyleType expected = HStyleType.fontSize;

            // act
            HStyleType actual = HUtil.EnumUtil.GetStyleTypeByStyleName(styleName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStyleTypeByStyleName_FontWeight()
        {
            // arrange
            string styleName = "font-weight";
            HStyleType expected = HStyleType.fontWeight;

            // act
            HStyleType actual = HUtil.EnumUtil.GetStyleTypeByStyleName(styleName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStyleTypeByStyleName_TextDecoration()
        {
            // arrange
            string styleName = "text-decoration";
            HStyleType expected = HStyleType.textDecoration;

            // act
            HStyleType actual = HUtil.EnumUtil.GetStyleTypeByStyleName(styleName);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
