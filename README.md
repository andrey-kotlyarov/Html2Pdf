# Html2Pdf
Console application for HTML file to PDF document conversion.

## Description

### Usage

```
Html2Pdf.Console.exe [html_file] [pdf_file]
```
or
```
Html2Pdf.Console.exe [html_file]
```
or
```
Html2Pdf.Console.exe
```



If no file is specified for *[pdf_file]* then pdf file name is generated at the basis of html file name.
If no file is specified for *[html_file]* then using **test.html** file.

Default folder for found and created files is:
* In Debug configuration using *Data* folder in root directory of solution;
* In Release configuration using current folder of exe file;

### Project Components and Used Technologies
* .Net 4.6.1 console application on C#
* Html parser established by native C# language
* Aspose.Pdf library for creating PDF document
* MS Tests




### Html tags support
Application work with valid XHTML documents only. Application support limited number tags and attributes.

List of tags that application support are below:
* p
* div
* span
* br
* a
* img
* form
* input

List of style attribute types that application support are below:
* color
* font-family
* font-size
* font-weight (in development)
* text-decoration
