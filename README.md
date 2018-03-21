# Html2Pdf
Console application for HTML file to PDF document conversion.

## Description

### Usage

```
> Html2Pdf.Console.exe [html_file] [pdf_file]
```
or
```
> Html2Pdf.Console.exe [html_file]
```
or
```
> Html2Pdf.Console.exe
```


If parameter **[pdf_file]** is not specified then pdf file will have the same name.
If parameter **[html_file]** is not specified then **test.html** file name will be used.

Default folder for find and create files is:
* **Data** folder in root directory of solution for Debug configuration;
* Current folder of application exe file for Release configuration;

### Project Components and Used Technologies
* .Net 4.6.1 console application on C#
* Html parser developed with native C# language
* Aspose.Pdf library
* MS Tests


### Supported Html tags and attributes
Application works with valid XHTML documents only and supports limited number tags and attributes.

List of supported tags:
* p
* div
* span
* br (in development)
* b
* i
* a
* img
* form
* input

List of supported style properties:
* color
* font-family
* font-size
* font-weight
* text-decoration
