using System;
using System.IO;

using Html2Pdf.Common;
using Html2Pdf.HParser;
using Html2Pdf.PCreator;



namespace Html2Pdf.Console
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            System.Console.WriteLine("Html2Pdf.Console Started.");
            System.Console.WriteLine("");

            string htmlFile = String.Empty;
            string pdfFile = String.Empty;

            if (args.Length == 0)
            {
                htmlFile = GetDataDir() + "test.html";
                //htmlFile = GetDataDir() + "test1.html";
                //htmlFile = GetDataDir() + "test2.html";
            }

            if (args.Length > 0)
            {
                htmlFile = GetDataDir() + args[0];
            }

            if (args.Length > 1)
            {
                pdfFile = GetDataDir() + args[1];
            }
            else
            {
                pdfFile = htmlFile.Replace(".html", ".pdf");
            }
            
            System.Console.WriteLine("Html file: " + htmlFile);
            System.Console.WriteLine("Pdf file: " + pdfFile);
            System.Console.WriteLine("");

            try
            {
                var hDocument = new HDocument(htmlFile);
                var pDocument = new PDocument(pdfFile, hDocument);

                //DEBUG
                //System.Console.Write(hDocument);
                //System.Console.WriteLine("");

                System.Console.WriteLine("PDF File was Created (" + pdfFile + ")");
            }
            catch (H2PException e)
            {
                System.Console.WriteLine("H2P Exception! code: " + e.Code + ", message: " + e.Message);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception! message: " + e.Message);
            }
            
            System.Console.WriteLine("\r\n\r\nProgram Finished. Press any key to exit . . .");
            System.Console.ReadKey();
        }


        public static string GetDataDir()
        {
            var currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());

            if (currentDir.FullName.EndsWith(@"Html2Pdf.Console\bin\Debug"))
            {
                return Path.Combine(currentDir.Parent.Parent.Parent.FullName, @"Data\");
            }
            else
            {
                //return Path.Combine(currentDir.FullName, @"Data\");
                return currentDir.FullName + @"\";
            }

        }
    }
}
