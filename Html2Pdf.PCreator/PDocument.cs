﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using Html2Pdf.HParser;



namespace Html2Pdf.PCreator
{
    public class PDocument
    {
        //private HNode rootNode;
        private HNode bodyNode;

        private TextState pageTextState;
        private MarginInfo pageMargin;
        private Aspose.Pdf.Color pageBackground;



        private Aspose.Pdf.Document pdfDocument;
        private Aspose.Pdf.Page pdfPage;

        private Aspose.Pdf.Text.TextFragment pdfTextFragment;
        private Aspose.Pdf.Image pdfImage;
        private HNodeTag hyperlinkNode;
        private Aspose.Pdf.Text.TextSegment pdfNewLine;

        private MarginInfo inlineParagraphMargin;

        private Aspose.Pdf.Forms.Field pdfFormField;
        private Dictionary<string, RadioButtonField> pdfRadioButtonFields = new Dictionary<string, RadioButtonField>();

        private int currentPageNum;

        



        public PDocument(string fileFullName, HDocument hDocument)
        {
            /*
            //DEBUG
            PDocument_debug(fileFullName, hDocument);
            return;
            */


            bodyNode = hDocument.BodyNode;
            
            pageTextState = PUtil.TextStateUtil.TextState_Default();
            PUtil.TextStateUtil.TextState_ModifyFromHStyles((bodyNode as HNodeTag).Styles, pageTextState);

            pageMargin = new MarginInfo(4, 4, 4, 12);
            pageBackground = Aspose.Pdf.Color.FromRgb(1.00, 1.00, 1.00);

            pdfDocument = new Document();
            pdfPage = null;
            pdfTextFragment = null;
            pdfImage = null;
            hyperlinkNode = null;
            pdfNewLine = null;
            inlineParagraphMargin = null;
            pdfFormField = null;
            pdfRadioButtonFields = new Dictionary<string, RadioButtonField>();

            updateCurrentPage();
            createBody();
            
            pdfDocument.Save(fileFullName);
        }


        

        private void updateCurrentPage()
        {
            bool emergedNewPage = false;

            pdfDocument.ProcessParagraphs();
            
            if (pdfPage == null)
            {
                pdfPage = pdfDocument.Pages.Add();
                emergedNewPage = true;
            }
            else if (pdfDocument.Pages.Count > currentPageNum)
            {
                pdfPage = pdfDocument.Pages[pdfDocument.Pages.Count];
                emergedNewPage = true;
            }

            if (emergedNewPage)
            {
                currentPageNum = pdfDocument.Pages.Count;

                pdfPage.PageInfo.DefaultTextState = pageTextState;
                pdfPage.PageInfo.Margin = pageMargin;
                pdfPage.Background = pageBackground;
            }
        }

        private void createTextFragmentByTagType(HTagType tagType)
        {
            if (!HUtil.TagUtil.IsBlockTag(tagType))
            {
                pdfTextFragment = null;
                return;
            }

            pdfTextFragment = new TextFragment();
            
            if (tagType == HTagType.div)
            {
                pdfTextFragment.Margin = new MarginInfo(0, 0, 0, 0);
            }
            else if (tagType == HTagType.p)
            {
                pdfTextFragment.Margin = new MarginInfo(0, 12, 0, 12);
            }
            else
            {
                pdfTextFragment.Margin = new MarginInfo(0, 0, 0, 0);
            }

            return;
        }

        


        private void addTextFragmentOnPage(bool needUpdateCurrentPage = true)
        {
            if (pdfTextFragment != null)
            {
                if (pdfTextFragment.Segments.Count == 0 || (pdfTextFragment.Segments.Count == 1 && pdfTextFragment.Segments[1].Text == String.Empty))
                {
                    // not adding textFragment
                }
                else
                {
                    pdfPage.Paragraphs.Add(pdfTextFragment);
                    
                    if (needUpdateCurrentPage)
                    {
                        updateCurrentPage();
                    }
                }
                
                pdfTextFragment = null;
            }
        }



        private void createBody()
        {
            foreach (HNode node in (bodyNode as HNodeContainer).ChildNodes)
            {
                createNode(node, pageTextState);
            }
        }


        
        

