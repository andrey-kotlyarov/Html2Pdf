﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;



namespace Html2Pdf.PCreator
{
    public static class PExample
    {
        //
        // Debug methods
        // for testing functionality Aspose.PDF
        //


        public static void Form1(Document pdfDocument)
        {
            Page pdfPage = pdfDocument.Pages.Add();

            TextFragment textFragment1 = new TextFragment("tF: ");
            //Console.WriteLine("textFragment1.Rectangle.Height: " + textFragment1.Rectangle.Height);
            textFragment1.Margin = new MarginInfo(0, 10-16, 0, 12); // =  textFragment1.Rectangle.Height - formField.Height

            //textFragment1.Margin = new MarginInfo(0, 10-16, 0, 0); // =  textFragment1.Rectangle.Height - formField.Height
            TextBoxField formField = new TextBoxField();

            formField.Height = 16;
            formField.Width = 100;
            formField.Margin = new MarginInfo(0, 12, 0, 12);

            formField.PartialName = "Text"; // ????

            formField.Value = "ttt";
            formField.IsInLineParagraph = true;



            TextFragment textFragment2 = new TextFragment("Tf 2.");
            textFragment2.IsInLineParagraph = true;





            pdfPage.Paragraphs.Add(textFragment1);
            pdfPage.Paragraphs.Add(formField);
            pdfPage.Paragraphs.Add(textFragment2);




        }


        public static void Text4(Document pdfDocument)
        {
            Page pdfPage = pdfDocument.Pages.Add();

            TextState defaultTextState = new TextState();
            defaultTextState.ForegroundColor = Aspose.Pdf.Color.Brown;
            defaultTextState.Font = FontRepository.FindFont("TimesNewRoman");
            defaultTextState.FontStyle = FontStyles.Regular;
            defaultTextState.FontSize = 13;

            defaultTextState.StrikeOut = false;
            defaultTextState.Underline = false;

            //pdfPage.PageInfo.Margin = new MarginInfo(0, 0, 0, 12);
            //pdfPage.PageInfo.DefaultTextState = defaultTextState;



            TextFragment textFragment = new TextFragment();


            TextSegment ts1 = new TextSegment();
            ts1.TextState = defaultTextState;
            ts1.Text = "First line";


            TextSegment ts2 = new TextSegment();
            ts2.TextState = defaultTextState;
            ts2.Text = "Second line";

            TextSegment ts_br = new TextSegment();
            ts_br.Text = Environment.NewLine;

            textFragment.Segments.Add(ts1);
            textFragment.Segments.Add(ts_br);
            textFragment.Segments.Add(ts2);

            pdfPage.Paragraphs.Add(textFragment);
        }


        public static void Text3(Document pdfDocument)
        {
            Page pdfPage = pdfDocument.Pages.Add();

            TextState defaultTextState = new TextState();
            defaultTextState.ForegroundColor = Aspose.Pdf.Color.Brown;
            defaultTextState.Font = FontRepository.FindFont("Times");
            defaultTextState.FontStyle = FontStyles.Regular;
            defaultTextState.FontSize = 13;

            defaultTextState.StrikeOut = false;
            defaultTextState.Underline = true;
            

            pdfPage.PageInfo.Margin = new MarginInfo(0, 0, 0, 12);
            pdfPage.PageInfo.DefaultTextState = defaultTextState;

            pdfPage.Background = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9);


