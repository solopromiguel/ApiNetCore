
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using WebApplication21.Dtos;
using WebApplication21.sakila;
using Control = WebApplication21.sakila.Control;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WebApplication21.Helpers
{
    public class WordOutputFormatter : OutputFormatter
    {
        public string ContentType { get; }

        public WordOutputFormatter()
        {
            ContentType = "application/ms-word";
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ContentType));
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;

            var filePath = string.Format("./DataExport/myfile.docx", DateTime.Now.Ticks);

            if (File.Exists(filePath))File.Delete(filePath);

            var templatePath = "";

            //EtapaIdentificacion viewModel = context.Object as EtapaIdentificacion;
            OutDto viewModel1 = context.Object as OutDto;
            
            EtapaIdentificacion viewModelEtapaIdentificacion=null;
            PrintPerfilCliente viewModelPerfilCliente = null;
            // Identifica el Tipo de Objeto , para poder generar el documento adecuado y/o reutilizar la Clase
            if (viewModel1.NameModel.Equals("EtapaIdentificacion"))
            {
                viewModelEtapaIdentificacion = viewModel1.Result as EtapaIdentificacion;
                templatePath = string.Format("./DataExport/my-template.docx");
            }
            if (viewModel1.NameModel.Equals("PrintPerfilCliente"))
            {
                viewModelPerfilCliente = viewModel1.Result as PrintPerfilCliente;
                templatePath = string.Format("./DataExport/Impresion1.docx");
            }

            //open the template then save it as another file (while also stream it to the user)

            byte[] byteArray = File.ReadAllBytes(templatePath);
            using (MemoryStream mem = new MemoryStream())
            {
                mem.Write(byteArray, 0, (int)byteArray.Length);

                //to create a new document
                // using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(mem, true))
                {
                    if (viewModelPerfilCliente != null)
                    {
                        SearchAndReplaceTextInTable(wordDoc, "QVAL1", viewModelPerfilCliente.QVAL1);
                        SearchAndReplaceTextInTable(wordDoc, "QVAL2", viewModelPerfilCliente.QVAL2);
                        SearchAndReplaceTextInTable(wordDoc, "QVAL3", viewModelPerfilCliente.QVAL3);
                        SearchAndReplaceTextInTable(wordDoc, "QVAL4", viewModelPerfilCliente.QVAL4);
                        SearchAndReplaceTextInTable(wordDoc, "QVAL5", viewModelPerfilCliente.QVAL5);
                        SearchAndReplaceTextInTable(wordDoc, "QVAL6", viewModelPerfilCliente.QVAL6);
                        SearchAndReplaceTextInTable(wordDoc, "QVAL7", viewModelPerfilCliente.QVAL7);
                        SearchAndReplaceTextInTable(wordDoc, "QVAL8", viewModelPerfilCliente.QVAL8);
                        SearchAndReplaceTextInTable(wordDoc, "QVAL9", viewModelPerfilCliente.QVAL9);
                        SearchAndReplaceTextInTable(wordDoc, "QVAL10", viewModelPerfilCliente.QVAB10);
                        SearchAndReplaceTextInTable(wordDoc, "QVAL11", viewModelPerfilCliente.QVAB11);
                        SearchAndReplaceTextInTable(wordDoc, "QVALD", viewModelPerfilCliente.QVAB12);
                        SearchAndReplaceTextInTable(wordDoc, "QVALD", viewModelPerfilCliente.QVAB13);
                        SearchAndReplaceTextInTable(wordDoc, "QVALD", viewModelPerfilCliente.QVAB14);
                        SearchAndReplaceTextInTable(wordDoc, "QVALD", viewModelPerfilCliente.QVAB15);
                        SearchAndReplaceTextInTable(wordDoc, "QVALD", viewModelPerfilCliente.QVAB16);
                    }
                    Debug.WriteLine("");

                    if (viewModelEtapaIdentificacion != null) {



                    var body = wordDoc.MainDocumentPart.Document.Body;
                    var paras = body.Elements<Paragraph>();

                    SearchAndReplaceText2(wordDoc, "nomfile", "CANALES ELECTRONICO");
                    SearchAndReplaceText(wordDoc, "inombre", "Ing. Armando Manuel Gonzales Cajes");
                    SearchAndReplaceText(wordDoc, "icargo", "Asistente de Cumplimiento");

                    body.Append(AddParagraph("Anexo 01", true, JustificationValues.Center));
                    body.Append(AddParagraph("IDENTIFICACIÓN DE LOS RIESGOS DE LAFT Y RIESGOS ASOCIADOS", false, JustificationValues.Center));
                    body.Append(AddParagraph("", false, JustificationValues.Center));
                    body.Append(AddParagraph("Para identificar los riesgos de LAFT y riesgos asociados, de acuerdo con la clasificación y caracterización Tarjetas Foráneas-Análisis de Riesgos  LAFT - Tarjetas Foráneas",
                                              false, JustificationValues.Left));
                    body.Append(AddParagraph("", false, JustificationValues.Center));

                    body.Append(AddTableAnexo2(viewModelEtapaIdentificacion));


                    body.Append(PageBreak());

                    body.Append(AddParagraph("Anexo 02", true, JustificationValues.Center));
                    body.Append(AddParagraph("EVALUACIÓN DE RIESGOS", false, JustificationValues.Center));
                    body.Append(AddParagraph("", false, JustificationValues.Center));
                    body.Append(AddParagraph("A continuación, se muestran los riesgos de LAFT identificados medidos a través de la probabilidad e impacto descritos anteriormente, así como también el nivel de riesgo determinado y los controles con los que cuenta la CMAC PIURA S.A.C. que ayudan a mitigar el riesgo de LAFT",
                                              false, JustificationValues.Left));
                    body.Append(AddParagraph("", false, JustificationValues.Center));

                    body.Append(AddTableAnexo2(viewModelEtapaIdentificacion));


                    body.Append(PageBreak());


                    body.Append(AddParagraph("Anexo 03", true, JustificationValues.Center));
                    body.Append(AddParagraph("CONTROLES IDENTIFICADOS Y CARACTERÍSTICAS", false, JustificationValues.Center));
                    body.Append(AddParagraph("", false, JustificationValues.Center));
                    
                  
                    body.Append(AddTableAnexo3(viewModelEtapaIdentificacion));

                    wordDoc.Close();

                    };
                }

                using (FileStream fileStream = new FileStream(filePath, System.IO.FileMode.CreateNew))
                {
                    mem.WriteTo(fileStream);
                    mem.Close();
                    fileStream.Close();
                }

                response.Headers.Add("Content-Disposition", "inline;filename=MyFile.docx");
                response.ContentType = "application/ms-word";

                await response.SendFileAsync(filePath);
            }

        }

        public Table AddTableAnexo2(EtapaIdentificacion data)
        {
            Table table = new Table();
            UInt32Value size = 6;
            TableProperties props = new TableProperties(

                    new TableBorders(
                    new TopBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size,
                        
                    },
                    new BottomBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size
                    },
                    new LeftBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size
                    },
                    new RightBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size
                    },
                    new InsideHorizontalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size
                    },

                    new InsideVerticalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size
                    }));

            table.AppendChild<TableProperties>(props);

            var hr = new TableRow();
            
            for (var j = 0; j < 6; j++)
            {
                var hc = new TableCell();

                var tcp = new TableCellProperties(new TableCellWidth()
                {
                    Type = TableWidthUnitValues.Dxa,
                    Width = "2000",
                });
                // Add cell shading.
                var shading = new Shading()
                {
                    Color = "auto",
                    Fill = "ABCDEF",
                    Val = ShadingPatternValues.Clear
                };


                switch (j)
                {
                    case 0:
                        hc.Append(new Paragraph(new Run(new Text("Descripción  del Riesgo de LAFT"))));
                        hc.Append(new TableCellProperties(
                            new TableCellWidth { Width = "2600" }));
                        break;
                    case 1:
                        hc.Append(new Paragraph(new Run(new Text("Probabilidad"))));
                        break;
                    case 2:
                        hc.Append(new Paragraph(new Run(new Text("Impacto"))));
                        break;
                    case 3:
                        hc.Append(new Paragraph(new Run(new Text("Riesgo de LAFT Inherente"))));
                        break;
                    case 4:
                        hc.Append(new Paragraph(new Run(new Text("Descripción  del Control"))));
                        break;
                    case 5:
                        hc.Append(new Paragraph(new Run(new Text("Riesgo de LAFT Residual"))));
                        break;



                }

                tcp.Append(shading);
                hc.Append(tcp);

                hr.Append(hc);

            }
            table.Append(hr);



            foreach (var item in data.Riesgos)
            {
                var tr = new TableRow();
                for (var j = 0; j < 6; j++)
                {
                    var tc = new TableCell();
                    switch (j)
                    {
                        case 0:
                            tc.Append(new Paragraph(new Run(new Text(item.Descripcion))));
                            break;
                        case 1:
                            tc.Append(new Paragraph(new Run(new Text(item.Probabilidad))));
                            break;
                        case 2:
                            tc.Append(new Paragraph(new Run(new Text(item.Impacto))));
                            break;
                        case 3:
                            tc.Append(new Paragraph(new Run(new Text("Inherente"))));
                            break;
                        case 4:
                            if (item.Controles!=null)
                            {
                                foreach (var controls in item.Controles)
                                {
                                    tc.Append(new Paragraph(new Run(new Text("-"+controls.Descripcion))));
                                }
                            }
                            
                            break;
                        case 5:
                            tc.Append(new Paragraph(new Run(new Text("Residual"))));
                            break;
                    }

                    // Assume you want columns that are automatically sized.
                    tc.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Auto }));

                    tr.Append(tc);
                }
                table.Append(tr);
            }

            return table;
            //}
            }
        public Table AddTableAnexo3(EtapaIdentificacion data)
        {
            Table table = new Table();
            UInt32Value size = 6;

            TableProperties props = new TableProperties(

                    new TableBorders(
                    new TopBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size,

                    },
                    new BottomBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size
                    },
                    new LeftBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size
                    },
                    new RightBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size
                    },
                    new InsideHorizontalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size
                    },
                    new InsideVerticalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = size
                    }));

            table.AppendChild<TableProperties>(props);

            var hr = new TableRow();

            for (var j = 0; j < 7; j++)
            {
                var hc = new TableCell();
                var tcp = new TableCellProperties(new TableCellWidth()
                {
                    Type = TableWidthUnitValues.Dxa,
                    Width = "2000",
                });
                // Add cell shading.
                var shading = new Shading()
                {
                    Color = "auto",
                    Fill = "ABCDEF",
                    Val = ShadingPatternValues.Clear
                };

                switch (j)
                {
                    case 0:
                        hc.Append(new Paragraph(new Run(new Text("Descripción del Control"))));
                        hc.Append(new TableCellProperties(
                            new TableCellWidth { Width = "2600" }));
                        break;
                    case 1:
                        hc.Append(new Paragraph(new Run(new Text("Cargo del Responsable"))));
                        break;
                    case 2:
                        hc.Append(new Paragraph(new Run(new Text("Periodicidad"))));
                        break;
                    case 3:
                        hc.Append(new Paragraph(new Run(new Text("Oportunidad"))));
                        break;
                    case 4:
                        hc.Append(new Paragraph(new Run(new Text("Grado de automatización"))));
                        break;
                    case 5:
                        hc.Append(new Paragraph(new Run(new Text("Formalización"))));
                        break;
                    case 6:
                        hc.Append(new Paragraph(new Run(new Text("Calificación  del Control"))));
                        break;

                }

                tcp.Append(shading);
                hc.Append(tcp);

                hr.Append(hc);

            }
            table.Append(hr);

            List<int> controlesAddId = new List<int>();

            foreach (var item in data.Riesgos)
            {
                
                foreach (var controls in item.Controles)
                {
                    // Permite agregar controles no repetidos
                    var flag= controlesAddId.FirstOrDefault(x => x == controls.Id);
                    if (flag.Equals(0))
                    {
                        controlesAddId.Add(controls.Id);

                        var tr = new TableRow();

                        for (var j = 0; j < 7; j++)
                        {
                            var tc = new TableCell();
                            switch (j)
                            {
                                case 0:
                                    tc.Append(new Paragraph(new Run(new Text(controls.Descripcion))));
                                    break;
                                case 1:
                                    tc.Append(new Paragraph(new Run(new Text(controls.Cargo))));
                                    break;
                                case 2:
                                    tc.Append(new Paragraph(new Run(new Text(controls.Periodicidad))));
                                    break;
                                case 3:
                                    tc.Append(new Paragraph(new Run(new Text(controls.Oportunidad))));
                                    break;
                                case 4:

                                    tc.Append(new Paragraph(new Run(new Text(controls.Grado))));

                                    break;
                                case 5:
                                    tc.Append(new Paragraph(new Run(new Text(controls.Formalizacion))));
                                    break;
                                case 6:
                                    tc.Append(new Paragraph(new Run(new Text(controls.Calificacion))));
                                    break;
                            }

                            // Assume you want columns that are automatically sized.
                            tc.Append(new TableCellProperties(
                                new TableCellWidth { Type = TableWidthUnitValues.Auto }));

                            tr.Append(tc);
                        }

                        table.Append(tr);

                    }
                }

                
            }

            return table;
            //}
        }

        private Paragraph AddParagraph(string txt, bool bold, JustificationValues pjustification)
        {
            var runProp = new RunProperties();
           // runProp.Append(fsize);

            if (bold)
                runProp.Append(new Bold());

            var run = new Run();
            run.Append(runProp);
            run.Append(new Text(txt));

            var pp = new ParagraphProperties();
            pp.Justification = new Justification()
            {
                Val = pjustification
            };

            var p = new Paragraph();
            p.Append(pp);
            p.Append(run);

            return p;
        }

        public static void SearchAndReplaceTextInTable(WordprocessingDocument wordDoc,
            string searchText, string replaceText)
        {
            var tables = wordDoc.MainDocumentPart.Document.Descendants<Table>().ToList();

            foreach (Table t in tables)
            {
                var rows = t.Elements<TableRow>();
                foreach (TableRow row in rows)
                {
                    var cells = row.Elements<TableCell>();
                    foreach (TableCell cell in cells)
                    {
                        if (cell.InnerText.Contains(searchText))
                        {
                            Paragraph p = cell.Elements<Paragraph>().First(x => x.InnerText.Contains(searchText));
                            Run r = p.Elements<Run>().ElementAt(1);
                            Text t1 = r.Elements<Text>().First();
                            t1.Text = replaceText;

                            return; //Reemplazará solo el primero que encuentre.
                        }

                    }

                }

            }

        }

            public static void SearchAndReplaceText (WordprocessingDocument wordDoc,
            string searchText , string replaceText)
        {

            //pendiente de refactorizar..
            foreach (var text in wordDoc.MainDocumentPart.Document.Descendants<Text>())
            {
                if (text.Text.Trim().Contains(searchText))
                {
                    text.Text = text.Text.Trim().Replace(searchText, replaceText);
                }
            }
        }    

            public static void SearchAndReplaceText2(WordprocessingDocument wordDoc,
    string searchText, string replaceText)
        {
            // pendiente de refactorizar..

            var body = wordDoc.MainDocumentPart.Document.Body;
            var paras = body.Elements<Paragraph>();

            //search & replace string
            foreach (var para in paras)
            {
                foreach (var run in para.Elements<Run>())
                {
                    foreach (var text in run.Elements<Text>())
                    {
                        if (text.Text.Trim().Contains(searchText))
                        {
                            text.Text = text.Text.Trim().Replace(searchText, replaceText);
                        }
                    }
                }
            }
        }


        public static Paragraph PageBreak()
        {
            return new Paragraph(
                      new Run(
                        new Break() { Type = BreakValues.Page }));
        }


            // Apply a style to a paragraph.
        public static void ApplyStyleToParagraph(WordprocessingDocument doc,
            string styleid, string stylename, Paragraph p)
           {
            // If the paragraph has no ParagraphProperties object, create one.
            if (p.Elements<ParagraphProperties>().Count() == 0)
            {
                p.PrependChild<ParagraphProperties>(new ParagraphProperties());
            }

            // Get the paragraph properties element of the paragraph.
            ParagraphProperties pPr = p.Elements<ParagraphProperties>().First();

            // Get the Styles part for this document.
            StyleDefinitionsPart part =
                doc.MainDocumentPart.StyleDefinitionsPart;

            // If the Styles part does not exist, add it and then add the style.
            if (part == null)
            {
                part = AddStylesPartToPackage(doc);
                AddNewStyle(part, styleid, stylename);
            }
            else
            {
                // If the style is not in the document, add it.
                if (IsStyleIdInDocument(doc, styleid) != true)
                {
                    // No match on styleid, so let's try style name.
                    string styleidFromName = GetStyleIdFromStyleName(doc, stylename);
                    if (styleidFromName == null)
                    {
                        AddNewStyle(part, styleid, stylename);
                    }
                    else
                        styleid = styleidFromName;
                }
            }

            // Set the style of the paragraph.
            pPr.ParagraphStyleId = new ParagraphStyleId() { Val = styleid };
        }

        // Return true if the style id is in the document, false otherwise.
        public static bool IsStyleIdInDocument(WordprocessingDocument doc,
            string styleid)
        {
            // Get access to the Styles element for this document.
            Styles s = doc.MainDocumentPart.StyleDefinitionsPart.Styles;

            // Check that there are styles and how many.
            int n = s.Elements<Style>().Count();
            if (n == 0)
                return false;

            // Look for a match on styleid.
            Style style = s.Elements<Style>()
                .Where(st => (st.StyleId == styleid) && (st.Type == StyleValues.Paragraph))
                .FirstOrDefault();
            if (style == null)
                return false;

            return true;
        }

        // Return styleid that matches the styleName, or null when there's no match.
        public static string GetStyleIdFromStyleName(WordprocessingDocument doc, string styleName)
        {
            StyleDefinitionsPart stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
            string styleId = stylePart.Styles.Descendants<StyleName>()
                .Where(s => s.Val.Value.Equals(styleName) &&
                    (((Style)s.Parent).Type == StyleValues.Paragraph))
                .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();
            return styleId;
        }

        // Create a new style with the specified styleid and stylename and add it to the specified
        // style definitions part.
        private static void AddNewStyle(StyleDefinitionsPart styleDefinitionsPart,
            string styleid, string stylename)
        {
            // Get access to the root element of the styles part.
            Styles styles = styleDefinitionsPart.Styles;

            // Create a new paragraph style and specify some of the properties.
            Style style = new Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true
            };
            StyleName styleName1 = new StyleName() { Val = stylename };
            BasedOn basedOn1 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
            style.Append(styleName1);
            style.Append(basedOn1);
            style.Append(nextParagraphStyle1);

            // Create the StyleRunProperties object and specify some of the run properties.
            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
            Bold bold1 = new Bold();
            Color color1 = new Color() { ThemeColor = ThemeColorValues.Accent2 };
            RunFonts font1 = new RunFonts() { Ascii = "Lucida Console" };
            Italic italic1 = new Italic();
            // Specify a 12 point size.
            FontSize fontSize1 = new FontSize() { Val = "24" };
            styleRunProperties1.Append(bold1);
            styleRunProperties1.Append(color1);
            styleRunProperties1.Append(font1);
            styleRunProperties1.Append(fontSize1);
            styleRunProperties1.Append(italic1);

            // Add the run properties to the style.
            style.Append(styleRunProperties1);

            // Add the style to the styles part.
            styles.Append(style);
        }

        // Add a StylesDefinitionsPart to the document.  Returns a reference to it.
        public static StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument doc)
        {
            StyleDefinitionsPart part;
            part = doc.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            Styles root = new Styles();
            root.Save(part);
            return part;
        }

    }
}
    
