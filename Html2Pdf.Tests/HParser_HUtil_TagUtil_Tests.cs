using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Html2Pdf.HParser;



namespace Html2Pdf.Tests
{

    [TestClass]
    public class HParser_HUtil_TagUtil_Tests
    {
        public TestContext TestContext { get; set; }



        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Data\\TagListData.xml", "TagItem", DataAccessMethod.Sequential)]
        [TestMethod]
        public void test()
        {

        }
        



        
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Data\\TagListData.xml", "TagItem", DataAccessMethod.Sequential)]
        [TestMethod]
        public void needEndTag()
        {
            // arrange
            string tagName = Convert.ToString(TestContext.DataRow["tagName"]);
            HTagType tagType = HUtil.EnumUtil.GetTagTypeByTagName(tagName);
            bool expected = Convert.ToBoolean(TestContext.DataRow["needEndTag"]);

            // act
            bool actual = HUtil.TagUtil.NeedEndTag(tagType);

            // assert
            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual, "TAG: {0}", tagName);
        }

        
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Data\\TagListData.xml", "TagItem", DataAccessMethod.Sequential)]
        [TestMethod]
        public void IsInlineTag()
        {
            // arrange
            string tagName = Convert.ToString(TestContext.DataRow["tagName"]);
            HTagType tagType = HUtil.EnumUtil.GetTagTypeByTagName(tagName);
            bool expected = Convert.ToBoolean(TestContext.DataRow["isInlineTag"]);

            // act
            bool actual = HUtil.TagUtil.IsInlineTag(tagType);

            // assert
            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual, "TAG: {0}", tagName);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "Data\\TagListData.xml", "TagItem", DataAccessMethod.Sequential)]
        [TestMethod]
        public void IsBlockTag()
        {
            // arrange
            string tagName = Convert.ToString(TestContext.DataRow["tagName"]);
            HTagType tagType = HUtil.EnumUtil.GetTagTypeByTagName(tagName);
            bool expected = Convert.ToBoolean(TestContext.DataRow["isBlockTag"]);

            // act
            bool actual = HUtil.TagUtil.IsBlockTag(tagType);

            // assert
            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, actual, "TAG: {0}", tagName);
        }
        


        
    }
}