            int pageCount = 1;
            for (int i = 1; i <= 100; i++)
            {
                TextFragment textFragment = new TextFragment("Text Fragment " + i + ".");

                //textFragment.IsFirstParagraphInColumn = true; // one item per page
                //textFragment.IsInLineParagraph = true;
                //textFragment.IsInNewPage = true;

                //textFragment.TextState.ForegroundColor = pdfDocument.PageInfo.DefaultTextState.ForegroundColor;
                //textFragment.Margin = new MarginInfo(12, 0, 6, 12);
                textFragment.Margin = new MarginInfo(12, 0, 6, 12);

                /*
                TextFragment textFragment2 = new TextFragment(" Text Fragment (2) " + i + ".");
                TextState textState = new TextState();
                textState.ApplyChangesFrom(defaultTextState);
                textState.ForegroundColor = Aspose.Pdf.Color.Gray;
                textState.FontStyle = FontStyles.Bold;
                textState.Underline = false;
                textState.FontSize = 7;
                
                
                textFragment2.TextState.ApplyChangesFrom(textState);
                textFragment2.Margin = new MarginInfo(0, 0, 0, 0);

                //Console.WriteLine(textFragment2.BaselinePosition);
                //Console.WriteLine(textFragment.BaselinePosition);
                //Console.WriteLine(textFragment2.Rectangle);
                //Console.WriteLine(textFragment.Rectangle);


                textFragment2.IsInLineParagraph = true;
                //textFragment2.BaselinePosition = new Position(100, 300);
                //textFragment2.Position = new Position(100, 200);
                */

                TextSegment textSegment = new TextSegment("Text Segment. ");
                TextState textState = new TextState();
                textState.ApplyChangesFrom(defaultTextState);
                textState.ForegroundColor = Aspose.Pdf.Color.Gray;
                textState.FontStyle = FontStyles.Bold;
                textState.Underline = false;
                textState.FontSize = 7;
                
                textSegment.TextState.ApplyChangesFrom(textState);
                textFragment.Segments.Add(textSegment);





                TextSegment textSegment2 = new TextSegment("Text Segment 2");
                TextState textState2 = new TextState();
                textState2.ApplyChangesFrom(defaultTextState);
                textState2.ForegroundColor = Aspose.Pdf.Color.Green;
                textState2.FontStyle = FontStyles.Regular;
                textState2.Underline = false;
                textState2.FontSize = 18;

                textSegment2.TextState.ApplyChangesFrom(textState2);
                textFragment.Segments.Add(textSegment2);



                pdfPage.Paragraphs.Add(textFragment);
                //pdfPage.Paragraphs.Add(textFragment2);
                

                pdfDocument.ProcessParagraphs();
                if (pdfDocument.Pages.Count > pageCount)
                {
                    pageCount = pdfDocument.Pages.Count;
                    pdfPage = pdfDocument.Pages[pageCount];

                    pdfPage.PageInfo.Margin = new MarginInfo(0, 0, 0, 12);
                    pdfPage.PageInfo.DefaultTextState = defaultTextState;

                    pdfPage.Background = Aspose.Pdf.Color.FromRgb(0.90, 1.00, 0.90);
                }


                //Console.WriteLine("page num: " + pdfPage.Number);
                //Console.WriteLine("page count: " + pdfDocument.Pages.Count);
            }


        }



        public static void Text2(Document pdfDocument)
        {
            Page pdfPage = pdfDocument.Pages.Add();

            int pos = 0;
            for (int i = 1; i <= 100; i++)
            {
                TextParagraph textParagraph = new TextParagraph();

                TextFragment textFragment1 = new TextFragment("Text Fragment (1) " + i + " ");
                //textFragment.Margin.Top = 8;
                //textFragment1.IsInLineParagraph = true;

                TextFragment textFragment2 = new TextFragment("Text Fragment (2) " + i + " ");
                //textFragment2.IsInLineParagraph = true;

                textParagraph.AppendLine(textFragment1/*, textState*/);
                //textParagraph.AppendLine(textFragment2/*, textState*/);
                textParagraph.Position = new Position(20, pdfDocument.PageInfo.Height - (i - pos) * (textFragment1.Rectangle.Height + 8));

                if (textParagraph.Position.YIndent < 0)
                {
                    pdfPage = pdfDocument.Pages.Add();
                    pos = i - 1;
                    textParagraph.Position = new Position(20, pdfDocument.PageInfo.Height - (i - pos) * (textFragment1.Rectangle.Height + 8));
                }


                TextBuilder textBuilder = new TextBuilder(pdfPage);
                textBuilder.AppendParagraph(textParagraph);

                
            }

        }


        public static void Text1(Document pdfDocument)
        {
            //??
            //pdfDocument.PageInfo.DefaultTextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.00, 0.50, 0.00);

            //pdfDocument.PageInfo.Margin.Left = pdfDocument.PageInfo.Margin.Right = pdfDocument.PageInfo.Margin.Bottom = pdfDocument.PageInfo.Margin.Top = 0;
            //pdfDocument.PageInfo.Margin.Top = 12;
            //pdfDocument.PageInfo.Margin = new MarginInfo(0, 0, 0, 12);


            Page pdfPage = pdfDocument.Pages.Add();
            //pdfPage.PageInfo.Margin.Left = pdfPage.PageInfo.Margin.Right = pdfPage.PageInfo.Margin.Bottom = pdfPage.PageInfo.Margin.Top = 0;
            //pdfPage.PageInfo.Margin.Top = 12;


            pdfPage.PageInfo.DefaultTextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.00, 0.50, 0.00);
            pdfPage.PageInfo.Margin = new MarginInfo(0, 0, 0, 12);
            pdfPage.Background = Aspose.Pdf.Color.FromRgb(0.90, 1.00, 0.90);