        private void createNode(HNode node, TextState parentTextState)
        {
            TextState nodeTextState = new TextState();
            nodeTextState.ApplyChangesFrom(parentTextState);

            if (node is HNodeTag)
            {
                PUtil.TextStateUtil.TextState_ModifyFromHStyles((node as HNodeTag).Styles, nodeTextState);
            }

            // Block element
            if ((node is HNodeTag) && HUtil.TagUtil.IsBlockTag((node as HNodeTag).TagType))
            {
                addTextFragmentOnPage();
                createTextFragmentByTagType((node as HNodeTag).TagType);
            }
            // Inline element or Text element
            else if (
                (node is HNodeTag) && HUtil.TagUtil.IsInlineTag((node as HNodeTag).TagType)
                ||
                (node is HNodeText)
            )
            {
                if ((node is HNodeText) && (node as HNodeText).ParentNode != null && ((node as HNodeText).ParentNode is HNodeTag) && (((node as HNodeText).ParentNode as HNodeTag)).TagType == HTagType.button)
                {
                    //
                }
                else
                {
                    // Create TextSegment for element
                    TextSegment textSegment = getTextSegment(node, nodeTextState);

                    // New Line, <BR />
                    if (pdfNewLine != null)
                    {
                        double marginTop = 0;
                        double marginBottom = 0;
                        if (pdfTextFragment != null)
                        {
                            marginBottom = pdfTextFragment.Margin.Bottom;
                            pdfTextFragment.Margin.Bottom = 0;
                        }

                        addTextFragmentOnPage();
                        createTextFragmentByTagType(HTagType.div);

                        if (pdfTextFragment != null)
                        {
                            pdfTextFragment.Margin.Top = marginTop;
                            pdfTextFragment.Margin.Bottom = marginBottom;
                        }

                        pdfNewLine = null;
                    }
                    // Image
                    else if (pdfImage != null)
                    {
                        double imageHeight = pdfImage.FixHeight;
                        MarginInfo margin = new MarginInfo(0, 12, 0, 12);
                        if (pdfTextFragment == null || pdfTextFragment.Segments.Count == 0 || (pdfTextFragment.Segments.Count == 1 && pdfTextFragment.Segments[1].Text == String.Empty))
                        {

                        }
                        else
                        {
                            pdfTextFragment.Margin.Top += imageHeight;
                            margin = new MarginInfo(0, pdfTextFragment.Margin.Bottom, 0, -1 * imageHeight);
                        }

                        addTextFragmentOnPage(false);

                        pdfImage.IsInLineParagraph = true;
                        pdfImage.Margin = margin;
                        inlineParagraphMargin = margin;


                        if (hyperlinkNode != null)
                        {
                            Aspose.Pdf.WebHyperlink pdfHyperlink = new WebHyperlink(hyperlinkNode.GetAttribute("href", "#"));
                            pdfImage.Hyperlink = pdfHyperlink;
                        }

                        pdfPage.Paragraphs.Add(pdfImage);

                        if (node.NextNode == null)
                        {
                            updateCurrentPage();
                        }

                        pdfImage = null;
                    }
                    // Form Field Element
                    else if (pdfFormField != null)
                    {
                        //
                        //
                        //

                        double inputHeight = pdfFormField.Height;
                        MarginInfo margin = new MarginInfo(0, 12, 0, 12);
                        if (pdfTextFragment == null || pdfTextFragment.Segments.Count == 0 || (pdfTextFragment.Segments.Count == 1 && pdfTextFragment.Segments[1].Text == String.Empty))
                        {

                        }
                        else
                        {
                            double textFragmentHeight = pdfTextFragment.Rectangle.Height;

                            margin = pdfTextFragment.Margin;

                            pdfTextFragment.Margin.Bottom = textFragmentHeight - inputHeight;
                            pdfTextFragment.Margin.Top += Math.Max(0, (inputHeight - textFragmentHeight));

                            pdfTextFragment.Margin.Top += inputHeight;
                        }


                        addTextFragmentOnPage(false);

                        pdfFormField.IsInLineParagraph = true;
                        pdfFormField.Margin = margin;
                        inlineParagraphMargin = new MarginInfo(pdfFormField.Width, margin.Bottom, margin.Right, margin.Top);


                        pdfPage.Paragraphs.Add(pdfFormField);

                        if (node.NextNode == null)
                        {
                            updateCurrentPage();
                        }

                        pdfFormField = null;
                    }
                    // TextFragment for InLineParagraph mode
                    else if (pdfTextFragment == null)
                    {
                        HTagType tagTypeForTextFragment = HTagType.div;
                        bool isInLineParagraphForTextFragment = false;


                        bool flagPreviousImage = false;
                        bool flagPreviousInput = false;

                        if (node.PrevNode != null && (node.PrevNode is HNodeTag) && (node.PrevNode as HNodeTag).TagType == HTagType.img)
                        {
                            // prev image element
                            if (node.ParentNode != null && (node.ParentNode is HNodeTag) && HUtil.TagUtil.IsBlockTag((node.ParentNode as HNodeTag).TagType))
                            {
                                tagTypeForTextFragment = (node.ParentNode as HNodeTag).TagType;
                            }

                            isInLineParagraphForTextFragment = true;
                            flagPreviousImage = true;
                        }
                        else if (node.PrevNode != null && (node.PrevNode is HNodeTag) && (node.PrevNode as HNodeTag).TagType == HTagType.input)
                        {
                            // prev input element
                            if (node.ParentNode != null && (node.ParentNode is HNodeTag) && HUtil.TagUtil.IsBlockTag((node.ParentNode as HNodeTag).TagType))
                            {
                                tagTypeForTextFragment = (node.ParentNode as HNodeTag).TagType;
                            }

                            isInLineParagraphForTextFragment = true;
                            flagPreviousInput = true;
                        }
                        else
                        {

                        }

                        createTextFragmentByTagType(tagTypeForTextFragment);
                        pdfTextFragment.IsInLineParagraph = isInLineParagraphForTextFragment;


                        if ((flagPreviousImage || flagPreviousInput) && inlineParagraphMargin != null)
                        {
                            pdfTextFragment.Margin.Top = -1 * pdfTextFragment.Rectangle.Height - inlineParagraphMargin.Bottom;
                            pdfTextFragment.Margin.Bottom = inlineParagraphMargin.Bottom;

                            pdfTextFragment.Margin.Left = inlineParagraphMargin.Left;

                            inlineParagraphMargin = null;
                        }
                        
                    }



                    if (textSegment != null && pdfTextFragment != null)
                    //if (textSegment != null)
                    {
                        pdfTextFragment.Segments.Add(textSegment);
                    }

                }

            }

            //
            // Create Nodes recursively with consider the hyperlink 
            //
            if ((node is HNodeTag) && (node as HNodeTag).TagType == HTagType.a)
            {
                hyperlinkNode = (node as HNodeTag);
            }

            if (node is HNodeContainer)
            {
                foreach (HNode childNode in (node as HNodeContainer).ChildNodes)
                {
                    createNode(childNode, nodeTextState);
                }
            }

            if ((node is HNodeTag) && (node as HNodeTag).TagType == HTagType.a)
            {
                hyperlinkNode = null;
            }
            //
            //
            //


            //
            // Add Text Fragment on Page (if need)
            //
            if ((node is HNodeTag) && HUtil.TagUtil.IsBlockTag((node as HNodeTag).TagType))
            {
                addTextFragmentOnPage();
            }
            //
            //
            //
        }

        
        private TextSegment getTextSegment(HNode node, TextState parentTextState)
        {
            TextSegment textSegment = null;

            // Text element
            if (node is HNodeText)
            {
                textSegment = new TextSegment();
                textSegment.TextState = parentTextState;
                textSegment.Text = (node as HNodeText).Text;

                
                if (hyperlinkNode != null)
                {
                    Aspose.Pdf.WebHyperlink pdfHyperlink = new WebHyperlink(hyperlinkNode.GetAttribute("href", "#"));
                    textSegment.Hyperlink = pdfHyperlink;
                }
                
            }
            // New Line element <br />
            if ((node is HNodeTag) && (node as HNodeTag).TagType == HTagType.br)
            {
                /*
                //слетают стили!!!
                textSegment = new TextSegment();
                //textSegment.TextState = parentTextState;
                textSegment.Text = Environment.NewLine;
                */

                pdfNewLine = new TextSegment();
                pdfNewLine.Text = Environment.NewLine;
            }
            // Hyperlink element <a>
            if ((node is HNodeTag) && (node as HNodeTag).TagType == HTagType.a)
            {
                PUtil.TextStateUtil.TextState_ModifyForHyperlink(parentTextState);
                PUtil.TextStateUtil.TextState_ModifyFromHStyles((node as HNodeTag).Styles, parentTextState);
            }
            // Bold text element <b>
            if ((node is HNodeTag) && (node as HNodeTag).TagType == HTagType.b)
            {
                PUtil.TextStateUtil.TextState_ModifyForBold(parentTextState);
                PUtil.TextStateUtil.TextState_ModifyFromHStyles((node as HNodeTag).Styles, parentTextState);
            }
            // Italic text element <i>
            if ((node is HNodeTag) && (node as HNodeTag).TagType == HTagType.i)
            {
                PUtil.TextStateUtil.TextState_ModifyForItalic(parentTextState);
                PUtil.TextStateUtil.TextState_ModifyFromHStyles((node as HNodeTag).Styles, parentTextState);
            }
            // Image element <img>
            if ((node is HNodeTag) && (node as HNodeTag).TagType == HTagType.img)
            {
                pdfImage = getImage(node as HNodeTag);
            }
            // Form field element <input>
            if ((node is HNodeTag) && (node as HNodeTag).TagType == HTagType.input)
            {
                pdfFormField = getFormField(node as HNodeTag);
            }
            // Button element <button>
            if ((node is HNodeTag) && (node as HNodeTag).TagType == HTagType.button)
            {
                pdfFormField = getFormFieldButton(node as HNodeTag);
            }

            // TODO - other inline tags


            return textSegment;
        }






