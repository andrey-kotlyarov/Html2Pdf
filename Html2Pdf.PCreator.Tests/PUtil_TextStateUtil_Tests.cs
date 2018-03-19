using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace Html2Pdf.PCreator.Tests
{
    [TestClass]
    public class PUtil_TextStateUtil_Tests
    {
        public PUtil_TextStateUtil_Tests()
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

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "TestData\\FontList.xml", "Font", DataAccessMethod.Sequential)]
        [TestMethod]
        public void GetFontSize()
        {
            // arrange
            string fontName = Convert.ToString(TestContext.DataRow["fontName"]);
            string fontSize = Convert.ToString(TestContext.DataRow["fontSize"]);
            float expected = (float)Convert.ToDouble(TestContext.DataRow["pdfSize"], new CultureInfo("en-US"));
            float delta = 0.01F;

            // act
            float actual = PUtil.TextStateUtil.GetFontSize(fontSize);

            // assert
            Assert.AreEqual(expected, actual, delta, "FONT: {0}, FONT_SIZE: {1}", fontName, fontSize);
        }
    }
}
