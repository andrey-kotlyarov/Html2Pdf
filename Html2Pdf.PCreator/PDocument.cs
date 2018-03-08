using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Aspose.Pdf;
using Html2Pdf.HParser;


namespace Html2Pdf.PCreator
{
    public class PDocument
    {
        
        public PDocument(string fileFullName, HDocument hDocument)
        {
            //throw new PException("file " + fileFullName + " is busy.");
            helloWorld(fileFullName);
        }


        private void helloWorld(string fileFullName)
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
    }
}