            //int pageNum = 0;


            //for (int i = 1; i <= 100; i++)
            for (int i = 1; i <= 3; i++)
            {
                TextFragment textFragment = new TextFragment("Text Fragment " + i + " ");

                //textFragment.IsFirstParagraphInColumn = true; // one item per page
                //textFragment.IsInLineParagraph = true;
                //textFragment.IsInNewPage = true;

                //textFragment.TextState.ForegroundColor = pdfDocument.PageInfo.DefaultTextState.ForegroundColor;
                textFragment.Margin = new MarginInfo(12, 0, 6, 12);


                TextSegment textSegment1 = new TextSegment("segment 1_ " + i + ", ");
                textFragment.Segments.Add(textSegment1);

                
                TextSegment textSegment2 = new TextSegment("segment 2_ " + i + ".");
                textFragment.Segments.Add(textSegment2);

                textSegment2.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                textSegment2.TextState.FontSize = 18;
                textSegment2.TextState.FontStyle = FontStyles.Bold;
                textSegment2.TextState.Font = FontRepository.FindFont("Times");

                pdfPage.Paragraphs.Add(textFragment);

                /*
                if (pdfPage.Number > pageNum)
                {
                    pdfPage.Background = Aspose.Pdf.Color.FromRgb(0.90, 1.00, 0.90);
                    pageNum++;
                }
                */
                /*
                if (pdfDocument.Pages.Count > pageNum)
                {
                    pageNum = pdfDocument.Pages.Count;
                    pdfDocument.Pages[pageNum].Background = Aspose.Pdf.Color.FromRgb(0.90, 1.00, 0.90);
                    //pdfPage = pdfDocument.Pages[pageNum];
                    //pdfPage.Background = Aspose.Pdf.Color.FromRgb(0.90, 1.00, 0.90);
                }
                */

                pdfPage = pdfDocument.Pages.Add();
                pdfPage.PageInfo.DefaultTextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.00, 0.50, 0.00);
                pdfPage.PageInfo.Margin = new MarginInfo(0, 0, 0, 12);
                pdfPage.Background = Aspose.Pdf.Color.FromRgb(0.80, 1.00, 0.80);

