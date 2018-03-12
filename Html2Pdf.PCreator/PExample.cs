using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Aspose.Pdf;



namespace Html2Pdf.PCreator
{
    public static class PExample
    {




        public static void Graph4(Page pdfPage)
        {
            pdfPage.PageInfo.Margin.Left = pdfPage.PageInfo.Margin.Right = pdfPage.PageInfo.Margin.Bottom = pdfPage.PageInfo.Margin.Top = 0;

            // Create Graph instance
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(200, 400);
            
            // Add graph object to paragraphs collection of page instance
            pdfPage.Paragraphs.Add(graph);
            // Create Rectangle instance
            //Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 100, 200, 120);
            //Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 400);
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 0, 100, 200);

            // Specify fill color for Graph object
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.Red;
            // Add rectangle object to shapes collection of Graph object
            graph.Shapes.Add(rect);
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
