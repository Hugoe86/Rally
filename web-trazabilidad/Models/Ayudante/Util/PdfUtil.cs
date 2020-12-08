using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Ayudante.Util
{
    public static class PdfUtil
    {
        public static void DocumentNestedData(Document document, object data, System.Drawing.Color fontColor, System.Drawing.Color lineColor)
        {
            if (data != null)
            {
                if (typeof(IEnumerable).IsAssignableFrom(data.GetType()))
                {
                    var list = Convert.ChangeType(data, Type.GetType(data.GetType().FullName)) as IList;

                    if (list.Count > 0)
                    {
                        int level = 1;
                        int row = 1;

                        foreach (var obj in list)
                        {
                            string nested = row.ToString();
                            var dataObjProperties = obj.GetPropertiesData();
                            bool isFirst = true;
                            
                            foreach (var property in dataObjProperties)
                            {
                                if (property.Attributes.Any(x => x.GetType() == typeof(DataSimpleAttribute)) ||
                                    property.Attributes.Any(x => x.GetType() == typeof(DataListAttribute)))
                                {
                                    var subList = Convert.ChangeType(property.Value, Type.GetType(property.Value.GetType().FullName)) as IList;
                                    Attribute nameAttribute = property.Attributes.Where(x => x.GetType() == typeof(NameAttribute)).FirstOrDefault();
                                    Attribute colorAttribute = property.Attributes.Where(x => x.GetType() == typeof(ColorAttribute)).FirstOrDefault();
                                    Attribute backgroundColorAttribute = property.Attributes.Where(x => x.GetType() == typeof(BackgroundColorAttribute)).FirstOrDefault();
                                    string name = nameAttribute == null ? null : ((NameAttribute)nameAttribute).Name;
                                    System.Drawing.Color color = colorAttribute == null ? System.Drawing.Color.Black : System.Drawing.Color.FromArgb(((ColorAttribute)colorAttribute).Aplha, ((ColorAttribute)colorAttribute).Red, ((ColorAttribute)colorAttribute).Green, ((ColorAttribute)colorAttribute).Blue);
                                    System.Drawing.Color backgroundColor = backgroundColorAttribute == null ? System.Drawing.Color.White : System.Drawing.Color.FromArgb(((BackgroundColorAttribute)backgroundColorAttribute).Aplha, ((BackgroundColorAttribute)backgroundColorAttribute).Red, ((BackgroundColorAttribute)backgroundColorAttribute).Green, ((BackgroundColorAttribute)backgroundColorAttribute).Blue);
                                    DocumentNestedData(document, subList, color, backgroundColor, (level + 1), $"{nested}");
                                }
                                else
                                {
                                    Attribute nameAttribute = property.Attributes.Where(x => x.GetType() == typeof(NameAttribute)).FirstOrDefault();
                                    Attribute colorAttribute = property.Attributes.Where(x => x.GetType() == typeof(ColorAttribute)).FirstOrDefault();
                                    Attribute typeAttribute = property.Attributes.Where(x => x.GetType() == typeof(TypeAttribute)).FirstOrDefault();
                                    Attribute formatAttribute = property.Attributes.Where(x => x.GetType() == typeof(FormatPdfAttribute)).FirstOrDefault();
                                    string name = nameAttribute == null ? null : ((NameAttribute)nameAttribute).Name;
                                    System.Drawing.Color color = colorAttribute == null ? System.Drawing.Color.Black : System.Drawing.Color.FromArgb(((ColorAttribute)colorAttribute).Aplha, ((ColorAttribute)colorAttribute).Red, ((ColorAttribute)colorAttribute).Green, ((ColorAttribute)colorAttribute).Blue);
                                    Type type = typeAttribute == null ? null : ((TypeAttribute)typeAttribute).Type;
                                    string format = formatAttribute == null ? null : ((FormatPdfAttribute)formatAttribute).Format;
                                    
                                    Text textName = new Text($"{(isFirst ? " " : string.Empty)}{name}: ");
                                    textName.SetBold();
                                    textName.SetFontSize(12f);
                                    
                                    Text textValue = new Text(!string.IsNullOrWhiteSpace(format) ? $"{property.Value.ToString()}:{format}" : property.Value.ToString());
                                    textValue.SetFontSize(12f);

                                    Paragraph paragraph = new Paragraph();

                                    if (isFirst)
                                    {
                                        Text textNumber = new Text(nested);
                                        textNumber.SetFontColor(new DeviceRgb(color.R, color.G, color.B));
                                        textNumber.SetBorder(new SolidBorder(new DeviceRgb(color.R, color.G, color.B), 1f));
                                        textNumber.SetBold();

                                        paragraph.Add(textNumber);
                                        isFirst = false;
                                    }
                                    
                                    paragraph.Add(textName);
                                    paragraph.Add(textValue);

                                    document.Add(paragraph);
                                }
                            }

                            row++;
                        }
                    }
                }
                else if (typeof(object).IsAssignableFrom(data.GetType()))
                {

                }
            }
        }

        private static void DocumentNestedData(Document document, object data, System.Drawing.Color fontColor, System.Drawing.Color lineColor, int level, string nested)
        {
            if (data != null)
            {
                if (typeof(IEnumerable).IsAssignableFrom(data.GetType()))
                {
                    var list = Convert.ChangeType(data, Type.GetType(data.GetType().FullName)) as IList;

                    if (list.Count > 0)
                    {
                        int row = 1;

                        foreach (var obj in list)
                        {
                            string subNested = $"{nested}.{row.ToString()}";
                            var dataObjProperties = obj.GetPropertiesData();
                            bool isFirst = true;

                            foreach (var property in dataObjProperties)
                            {
                                if (property.Attributes.Any(x => x.GetType() == typeof(DataSimpleAttribute)) ||
                                    property.Attributes.Any(x => x.GetType() == typeof(DataListAttribute)))
                                {
                                    var subList = Convert.ChangeType(property.Value, Type.GetType(property.Value.GetType().FullName)) as IList;
                                    Attribute nameAttribute = property.Attributes.Where(x => x.GetType() == typeof(NameAttribute)).FirstOrDefault();
                                    Attribute colorAttribute = property.Attributes.Where(x => x.GetType() == typeof(ColorAttribute)).FirstOrDefault();
                                    Attribute backgroundColorAttribute = property.Attributes.Where(x => x.GetType() == typeof(BackgroundColorAttribute)).FirstOrDefault();
                                    string name = nameAttribute == null ? null : ((NameAttribute)nameAttribute).Name;
                                    System.Drawing.Color color = colorAttribute == null ? System.Drawing.Color.Black : System.Drawing.Color.FromArgb(((ColorAttribute)colorAttribute).Aplha, ((ColorAttribute)colorAttribute).Red, ((ColorAttribute)colorAttribute).Green, ((ColorAttribute)colorAttribute).Blue);
                                    System.Drawing.Color backgroundColor = backgroundColorAttribute == null ? System.Drawing.Color.White : System.Drawing.Color.FromArgb(((BackgroundColorAttribute)backgroundColorAttribute).Aplha, ((BackgroundColorAttribute)backgroundColorAttribute).Red, ((BackgroundColorAttribute)backgroundColorAttribute).Green, ((BackgroundColorAttribute)backgroundColorAttribute).Blue);
                                    DocumentNestedData(document, subList, color, backgroundColor, (level + 1), $"{subNested}");
                                }
                                else
                                {
                                    Attribute nameAttribute = property.Attributes.Where(x => x.GetType() == typeof(NameAttribute)).FirstOrDefault();
                                    Attribute colorAttribute = property.Attributes.Where(x => x.GetType() == typeof(ColorAttribute)).FirstOrDefault();
                                    Attribute backgroundColorAttribute = property.Attributes.Where(x => x.GetType() == typeof(BackgroundColorAttribute)).FirstOrDefault();
                                    Attribute typeAttribute = property.Attributes.Where(x => x.GetType() == typeof(TypeAttribute)).FirstOrDefault();
                                    Attribute formatAttribute = property.Attributes.Where(x => x.GetType() == typeof(FormatPdfAttribute)).FirstOrDefault();
                                    string name = nameAttribute == null ? null : ((NameAttribute)nameAttribute).Name;
                                    System.Drawing.Color color = colorAttribute == null ? System.Drawing.Color.Black : System.Drawing.Color.FromArgb(((ColorAttribute)colorAttribute).Aplha, ((ColorAttribute)colorAttribute).Red, ((ColorAttribute)colorAttribute).Green, ((ColorAttribute)colorAttribute).Blue);
                                    System.Drawing.Color backgroundColor = backgroundColorAttribute == null ? System.Drawing.Color.White : System.Drawing.Color.FromArgb(((BackgroundColorAttribute)backgroundColorAttribute).Aplha, ((BackgroundColorAttribute)backgroundColorAttribute).Red, ((BackgroundColorAttribute)backgroundColorAttribute).Green, ((BackgroundColorAttribute)backgroundColorAttribute).Blue);
                                    Type type = typeAttribute == null ? null : ((TypeAttribute)typeAttribute).Type;
                                    string format = formatAttribute == null ? null : ((FormatPdfAttribute)formatAttribute).Format;
                                    
                                    Text textName = new Text($"{name}: ");
                                    textName.SetBold();
                                    textName.SetFontSize(12f);

                                    Text textValue = new Text(!string.IsNullOrWhiteSpace(format) ? $"{property.Value.ToString()}:{format}" : property.Value.ToString());
                                    textValue.SetFontSize(12f);

                                    Paragraph paragraph = new Paragraph();

                                    if (isFirst)
                                    {
                                        Text textNumber = new Text(subNested);
                                        textNumber.SetFontColor(new DeviceRgb(backgroundColor.R, backgroundColor.G, backgroundColor.B));
                                        textNumber.SetBorder(new SolidBorder(new DeviceRgb(backgroundColor.R, backgroundColor.G, backgroundColor.B), 1f));
                                        textNumber.SetBold();

                                        paragraph.Add(textNumber);
                                        isFirst = false;
                                    }

                                    paragraph.Add(textName);
                                    paragraph.Add(textValue);
                                    paragraph.SetMarginLeft((level * 20));

                                    document.Add(paragraph);
                                }
                            }

                            row++;
                        }
                    }
                }
                else if (typeof(object).IsAssignableFrom(data.GetType()))
                {

                }
            }
        }

        public static Table DocumentTableData(object data, System.Drawing.Color tableColor, System.Drawing.Color tableBackgroundColor, string tableTitle = null, bool generateHeaders = false)
        {
            Table table = null;

            if (data != null)
            {
                if (typeof(IEnumerable).IsAssignableFrom(data.GetType()))
                {
                    var list = Convert.ChangeType(data, Type.GetType(data.GetType().FullName)) as IList;

                    if (list.Count > 0)
                    {
                        int columns = list[0].GetPropertiesName().Count;
                        table = new Table(columns, true);
                        table.SetWidth(500);
                        table.SetBorder(null);

                        if (!string.IsNullOrWhiteSpace(tableTitle))
                        {
                            Paragraph paragraph = new Paragraph(tableTitle);
                            Cell cell = new Cell(1, columns);
                            cell.SetBackgroundColor(new DeviceRgb(tableBackgroundColor.R, tableBackgroundColor.G, tableBackgroundColor.B));
                            cell.SetHeight(25);
                            cell.SetFontSize(14);
                            cell.SetBold();
                            cell.SetFontColor(new DeviceRgb(tableColor.R, tableColor.G, tableColor.B));
                            cell.SetTextAlignment(TextAlignment.CENTER);
                            cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                            cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                            cell.SetBorder(new SolidBorder(new DeviceRgb(255, 255, 255), 1f));
                            cell.Add(paragraph);
                            table.AddHeaderCell(cell);
                        }

                        var nameProperties = list[0].GetPropertiesName();

                        if (generateHeaders)
                        {
                            foreach (var property in nameProperties)
                            {
                                Paragraph paragraph = new Paragraph(property);
                                Cell cell = new Cell();
                                cell.SetBackgroundColor(new DeviceRgb(tableBackgroundColor.R, tableBackgroundColor.G, tableBackgroundColor.B));
                                cell.SetHeight(25);
                                cell.SetFontSize(14);
                                cell.SetBold();
                                cell.SetFontColor(new DeviceRgb(tableColor.R, tableColor.G, tableColor.B));
                                cell.SetTextAlignment(TextAlignment.CENTER);
                                cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                                cell.SetBorder(new SolidBorder(new DeviceRgb(255, 255, 255), 1f));
                                cell.Add(paragraph);
                                table.AddHeaderCell(cell);
                            }
                        }

                        int row = 1;

                        foreach (var obj in list)
                        {
                            var dataObjProperties = obj.GetPropertiesData();

                            foreach (var property in dataObjProperties)
                            {
                                if (property.Attributes.Any(x => x.GetType() == typeof(DataSimpleAttribute)) ||
                                    property.Attributes.Any(x => x.GetType() == typeof(DataListAttribute)))
                                {
                                    var subList = Convert.ChangeType(property.Value, Type.GetType(property.Value.GetType().FullName)) as IList;
                                    Attribute nameAttribute = property.Attributes.Where(x => x.GetType() == typeof(NameAttribute)).FirstOrDefault();
                                    Attribute colorAttribute = property.Attributes.Where(x => x.GetType() == typeof(ColorAttribute)).FirstOrDefault();
                                    Attribute backgroundColorAttribute = property.Attributes.Where(x => x.GetType() == typeof(BackgroundColorAttribute)).FirstOrDefault();
                                    string name = nameAttribute == null ? null : ((NameAttribute)nameAttribute).Name;
                                    System.Drawing.Color color = colorAttribute == null ? System.Drawing.Color.Black : System.Drawing.Color.FromArgb(((ColorAttribute)colorAttribute).Aplha, ((ColorAttribute)colorAttribute).Red, ((ColorAttribute)colorAttribute).Green, ((ColorAttribute)colorAttribute).Blue);
                                    System.Drawing.Color backgroundColor = backgroundColorAttribute == null ? System.Drawing.Color.White : System.Drawing.Color.FromArgb(((BackgroundColorAttribute)backgroundColorAttribute).Aplha, ((BackgroundColorAttribute)backgroundColorAttribute).Red, ((BackgroundColorAttribute)backgroundColorAttribute).Green, ((BackgroundColorAttribute)backgroundColorAttribute).Blue);
                                    table.AddCell(DocumentInnerTable(table, columns, subList, color, backgroundColor, name, true));
                                }
                                else
                                {
                                    Attribute colorAttribute = property.Attributes.Where(x => x.GetType() == typeof(ColorAttribute)).FirstOrDefault();
                                    System.Drawing.Color color = colorAttribute == null ? System.Drawing.Color.Black : System.Drawing.Color.FromArgb(((ColorAttribute)colorAttribute).Aplha, ((ColorAttribute)colorAttribute).Red, ((ColorAttribute)colorAttribute).Green, ((ColorAttribute)colorAttribute).Blue);
                                    Attribute typeAttribute = property.Attributes.Where(x => x.GetType() == typeof(TypeAttribute)).FirstOrDefault();
                                    Type type = typeAttribute == null ? null : ((TypeAttribute)typeAttribute).Type;
                                    Attribute formatAttribute = property.Attributes.Where(x => x.GetType() == typeof(FormatPdfAttribute)).FirstOrDefault();
                                    string format = formatAttribute == null ? null : ((FormatPdfAttribute)formatAttribute).Format;
                                    Paragraph paragraph = new Paragraph(!string.IsNullOrWhiteSpace(format) ? $"{property.Value.ToString()}:{format}" : property.Value.ToString());
                                    Cell cell = new Cell();
                                    DeviceRgb backgroundColor = null;
                                    if (row % 2 == 0)
                                    {
                                        System.Drawing.Color backgroundColorEven = System.Drawing.Color.White;
                                        backgroundColor = new DeviceRgb(backgroundColorEven.R, backgroundColorEven.G, backgroundColorEven.B);
                                    }
                                    else
                                    {
                                        System.Drawing.Color backgroundColorOdd = System.Drawing.Color.LightGray;
                                        backgroundColor = new DeviceRgb(backgroundColorOdd.R, backgroundColorOdd.G, backgroundColorOdd.B);
                                    }
                                    cell.SetBackgroundColor(backgroundColor);
                                    cell.SetHeight(16);
                                    cell.SetFontSize(12);
                                    cell.SetFontColor(new DeviceRgb(color.R, color.G, color.B));
                                    if (type.IsNumeric())
                                    {
                                        cell.SetTextAlignment(TextAlignment.RIGHT);
                                        cell.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
                                    }
                                    else
                                    {
                                        cell.SetTextAlignment(TextAlignment.LEFT);
                                        cell.SetHorizontalAlignment(HorizontalAlignment.LEFT);
                                    }
                                    cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                                    cell.SetBorder(new SolidBorder(new DeviceRgb(255, 255, 255), 1f));
                                    cell.Add(paragraph);
                                    table.AddCell(cell);
                                }
                            }

                            row++;
                        }
                    }
                }
                else if (typeof(object).IsAssignableFrom(data.GetType()))
                {

                }
            }

            return table;
        }

        private static Cell DocumentInnerTable(Table tableParent, int columnsParent, object data, System.Drawing.Color tableColor, System.Drawing.Color tableBackgroundColor, string tableTitle = null, bool generateHeaders = false)
        {
            Cell cellParent = new Cell(1, columnsParent);
            cellParent.SetBorder(null);
            cellParent.SetPaddingLeft(10);

            if (data != null)
            {
                if (typeof(IEnumerable).IsAssignableFrom(data.GetType()))
                {
                    var list = Convert.ChangeType(data, Type.GetType(data.GetType().FullName)) as IList;

                    if (list.Count > 0)
                    {
                        int columns = list[0].GetPropertiesName().Count;
                        Table innerTable = new Table(columns, true);
                        innerTable.SetWidth(500);
                        innerTable.SetBorder(null);

                        if (!string.IsNullOrWhiteSpace(tableTitle))
                        {
                            Paragraph paragraph = new Paragraph(tableTitle);
                            Cell cell = new Cell(1, columns);
                            cell.SetBackgroundColor(new DeviceRgb(tableBackgroundColor.R, tableBackgroundColor.G, tableBackgroundColor.B));
                            cell.SetHeight(25);
                            cell.SetFontSize(14);
                            cell.SetBold();
                            cell.SetFontColor(new DeviceRgb(tableColor.R, tableColor.G, tableColor.B));
                            cell.SetTextAlignment(TextAlignment.CENTER);
                            cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                            cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                            cell.SetBorder(new SolidBorder(new DeviceRgb(255, 255, 255), 1f));
                            cell.Add(paragraph);
                            innerTable.AddHeaderCell(cell);
                        }

                        var nameProperties = list[0].GetPropertiesName();

                        if (generateHeaders)
                        {
                            foreach (var property in nameProperties)
                            {
                                Paragraph paragraph = new Paragraph(property);
                                Cell cell = new Cell();
                                cell.SetBackgroundColor(new DeviceRgb(tableBackgroundColor.R, tableBackgroundColor.G, tableBackgroundColor.B));
                                cell.SetHeight(25);
                                cell.SetFontSize(14);
                                cell.SetBold();
                                cell.SetFontColor(new DeviceRgb(tableColor.R, tableColor.G, tableColor.B));
                                cell.SetTextAlignment(TextAlignment.CENTER);
                                cell.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                                cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                                cell.SetBorder(new SolidBorder(new DeviceRgb(255, 255, 255), 1f));
                                cell.Add(paragraph);
                                innerTable.AddHeaderCell(cell);
                            }
                        }

                        int row = 1;

                        foreach (var obj in list)
                        {
                            var dataObjProperties = obj.GetPropertiesData();

                            foreach (var property in dataObjProperties)
                            {
                                if (property.Attributes.Any(x => x.GetType() == typeof(DataSimpleAttribute)) ||
                                    property.Attributes.Any(x => x.GetType() == typeof(DataListAttribute)))
                                {
                                    var subList = Convert.ChangeType(property.Value, Type.GetType(property.Value.GetType().FullName)) as IList;
                                    Attribute nameAttribute = property.Attributes.Where(x => x.GetType() == typeof(NameAttribute)).FirstOrDefault();
                                    Attribute colorAttribute = property.Attributes.Where(x => x.GetType() == typeof(ColorAttribute)).FirstOrDefault();
                                    Attribute backgroundColorAttribute = property.Attributes.Where(x => x.GetType() == typeof(BackgroundColorAttribute)).FirstOrDefault();
                                    string name = nameAttribute == null ? null : ((NameAttribute)nameAttribute).Name;
                                    System.Drawing.Color color = colorAttribute == null ? System.Drawing.Color.Black : System.Drawing.Color.FromArgb(((ColorAttribute)colorAttribute).Aplha, ((ColorAttribute)colorAttribute).Red, ((ColorAttribute)colorAttribute).Green, ((ColorAttribute)colorAttribute).Blue);
                                    System.Drawing.Color backgroundColor = backgroundColorAttribute == null ? System.Drawing.Color.White : System.Drawing.Color.FromArgb(((BackgroundColorAttribute)backgroundColorAttribute).Aplha, ((BackgroundColorAttribute)backgroundColorAttribute).Red, ((BackgroundColorAttribute)backgroundColorAttribute).Green, ((BackgroundColorAttribute)backgroundColorAttribute).Blue);
                                    innerTable.AddCell(DocumentInnerTable(innerTable, columns, subList, color, backgroundColor, name, true));
                                }
                                else
                                {
                                    Attribute colorAttribute = property.Attributes.Where(x => x.GetType() == typeof(ColorAttribute)).FirstOrDefault();
                                    System.Drawing.Color color = colorAttribute == null ? System.Drawing.Color.Black : System.Drawing.Color.FromArgb(((ColorAttribute)colorAttribute).Aplha, ((ColorAttribute)colorAttribute).Red, ((ColorAttribute)colorAttribute).Green, ((ColorAttribute)colorAttribute).Blue);
                                    Attribute typeAttribute = property.Attributes.Where(x => x.GetType() == typeof(TypeAttribute)).FirstOrDefault();
                                    Type type = typeAttribute == null ? null : ((TypeAttribute)typeAttribute).Type;
                                    Attribute formatAttribute = property.Attributes.Where(x => x.GetType() == typeof(FormatPdfAttribute)).FirstOrDefault();
                                    string format = formatAttribute == null ? null : ((FormatPdfAttribute)formatAttribute).Format;
                                    Paragraph paragraph = new Paragraph(!string.IsNullOrWhiteSpace(format) ? string.Format("{0:1}", property.Value.ToString(), format) : property.Value.ToString());
                                    Cell cell = new Cell();
                                    DeviceRgb backgroundColor = null;
                                    if (row % 2 == 0)
                                    {
                                        System.Drawing.Color backgroundColorEven = System.Drawing.Color.White;
                                        backgroundColor = new DeviceRgb(backgroundColorEven.R, backgroundColorEven.G, backgroundColorEven.B);
                                    }
                                    else
                                    {
                                        System.Drawing.Color backgroundColorOdd = System.Drawing.Color.LightGray;
                                        backgroundColor = new DeviceRgb(backgroundColorOdd.R, backgroundColorOdd.G, backgroundColorOdd.B);
                                    }
                                    cell.SetBackgroundColor(backgroundColor);
                                    cell.SetHeight(16);
                                    cell.SetFontSize(12);
                                    cell.SetFontColor(new DeviceRgb(color.R, color.G, color.B));
                                    if (type.IsNumeric())
                                    {
                                        cell.SetTextAlignment(TextAlignment.RIGHT);
                                        cell.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
                                    }
                                    else
                                    {
                                        cell.SetTextAlignment(TextAlignment.LEFT);
                                        cell.SetHorizontalAlignment(HorizontalAlignment.LEFT);
                                    }
                                    cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                                    cell.SetBorder(new SolidBorder(new DeviceRgb(255, 255, 255), 1f));
                                    cell.Add(paragraph);
                                    innerTable.AddCell(cell);
                                }
                            }

                            row++;
                        }

                        cellParent.Add(innerTable);
                    }
                }
                else if (typeof(object).IsAssignableFrom(data.GetType()))
                {

                }
            }

            return cellParent;
        }
    }
}