using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace Html2Pdf.HParser.Tests
{
    /// <summary>
    /// Summary description for HUtil_TagUtil_Tests
    /// </summary>
    [TestClass]
    public class HUtil_TagUtil_Tests
    {
        public HUtil_TagUtil_Tests()
        {
            //
            // Constructor logic here
            //
        }


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get => testContextInstance; set => testContextInstance = value; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "TestData\\TagList.xml", "Tag", DataAccessMethod.Sequential)]
        [TestMethod]
        public void NeedEndTag()
        {
            // arrange
            string tagName = Convert.ToString(TestContext.DataRow["tagName"]);
            HTagType tagType = HUtil.EnumUtil.GetTagTypeByTagName(tagName);
            bool expected = Convert.ToBoolean(TestContext.DataRow["needEndTag"]);

            // act
            bool actual = HUtil.TagUtil.NeedEndTag(tagType);

            // assert
            Assert.AreEqual(expected, actual, "TAG: {0}", tagName);
        }


        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "TestData\\TagList.xml", "Tag", DataAccessMethod.Sequential)]
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
            Assert.AreEqual(expected, actual, "TAG: {0}", tagName);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "TestData\\TagList.xml", "Tag", DataAccessMethod.Sequential)]
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
            Assert.AreEqual(expected, actual, "TAG: {0}", tagName);
        }
    }
}
