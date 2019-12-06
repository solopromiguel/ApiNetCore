
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

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            var templatePath = string.Format("./DataExport/my-template.docx");


            //EtapaIdentificacion viewModel = context.Object as EtapaIdentificacion;
            OutDto viewModel1 = context.Object as OutDto;

            EtapaIdentificacion viewModel=null;

            if (viewModel1.NameModel.Equals("EtapaIdentificacion"))
            {
                viewModel = viewModel1.Result as EtapaIdentificacion;
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

                    var body = wordDoc.MainDocumentPart.Document.Body;
                    var paras = body.Elements<Paragraph>();

                    //search & replace string
                    foreach (var para in paras)
                    {
                        foreach (var run in para.Elements<Run>())
                        {
                            foreach (var text in run.Elements<Text>())
                            {
                                if (text.Text.Trim().Contains("nomfile"))
                                {
                                    text.Text = text.Text.Trim().Replace("nomfile", viewModel.Nombre);
                                }
                                if (text.Text.Trim().Contains("inombre"))
                                {
                                    text.Text = text.Text.Trim().Replace("inombre", viewModel.Nombre);

                                }
                                if (text.Text.Trim().Contains("icargo"))
                                {
                                    text.Text = text.Text.Trim().Replace("icargo", viewModel.Nombre);
                                }
                                if (text.Text.Trim().Contains("fecha"))
                                {
                                    text.Text = text.Text.Trim().Replace("fecha", viewModel.Nombre);
                                }
                            }
                        }
                    }

                    //another approach to search & replace string
                    foreach (var text in wordDoc.MainDocumentPart.Document.Descendants<Text>())
                    {
                        if (text.Text.Trim().Contains("nomfile"))
                        {
                            text.Text = text.Text.Trim().Replace("nomfile", viewModel.Nombre);
                        }
                    }

                    ControlDto controlDto;
                    List<ControlDto> list = new List<ControlDto>();

                    for (int i = 0; i < 10; i++)
                    {
                        controlDto = new ControlDto
                        {

                            nombre = "Control " + i,
                            descripcion = "Descripcion " + i,
                            probabilidad = i.ToString()

                        };

                        list.Add(controlDto);
                    }

                    Table table2 = AddTable(viewModel);
                    body.Append(table2);


                    //append some stuff to the document
                    Paragraph p = new Paragraph();
                    Run r = new Run();
                    Text t = new Text("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent quam augue, tempus id metus in, laoreet viverra quam. Sed vulputate risus lacus, et dapibus orci porttitor non.");
                    r.Append(t);
                    p.Append(r);
                    body.Append(p);

                    p = new Paragraph();
                    r = new Run();
                    t = new Text(viewModel.Nombre);
                    r.Append(t);
                    p.Append(r);
                    body.Append(p);

                    string addedText = "This is the text added by the API example";


                    //Table table1 = new Table();

                    //var tr = new TableRow();

                    //var tc = new TableCell();
                    //tc.Append(new Paragraph(new Run(new Text("texto"))));

                    //// Assume you want columns that are automatically sized.
                    //tc.Append(new TableCellProperties(
                    //    new TableCellWidth { Type = TableWidthUnitValues.Auto }));

                    //tr.Append(tc);

                    //table1.Append(tr);

                    Table table1 = AddTable(viewModel);
                    body.Append(table1);


                    //Table table =
                    //wordDoc.MainDocumentPart.Document.Body.Elements<Table>().First();

                    //for (int i = 0; i < list.Count; i++)
                    //{


                    //    // Find the second row in the table.  
                    //    TableRow row = table.Elements<TableRow>().ElementAt(i);


                    //    for (int j = 0; j < 2; j++)
                    //    {
                    //        // Find the third cell in the row.  
                    //        TableCell cell = row.Elements<TableCell>().ElementAt(2);

                    //        // Find the first paragraph in the table cell.  
                    //        Paragraph parag = cell.Elements<Paragraph>().FirstOrDefault();

                    //        // Find the first run in the paragraph.  
                    //        Run run1 = parag.Elements<Run>().First();

                    //        // Set the text for the run.  
                    //        Text text1 = run1.Elements<Text>().First();

                    //        switch (j)
                    //        {
                    //            case 0:
                    //                text1.Text = list[i].nombre;
                    //                break;
                    //            case 1:
                    //                text1.Text = list[i].descripcion;
                    //                break;
                    //            case 2:
                    //                text1.Text = list[i].probabilidad;
                    //                break;
                    //        }

                    //    }

                    //}

                    wordDoc.Close();


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

        public Table AddTable(EtapaIdentificacion data)
        {
            //using (var document = WordprocessingDocument.Open(fileName, true))
            //{

            // var doc = document.MainDocumentPart.Document;

            Table table = new Table();

            TableProperties props = new TableProperties(

                    new TableBorders(
                    new TopBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 8
                    },
                    new BottomBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 8
                    },
                    new LeftBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 8
                    },
                    new RightBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 8
                    },
                    new InsideHorizontalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 8
                    },
                    new InsideVerticalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 8
                    }));

            table.AppendChild<TableProperties>(props);


            foreach (var item in data.Riesgos)
            {
                var tr = new TableRow();
                for (var j = 0; j < 3; j++)
                {
                    var tc = new TableCell();
                    switch (j)
                    {
                        case 0:
                            tc.Append(new Paragraph(new Run(new Text(item.Descripcion))));
                            break;
                        case 1:
                            tc.Append(new Paragraph(new Run(new Text(item.Descripcion))));
                            break;
                        case 2:
                            tc.Append(new Paragraph(new Run(new Text(item.Descripcion))));
                            break;
                    }

                    // Assume you want columns that are automatically sized.
                    tc.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Auto }));

                    tr.Append(tc);
                }
                table.Append(tr);
            }

            //for (var i = 0; i <= data.Riesgos.Count; i++)
            //{
            //    var tr = new TableRow();
            //    for (var j = 0; j < 3; j++)
            //    {
            //        var tc = new TableCell();
            //        switch (j)
            //        {
            //            case 0:
            //                tc.Append(new Paragraph(new Run(new Text(data.Riesgos[0].nombre))));
            //                break;
            //            case 1:
            //                tc.Append(new Paragraph(new Run(new Text(data[0].descripcion))));
            //                break;
            //            case 2:
            //                tc.Append(new Paragraph(new Run(new Text(data[0].probabilidad))));
            //                break;
            //        }

            //        Assume you want columns that are automatically sized.
            //        tc.Append(new TableCellProperties(
            //            new TableCellWidth { Type = TableWidthUnitValues.Auto }));

            //        tr.Append(tc);
            //    }
            //    table.Append(tr);
            //}
            //doc.Body.Append(table);
            //doc.Save();

            return table;
            //}
        }

    }
}
    