                //pdfPage.PageInfo.Margin.Left = pdfPage.PageInfo.Margin.Right = pdfPage.PageInfo.Margin.Bottom = pdfPage.PageInfo.Margin.Top = 0;
                //pdfPage.PageInfo.Margin.Top = 12;

            }

            


        }




        public static void Graph4(Page pdfPage)
        {
            //pdfPage.PageInfo.Margin.Left = pdfPage.PageInfo.Margin.Right = pdfPage.PageInfo.Margin.Bottom = pdfPage.PageInfo.Margin.Top = 0;
            pdfPage.PageInfo.Margin.Left = pdfPage.PageInfo.Margin.Right = pdfPage.PageInfo.Margin.Bottom = pdfPage.PageInfo.Margin.Top = 20;

            // Create Graph instance
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(200, 400);
            
            // Add graph object to paragraphs collection of page instance
            pdfPage.Paragraphs.Add(graph);

            // Create Rectangle instance
            //Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 100, 200, 120);
            //Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 400);
            Aspose.Pdf.Drawing.Rectangle rect1 = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 400);
            Aspose.Pdf.Drawing.Rectangle rect2 = new Aspose.Pdf.Drawing.Rectangle(100, 0, 100, 200);

            // Specify fill color for Graph object
            rect1.GraphInfo.FillColor = Aspose.Pdf.Color.Blue;
            rect2.GraphInfo.FillColor = Aspose.Pdf.Color.Red;

            // Add rectangle object to shapes collection of Graph object
            graph.Shapes.Add(rect1);
            graph.Shapes.Add(rect2);
        }



        public static void Graph3(Page pdfPage)
        {
            pdfPage.PageInfo.Margin.Left = pdfPage.PageInfo.Margin.Right = pdfPage.PageInfo.Margin.Bottom = pdfPage.PageInfo.Margin.Top = 0;
            // Create Graph object with Width and Height equal to page dimensions
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph((float)pdfPage.PageInfo.Width, (float)pdfPage.PageInfo.Height);
            // Create first line object starting from Lower-Left to Top-Right corner of page
            Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(new float[] { (float)pdfPage.Rect.LLX, 0, (float)pdfPage.PageInfo.Width, (float)pdfPage.Rect.URY });
            // Add line to shapes collection of Graph object
            graph.Shapes.Add(line);
            // Draw line from Top-Left corner of page to Bottom-Right corner of page
            Aspose.Pdf.Drawing.Line line2 = new Aspose.Pdf.Drawing.Line(new float[] { 0, (float)pdfPage.Rect.URY, (float)pdfPage.PageInfo.Width, (float)pdfPage.Rect.LLX });
            // Add line to shapes collection of Graph object
            graph.Shapes.Add(line2);
            // Add Graph object to paragraphs collection of page
            pdfPage.Paragraphs.Add(graph);
        }




        public static void Graph2(Page pdfPage)
        {
            int alpha = 10;
            int green = 0;
            int red = 100;
            int blue = 0;
            // Create Color object using Alpha RGB 
            Aspose.Pdf.Color alphaColor = Aspose.Pdf.Color.FromArgb(alpha, red, green, blue); // Provide alpha channel
                                                                                              // Instantiate Document object
            // Create Graph object with certain dimensions
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(300, 400);
            // Set border for Drawing object
            graph.Border = (new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, Aspose.Pdf.Color.Black));
            // Add graph object to paragraphs collection of Page instance
            pdfPage.Paragraphs.Add(graph);
            // Create Rectangle object with certain dimensions
            Aspose.Pdf.Drawing.Rectangle rectangle = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            // Create graphInfo object for Rectangle instance
            Aspose.Pdf.GraphInfo graphInfo = rectangle.GraphInfo;
            // Set color information for GraphInfo instance
            graphInfo.Color = (Aspose.Pdf.Color.Red);
            // Set fill color for GraphInfo
            graphInfo.FillColor = (alphaColor);
            // Add rectangle shape to shapes collection of graph object
            graph.Shapes.Add(rectangle);
        }
        
        

        
        public static void Graph1(Page pdfPage)
        {
            pdfPage.SetPageSize(375, 300);
            // Set left margin for page object as 0
            pdfPage.PageInfo.Margin.Left = 0;
            // Set top margin of page object as 0
            pdfPage.PageInfo.Margin.Top = 0;
            // Create a new rectangle with Color as Red, Z-Order as 0 and certain dimensions
            AddRectangle(pdfPage, 50, 40, 60, 40, Aspose.Pdf.Color.Red, 2);
            // Create a new rectangle with Color as Blue, Z-Order as 0 and certain dimensions
            AddRectangle(pdfPage, 20, 20, 30, 30, Aspose.Pdf.Color.Blue, 1);
            // Create a new rectangle with Color as Green, Z-Order as 0 and certain dimensions
            AddRectangle(pdfPage, 40, 40, 60, 30, Aspose.Pdf.Color.Green, 0);
        }

        private static void AddRectangle(Aspose.Pdf.Page page, float x, float y, float width, float height, Aspose.Pdf.Color color, int zindex)
        {
            // Create graph object with dimensions same as specified for Rectangle object
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(width, height);
            // Can we change the position of graph instance
            graph.IsChangePosition = false;
            // Set Left coordinate position for Graph instance
            graph.Left = x;
            // Set Top coordinate position for Graph object
            graph.Top = y;
            // Add a rectangle inside the "graph"
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, width, height);
            // Set rectangle fill color
            rect.GraphInfo.FillColor = color;
            // Color of graph object
            rect.GraphInfo.Color = color;
            // Add rectangle to shapes collection of graph instance
            graph.Shapes.Add(rect);
            // Set Z-Index for rectangle object
            graph.ZIndex = zindex;
            // Add graph to paragraphs collection of page object
            page.Paragraphs.Add(graph);
        }
        
    }
}
