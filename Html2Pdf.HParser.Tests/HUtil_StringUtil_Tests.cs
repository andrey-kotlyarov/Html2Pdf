using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Html2Pdf.HParser.Tests
{
    [TestClass]
    public class HUtil_StringUtil_Tests
    {


        public HUtil_StringUtil_Tests()
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

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "TestData\\StringList.xml", "String", DataAccessMethod.Sequential)]
        [TestMethod]
        public void HtmlAmpersandSequenceDecode()
        {
            // arrange
            string source = Convert.ToString(TestContext.DataRow["source"]);
            string expected = Convert.ToString(TestContext.DataRow["ampDecode"]);
            
            // act
            string actual = HUtil.StringUtil.HtmlAmpersandSequenceDecode(source);

            // assert
            Assert.AreEqual(expected, actual, "SOURCE: {0}", source);
        }
        

    }
}