        private Aspose.Pdf.Image getImage(HNodeTag imageNode)
        {
            /*
            int height = 0;
            int width = 0;

            using (WebClient webClient = new WebClient())
            {
                //var watch = System.Diagnostics.Stopwatch.StartNew();

                byte[] data = webClient.DownloadData(imageUrl);
                
                using (MemoryStream imageStream = new MemoryStream(data))
                {
                    // Add image to Images collection of Page Resources
                    pdfPage.Resources.Images.Add(imageStream);

                    height = pdfPage.Resources.Images[pdfPage.Resources.Images.Count].Height;
                    width = pdfPage.Resources.Images[pdfPage.Resources.Images.Count].Width;
                }
            }
            //return pdfPage.Resources.Images[pdfPage.Resources.Images.Count];
            */

            //pdfImage = getImage(((node as HNodeTag).GetAttribute("src", "")));


            Aspose.Pdf.Image image = null;
            string imageUrl = imageNode.GetAttribute("src", "");

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] imageData = webClient.DownloadData(imageUrl);

                    using (MemoryStream imageStream = new MemoryStream(imageData))
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromStream(imageStream);

                        image = new Aspose.Pdf.Image();
                        image.File = imageUrl;

                        image.FixHeight = img.Height;
                        image.FixWidth = img.Width;
                    }
                }
            }
            catch { }

            return image;
        }



        
        private RadioButtonField getRadioButtonField(string name)
        {
            RadioButtonField rbf = null;

            foreach (KeyValuePair<string, RadioButtonField> pair in pdfRadioButtonFields)
            {
                if (pair.Key == name)
                {
                    rbf = pair.Value;
                    break;
                }
            }

            if (rbf == null)
            {
                rbf = new RadioButtonField(pdfPage);

                rbf.PartialName = name;
                rbf.Name = name;
                pdfDocument.Form.Add(rbf, pdfPage.Number);

                pdfRadioButtonFields.Add(name, rbf);
            }

            return rbf;
        }

        private Aspose.Pdf.Forms.Field getFormField(HNodeTag inputNode)
        {
            Aspose.Pdf.Forms.Field field = null;

            string fieldType = inputNode.GetAttribute("type", "text");
            //button|checkbox|file|hidden|image|password|radio|reset|submit|text

            switch (fieldType)
            {
                case "button":
                case "submit":
                    field = getFormField_Button();
                    break;
                case "checkbox":
                    field = getFormField_CheckBox();
                    break;
                case "file":
                case "hidden":
                case "image":
                case "password":
                case "reset":
                    break;
                case "radio":
                    field = getFormField_RadioBox();
                    break;
                case "text":
                default:
                    field = getFormField_TextBox();
                    break;
            }

            return field;

            Aspose.Pdf.Forms.Field getFormField_TextBox()
            {
                TextBoxField textBoxField = new TextBoxField();

                textBoxField.Height = 16;
                textBoxField.Width = 160;

                string name = inputNode.GetAttribute("name", "text_box_field");
                textBoxField.PartialName = name;

                string value = inputNode.GetAttribute("value", "");
                if (!String.IsNullOrEmpty(value))
                {
                    textBoxField.Value = value;
                }
                //Так делать нельзя, падает исключение при pdfDocument.ProcessParagraphs();
                //textBoxField.Value = "";
                
                //Border border = new Border(field);
                //border.Width = 1;
                //border.Dash = new Dash(1, 1);
                //field.Border = border;
                
                return textBoxField;
            }

            Aspose.Pdf.Forms.Field getFormField_CheckBox()
            {
                
                CheckboxField checkBoxField = new CheckboxField();

                checkBoxField.Height = 10;
                checkBoxField.Width = 10;

                string name = inputNode.GetAttribute("name", "checkbox_field");
                checkBoxField.PartialName = name;

                //Так делать нельзя, падает исключение при pdfDocument.ProcessParagraphs();
                //checkBoxField.Checked = true;

                return checkBoxField;
            }

            Aspose.Pdf.Forms.Field getFormField_RadioBox()
            {
                string name = inputNode.GetAttribute("name", "radio_button_field");
                RadioButtonField rbf = getRadioButtonField(name);
                
                RadioButtonOptionField opt = new RadioButtonOptionField();
                opt.OptionName = name + "_" + (rbf.Count + 1).ToString();
                opt.Width = 12;
                opt.Height = 12;

                //Так делать нельзя, падает исключение при pdfDocument.ProcessParagraphs();
                //opt.Border = new Border(opt);
                //opt.Border.Width = 1;
                //opt.Border.Style = BorderStyle.Solid;
                //opt.Characteristics.Border = System.Drawing.Color.Black;
                //opt.DefaultAppearance.TextColor = System.Drawing.Color.Red;

                //opt.Caption = new TextFragment("Item1");


                rbf.Add(opt);
                
                return opt;
            }

            Aspose.Pdf.Forms.Field getFormField_Button()
            {
                ButtonField buttonField = new ButtonField();

                string value = inputNode.GetAttribute("value", "");

                buttonField.Height = 18;
                buttonField.Width = 12 + 6 * value.Length;

                if (!String.IsNullOrEmpty(value))
                {
                    buttonField.Value = value;
                }

                return buttonField;
            }
        }



        private Aspose.Pdf.Forms.Field getFormFieldButton(HNodeTag buttonNode)
        {
            Aspose.Pdf.Forms.Field field = null;
            string value = "";

            if ((buttonNode is HNodeContainer) && (buttonNode as HNodeContainer).ChildNodes.Count > 0)
            {
                if ((buttonNode as HNodeContainer).ChildNodes[0] is HNodeText)
                {
                    value = ((buttonNode as HNodeContainer).ChildNodes[0] as HNodeText).Text;
                }
            }

            field = getFormField_Button();
            return field;

            Aspose.Pdf.Forms.Field getFormField_Button()
            {
                ButtonField buttonField = new ButtonField();

                buttonField.Height = 18;
                buttonField.Width = 12 + 6 * value.Length;

                if (!String.IsNullOrEmpty(value))
                {
                    buttonField.Value = value;
                }

                return buttonField;
            }
        }













        //
        // Debug methods
        // for testing functionality Aspose.PDF
        //

        public void PDocument_debug(string fileFullName, HDocument hDocument)
        {
            //throw new PException("file " + fileFullName + " is busy.");

            //debug_helloWorld(fileFullName);
            //debug_createImage(fileFullName);
            //debug_createText(fileFullName);

            //debug_createP(fileFullName);



            pdfDocument = new Document();


            //PExample.Form1(pdfDocument);

            //PExample.Text1(pdfDocument);
            //PExample.Text2(pdfDocument);
            //PExample.Text3(pdfDocument);
            PExample.Text4(pdfDocument);

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

        //
        // Debug methods
        // for testing functionality Aspose.PDF
        // (end)
        //
    }
}
