using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Aspose.Pdf;
using Aspose.Pdf.Text;
using Html2Pdf.HParser;


namespace Html2Pdf.PCreator
{
    public class PDocument
    {
        private Aspose.Pdf.Document pdfDocument;
        private Aspose.Pdf.Page pdfPage;

        //private HNode rootNode;
        private HNode bodyNode;

        private TextState bodyTextState;



        public PDocument(string fileFullName, HDocument hDocument)
        {
            bodyNode = hDocument.BodyNode;
            bodyTextState = PUtil.TextStateUtil.TextState_FromHStyles((bodyNode as HNodeTag).Styles);


            //throw new PException("file " + fileFullName + " is busy.");

            //debug_helloWorld(fileFullName);
            //debug_createImage(fileFullName);
            //debug_createText(fileFullName);

            //debug_createP(fileFullName);




            pdfDocument = new Document();


            //PExample.Text1(pdfDocument);
            //PExample.Text2(pdfDocument);
            PExample.Text3(pdfDocument);

            //pdfPage = pdfDocument.Pages.Add();
            //PExample.Graph1(pdfPage);
            //PExample.Graph2(pdfPage);
            //PExample.Graph3(pdfPage);
            //PExample.Graph4(pdfPage);




            pdfDocument.Save(fileFullName);
        }







        private void debug_helloWorld(string fileFullName)
        {
            try
            {
                // Initialize document object
                Document document = new Document();
                // Add page
                Page page = document.Pages.Add();
                // Add text to new page
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Hello World!"));
                // Save updated PDF
                document.Save(fileFullName);
            }
            catch (Exception e)
            {
                throw new PException(e.Message);
            }
        }


