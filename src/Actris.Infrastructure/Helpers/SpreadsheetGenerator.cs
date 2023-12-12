using ClosedXML.Excel;
using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;
using Actris.Abstraction.Model.View;

namespace Actris.Infrastructure.Helpers
{
    public static class SpreadsheetGenerator
    {
        public static XLWorkbook Generate<T>(string title, 
            IEnumerable<T> items, 
            List<ColumnDefinition> columnDefinitions)
        {
            XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();
            wb = Generate(wb, title, items, columnDefinitions);
            return wb;
        }

        public static XLWorkbook Generate<T>(XLWorkbook wb, string title,
            IEnumerable<T> items,
            List<ColumnDefinition> columnDefinitions)
        {
            IXLWorksheet ws = wb.AddWorksheet(title);

            // header
            var col = 1;
            foreach (var columnDefinition in columnDefinitions)
            {
                // set column name with format text
                ws.Cell(1, col).SetValue(columnDefinition.Name);
                ws.Cell(1, col).Style.Font.Bold = true;
                ws.Cell(1, col).Style.Fill.BackgroundColor = XLColor.FromHtml("#D9D9D9");
                col++;
            }
            // rows mulai dari row 2
            var row = 2;
            foreach (var item in items)
            {
                Type t = item.GetType();
                PropertyInfo[] props = t.GetProperties();

                // tiap col
                col = 1;
                foreach (var columnDefinition in columnDefinitions)
                {
                    var prop = props.FirstOrDefault(o => o.Name == columnDefinition.Id);
                    var val = prop.GetValue(item);

                    if (val != null)
                    {
                        // set column format
                        switch (columnDefinition.Type)
                        {
                            case ColumnType.Number:
                                if (prop.PropertyType == typeof(Int32))
                                {
                                    ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Integer);
                                }
                                else if (prop.PropertyType == typeof(decimal?))
                                {
                                    ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Precision2WithSeparator);
                                }
                                else
                                {
                                    ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.General);
                                }
                                break;
                            case ColumnType.Percentage:
                         
                          
                                    ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.PercentPrecision2);
                                
                                break;
                            case ColumnType.Link:

                                ws.Cell(row,col).Hyperlink = new XLHyperlink(val.ToString());
                                
                                break;
                            case ColumnType.DateTime:
                                ws.Cell(row, col).Style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.DateTime.MonthDayYear4WithDashesHour24Minutes);
                                break;
                            case ColumnType.Date:
                                if (prop.PropertyType == typeof(DateTime?))
                                {
                                    var valDateTime = (DateTime?)prop.GetValue(item);
                                    val = valDateTime.Value.Date;
                                }
                                ws.Cell(row, col).SetDataType(XLDataType.DateTime);
                                ws.Cell(row, col).Style.NumberFormat.Format = "dd-mmm-yyyy";
                                break;
                            case ColumnType.RichText:
                                ws.Cell(row, col).Style.Alignment.WrapText = true;
                                break;
                        }
                        ws.Cell(row, col).SetValue(val);
                    }
                    col++;
                }
                row++;
            }

            //auto fit column
            ws.Columns(1, columnDefinitions.Count()).AdjustToContents();
            ws.SheetView.FreezeRows(1);
            return wb;
        }
    }
}
