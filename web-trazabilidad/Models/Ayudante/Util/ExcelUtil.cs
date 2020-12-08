using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Ayudante.Util
{
    public static class ExcelUtil
    {
        public static int WorksheetTableData(ExcelWorksheet worksheet, int row, int col, object data, Color tableColor, Color tableBackgroundColor, string tableTitle = null, bool generateHeaders = false, bool generateLevel = false)
        {
            if (data != null)
            {
                if (typeof(IEnumerable).IsAssignableFrom(data.GetType()))
                {
                    var list = Convert.ChangeType(data, Type.GetType(data.GetType().FullName)) as IList;
                    int colInit = col;

                    if (list.Count > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(tableTitle))
                        {
                            int colTo = col + list[0].GetPropertiesName().Count - 1;
                            WorksheedTableTitle(worksheet, row, row, col, colTo, 30, 14, tableTitle, tableColor, tableBackgroundColor);
                            if (generateLevel)
                            {
                                WorksheetTableRowLevel(worksheet, row, (colInit - 1));
                            }
                            row++;
                        }

                        var nameProperties = list[0].GetPropertiesName();
                        
                        if (generateHeaders)
                        {
                            foreach (var property in nameProperties)
                            {
                                WorksheetTableCellHeader(worksheet, row, col, property, 30, 14, tableColor, tableBackgroundColor);
                                col++;
                            }
                            if (generateLevel)
                            {
                                WorksheetTableRowLevel(worksheet, row, (colInit - 1));
                            }
                            row++;
                        }

                        int innerRow = 1;

                        foreach (var obj in list)
                        {
                            var dataObjProperties = obj.GetPropertiesData();
                            col = colInit;
                            int rowInit = row;
                            foreach (var property in dataObjProperties)
                            {
                                if (property.Attributes.Any(x => x.GetType() == typeof(DataSimpleAttribute)) ||
                                    property.Attributes.Any(x => x.GetType() == typeof(DataListAttribute)))
                                {
                                    var subList = Convert.ChangeType(property.Value, Type.GetType(property.Value.GetType().FullName)) as IList;
                                    rowInit++;
                                    Attribute nameAttribute = property.Attributes.Where(x => x.GetType() == typeof(NameAttribute)).FirstOrDefault();
                                    Attribute colorAttribute = property.Attributes.Where(x => x.GetType() == typeof(ColorAttribute)).FirstOrDefault();
                                    Attribute backgroundColorAttribute = property.Attributes.Where(x => x.GetType() == typeof(BackgroundColorAttribute)).FirstOrDefault();
                                    string name = nameAttribute == null ? null : ((NameAttribute)nameAttribute).Name;
                                    Color color = colorAttribute == null ? Color.Black : Color.FromArgb(((ColorAttribute)colorAttribute).Aplha, ((ColorAttribute)colorAttribute).Red, ((ColorAttribute)colorAttribute).Green, ((ColorAttribute)colorAttribute).Blue);
                                    Color backgroundColor = backgroundColorAttribute == null ? Color.White : Color.FromArgb(((BackgroundColorAttribute)backgroundColorAttribute).Aplha, ((BackgroundColorAttribute)backgroundColorAttribute).Red, ((BackgroundColorAttribute)backgroundColorAttribute).Green, ((BackgroundColorAttribute)backgroundColorAttribute).Blue);
                                    rowInit = WorksheetTableData(worksheet, rowInit, (colInit + 1), subList, color, backgroundColor, name, true, true) - 1;
                                }
                                else
                                {
                                    Attribute colorAttribute = property.Attributes.Where(x => x.GetType() == typeof(ColorAttribute)).FirstOrDefault();
                                    Color color = colorAttribute == null ? Color.Black : Color.FromArgb(((ColorAttribute)colorAttribute).Aplha, ((ColorAttribute)colorAttribute).Red, ((ColorAttribute)colorAttribute).Green, ((ColorAttribute)colorAttribute).Blue);
                                    Attribute typeAttribute = property.Attributes.Where(x => x.GetType() == typeof(TypeAttribute)).FirstOrDefault();
                                    Type type = typeAttribute == null ? null : ((TypeAttribute)typeAttribute).Type;
                                    Attribute formatAttribute = property.Attributes.Where(x => x.GetType() == typeof(FormatExcelAttribute)).FirstOrDefault();
                                    string format = formatAttribute == null ? null : ((FormatExcelAttribute)formatAttribute).Format;
                                    WorksheetTableCell(worksheet, row, col, property.Value, type, format, 16, 12, color);
                                }
                                
                                col++;
                            }

                            WorksheetTableRowEvenOdd(worksheet, row, colInit, (colInit + nameProperties.Count - 1), innerRow % 2 == 0);
                            innerRow++;

                            if (generateLevel)
                            {
                                WorksheetTableRowLevel(worksheet, row, (colInit - 1));
                            }

                            if (rowInit != row)
                            {
                                row = rowInit;
                            }
                            row++;
                        }
                    }
                }
                else if (typeof(object).IsAssignableFrom(data.GetType()))
                {

                }
            }

            return row;
        }

        private static void WorksheedTableTitle(ExcelWorksheet worksheet, int rowFrom, int rowTo, int colFrom, int colTo, double rowHeight, float fontSize, object value, Color color, Color backgroundColor)
        {
            worksheet.Row(rowFrom).Height = rowHeight;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Value = value;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Font.Bold = true;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Font.Size = fontSize;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Font.Color.SetColor(color);
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Fill.BackgroundColor.SetColor(backgroundColor);
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.WrapText = false;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Merge = false;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Numberformat.Format = string.Empty;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Merge = true;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Top.Color.SetColor(Color.White);
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Bottom.Color.SetColor(Color.White);
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Left.Color.SetColor(Color.White);
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Right.Color.SetColor(Color.White);
        }

        private static void WorksheedTableCellMerge(ExcelWorksheet worksheet, int rowFrom, int rowTo, int colFrom, int colTo, int height, int level, object value, string format, Type type, Color color, Color backgroundColor)
        {
            worksheet.Row(rowFrom).Height = height;
            worksheet.Row(rowFrom).OutlineLevel = level;
            worksheet.Row(rowFrom).Collapsed = true;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Value = value;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Font.Bold = false;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Font.Size = 12;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Font.Color.SetColor(color);
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Fill.BackgroundColor.SetColor(backgroundColor);
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.WrapText = false;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Merge = false;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Numberformat.Format = string.IsNullOrWhiteSpace(format) ? string.Empty : format;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Merge = true;

            if (type.IsNumeric())
            {
                worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
            else
            {
                worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            }

            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Top.Color.SetColor(Color.White);
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Bottom.Color.SetColor(Color.White);
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Left.Color.SetColor(Color.White);
            worksheet.Cells[rowFrom, colFrom, rowTo, colTo].Style.Border.Right.Color.SetColor(Color.White);
        }

        private static void WorksheetTableRowEvenOdd(ExcelWorksheet worksheet, int row, int colFrom, int colTo, bool isEven)
        {
            worksheet.Cells[row, colFrom, row, colTo].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[row, colFrom, row, colTo].Style.Fill.BackgroundColor.SetColor((isEven ? Color.White : Color.LightGray));
        }

        private static void WorksheetTableRowLevel(ExcelWorksheet worksheet, int row, int level)
        {
            worksheet.Row(row).OutlineLevel = level;
            worksheet.Row(row).Collapsed = true;
        }

        private static void WorksheetTableCellHeader(ExcelWorksheet worksheet, int row, int col, object value, double rowHeight, float fontSize, Color color, Color backgroundColor)
        {
            worksheet.Row(row).Height = rowHeight;
            worksheet.Cells[row, col].Value = value;
            worksheet.Cells[row, col].Style.Font.Bold = true;
            worksheet.Cells[row, col].Style.Font.Size = fontSize;
            worksheet.Cells[row, col].Style.Font.Color.SetColor(color);
            worksheet.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(backgroundColor);
            worksheet.Cells[row, col].Style.WrapText = false;
            worksheet.Cells[row, col].Merge = false;
            worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[row, col].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[row, col].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[row, col].Style.Border.Top.Color.SetColor(Color.White);
            worksheet.Cells[row, col].Style.Border.Bottom.Color.SetColor(Color.White);
            worksheet.Cells[row, col].Style.Border.Left.Color.SetColor(Color.White);
            worksheet.Cells[row, col].Style.Border.Right.Color.SetColor(Color.White);
        }

        private static void WorksheetTableCell(ExcelWorksheet worksheet, int row, int col, object value, Type type, string format, double rowHeight, float fontSize, Color color)
        {
            worksheet.Row(row).Height = rowHeight;
            worksheet.Cells[row, col].Value = value;
            worksheet.Cells[row, col].Style.Font.Bold = false;
            worksheet.Cells[row, col].Style.Font.Size = fontSize;
            worksheet.Cells[row, col].Style.Font.Color.SetColor(color);
            worksheet.Cells[row, col].Style.WrapText = false;
            worksheet.Cells[row, col].Merge = false;
            worksheet.Cells[row, col].Style.Numberformat.Format = string.IsNullOrWhiteSpace(format) ? string.Empty : format;

            if (type.IsNumeric())
            {
                worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
            else
            {
                worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            }
            
            worksheet.Cells[row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[row, col].Style.Border.Left.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[row, col].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            worksheet.Cells[row, col].Style.Border.Top.Color.SetColor(Color.White);
            worksheet.Cells[row, col].Style.Border.Bottom.Color.SetColor(Color.White);
            worksheet.Cells[row, col].Style.Border.Left.Color.SetColor(Color.White);
            worksheet.Cells[row, col].Style.Border.Right.Color.SetColor(Color.White);
        }
    }
}