        private void debug_createImage(string fileFullName)
        {
            Document pdfDocument = new Document();
            Page page = pdfDocument.Pages.Add();

            // Load image into stream
            //FileStream imageStream = new FileStream("https://www.shareicon.net/data/48x48/2016/08/05/806939_document_512x512.png", FileMode.Open);

            string imageUrl = "https://www.shareicon.net/data/48x48/2016/08/05/806939_document_512x512.png";

            /*
            // Creates an HttpWebRequest with the specified URL. 
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
            // Sends the HttpWebRequest and waits for the response.			
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            // Gets the stream associated with the response.
            Stream imageStream = myHttpWebResponse.GetResponseStream();
            */

            
            using (WebClient webClient = new WebClient())
            {
                //var watch = System.Diagnostics.Stopwatch.StartNew();

                byte[] data = webClient.DownloadData(imageUrl);

                
                using (MemoryStream imageStream = new MemoryStream(data))
                {


                    // Add image to Images collection of Page Resources
                    page.Resources.Images.Add(imageStream);



                    //Console.WriteLine(imageData.Length + " bytes received");
                }
                
                //watch.Stop();
                //Console.WriteLine("Total Execution Time :{0}", watch.ElapsedMilliseconds);
            }



            int lowerLeftX = 100;
            int lowerLeftY = 600;
            int upperRightX = 148;
            int upperRightY = 648;

            
            // Using GSave operator: this operator saves current graphics state
            page.Contents.Add(new Operator.GSave());
            // Create Rectangle and Matrix objects
            Aspose.Pdf.Rectangle rectangle = new Aspose.Pdf.Rectangle(lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            Matrix matrix = new Matrix(new double[] { rectangle.URX - rectangle.LLX, 0, 0, rectangle.URY - rectangle.LLY, rectangle.LLX, rectangle.LLY });
            // Using ConcatenateMatrix (concatenate matrix) operator: defines how image must be placed
            page.Contents.Add(new Operator.ConcatenateMatrix(matrix));
            XImage ximage = page.Resources.Images[page.Resources.Images.Count];
            // Using Do operator: this operator draws image
            page.Contents.Add(new Operator.Do(ximage.Name));
            // Using GRestore operator: this operator restores graphics state
            page.Contents.Add(new Operator.GRestore());
            
            // Save updated document
            pdfDocument.Save(fileFullName);
        }



        private void debug_createText(string fileFullName)
        {
            /*
            // Create Document instance
            Document pdfDocument = new Document();
            // Add page to pages collection of Document
            Page page = pdfDocument.Pages.Add();
            // Instantiate TextStamp instance with sample text


            
            TextStamp stamp = new TextStamp("This is text stamp with character spacing");
            // Specify font name for Stamp object
            stamp.TextState.Font = FontRepository.FindFont("Arial");
            // Specify Font size for TextStamp
            stamp.TextState.FontSize = 12;
            // Specify character specing as 1f
            stamp.TextState.CharacterSpacing = 1f;
            // Set the XIndent for Stamp
            stamp.XIndent = 100;
            // Set the YIndent for Stamp
            stamp.YIndent = 500;
            // Add textual stamp to page instance
            stamp.Put(page);

            // Save resulting PDF document.
            pdfDocument.Save(fileFullName);
            */


            /*
            Aspose.Pdf.Document pdfApplicationDoc = new Aspose.Pdf.Document();
            Aspose.Pdf.Page applicationFirstPage = (Aspose.Pdf.Page)pdfApplicationDoc.Pages.Add();

            // Initialize new TextFragment with text containing required newline markers
            Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment("Applicant Name: " + Environment.NewLine + " Joe Smoe");

            // Set text fragment properties if necessary
            textFragment.TextState.FontSize = 12;
            textFragment.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("TimesNewRoman");
            textFragment.TextState.BackgroundColor = Aspose.Pdf.Color.LightGray;
            textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Create TextParagraph object
            TextParagraph par = new TextParagraph();

            // Add new TextFragment to paragraph
            par.AppendLine(textFragment);

            // Set paragraph position
            par.Position = new Aspose.Pdf.Text.Position(100, 600);

            // Create TextBuilder object
            TextBuilder textBuilder = new TextBuilder(applicationFirstPage);
            // Add the TextParagraph using TextBuilder
            textBuilder.AppendParagraph(par);


            // Save resulting PDF document.
            pdfApplicationDoc.Save(fileFullName);
            */


            /*
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();
            Aspose.Pdf.Page pdfPage = (Aspose.Pdf.Page)pdfDocument.Pages.Add();

            // create text paragraph
            TextParagraph paragraph = new TextParagraph();

            // set the paragraph rectangle
            //paragraph.Rectangle = new Rectangle(100, 600, 200, 700);

            // set word wrapping options
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // append string lines
            paragraph.AppendLine("the quick brown fox jumps over the lazy dog");
            paragraph.AppendLine("line2");
            paragraph.AppendLine("line3");

            // append the paragraph to the Pdf page with the TextBuilder
            TextBuilder textBuilder = new TextBuilder(pdfPage);
            textBuilder.AppendParagraph(paragraph);

            pdfDocument.Save(fileFullName);
            */


            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();
            Aspose.Pdf.Page pdfPage = (Aspose.Pdf.Page)pdfDocument.Pages.Add();

            for (int i = 0; i < 200; i++)
            {
                TextFragment line = new TextFragment("Line fragment: " + i);
                
                pdfPage.Paragraphs.Add(line);
            }

            pdfDocument.Save(fileFullName);
        }


        private void debug_createP(string fileFullName)
        {
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();
            Aspose.Pdf.Page pdfPage = (Aspose.Pdf.Page)pdfDocument.Pages.Add();

            Console.WriteLine("pdfPage.Rect = " + pdfPage.Rect);
            Console.WriteLine("pdfPage.PageInfo: Width - Height - PureHeight = " + pdfPage.PageInfo.Width + " - " + pdfPage.PageInfo.Height + " - " + pdfPage.PageInfo.PureHeight);
            Console.WriteLine("pdfPage.PageInfo: Margin.Top - Right - Bottom - Left = " + pdfPage.PageInfo.Margin.Top + " - " + pdfPage.PageInfo.Margin.Right + " - " + pdfPage.PageInfo.Margin.Bottom + " - " + pdfPage.PageInfo.Margin.Left);

            TextParagraph paragraph = new TextParagraph();

            // append string lines
            paragraph.AppendLine("the quick brown fox jumps over the lazy dog");
            paragraph.AppendLine("line2");
            paragraph.AppendLine("line3");


            Console.WriteLine("paragraph.Rectangle = " + paragraph.Rectangle);
            Console.WriteLine("paragraph.TextRectangle = " + paragraph.TextRectangle);



            // Create Graph instance
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph((float)pdfPage.Rect.Width, (float)pdfPage.Rect.Height);
            // Add graph object to paragraphs collection of page instance
            pdfPage.Paragraphs.Add(graph);
            // Create Rectangle instance
            //Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 100, 200, 120);

            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle((float)paragraph.Rectangle.LLX, (float)paragraph.Rectangle.LLY, (float)paragraph.Rectangle.Width, (float)paragraph.Rectangle.Height);
            //Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle((float)paragraph.TextRectangle.LLX, (float)paragraph.TextRectangle.LLY, (float)paragraph.TextRectangle.Width, (float)paragraph.TextRectangle.Height);

            // Specify fill color for Graph object

            //rect.GraphInfo.FillColor = Aspose.Pdf.Color.Red;
            rect.GraphInfo.LineWidth = 2;
            rect.GraphInfo.Color = Aspose.Pdf.Color.Brown;

            // Add rectangle object to shapes collection of Graph object
            graph.Shapes.Add(rect);




            TextBuilder textBuilder = new TextBuilder(pdfPage);
            textBuilder.AppendParagraph(paragraph);


            pdfDocument.Save(fileFullName);
        }

    }
}
