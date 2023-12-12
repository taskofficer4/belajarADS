using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Actris.Abstraction.Model.View
{
    public class GridListModel
    {
        public string GridId { get; set; }
        public string GridJsVar => $"adsGrid['{GridId}']";

        public bool IsForLookup { get; set; }

        public string CrudId { get; set; }

        public List<ColumnDefinition> ColumnDefinitions { get; set; }
        public List<GridRow> Rows { get; set; }
        public FilterList FilterList { get; set; }

        public bool HideActionButton { get; set; }
        public bool HideExportButton { get; set; }

        public GridListModel()
        {
            ColumnDefinitions = new List<ColumnDefinition>();
            Rows = new List<GridRow>();
        }

        public bool ShowRowNumber { get; set; }
        public void FillRows(object obj)
        {

            // cast to list
            var list = (IList)obj;
            if (list == null || list.Count == 0)
            {
                return;
            }
            int no = 1;
            if (FilterList != null)
            {
                no = (FilterList.Page - 1 * FilterList.Size) + 1;
            }


            Rows = new List<GridRow>();
            
            var type = list[0].GetType();
            var props = type.GetProperties();

            var idColumn = ColumnDefinitions.FirstOrDefault(o => o.Type == ColumnType.Id);
            System.Reflection.PropertyInfo idProp = null;
            if (idColumn != null)
            {
                idProp = props.FirstOrDefault(o => o.Name == idColumn.Id);
            }



            foreach (var item in list)
            {
                var row = new GridRow
                {
                    No = no++,
                    Columns = new List<GridColumn>()
                };

                if (idProp != null)
                {
                    row.Id = idProp.GetValue(item).ToString();
                }


                var listProperty = props.ToList();
                if (idColumn != null)
                {
                    listProperty = props.Where(o => o.Name != idColumn.Id).ToList();
                }
                foreach (var property in listProperty)
                {
                    var columnDefinition = ColumnDefinitions.FirstOrDefault(o => o.Id == property.Name);

                    if (columnDefinition == null)
                    {
                        continue;
                    }
                    string val = "";
                    string text = "";
                    var rawValue = property.GetValue(item);

                    if (rawValue != null)
                    {
                        val = rawValue.ToString();
                        text = rawValue.ToString();
                        switch (columnDefinition.Type)
                        {
                            case ColumnType.Date:
                                {
                                    var isDateTime = (rawValue is DateTime);
                                    DateTime? dateTime;

                                    if (isDateTime)
                                    {
                                        dateTime = (DateTime?)rawValue;
                                    }
                                    else
                                    {
                                        dateTime = DateTime.Parse(val);
                                    }

                                    if (dateTime.HasValue && dateTime.Value != DateTime.MinValue)
                                    {
                                        text = dateTime.Value.ToString("d MMM yyyy");
                                        val = dateTime.Value.ToString("yyyy-MM-dd");
                                    }
                                    else
                                    {
                                        text = "";
                                        val = "";
                                    }
                                    break;
                                }

                            case ColumnType.DateTime:
                                {
                                    var dateTime = ((DateTime?)rawValue);

                                    if (dateTime.HasValue)
                                    {
                                        text = dateTime.Value.ToString("d MMM yyyy HH:mm");
                                        val = dateTime.Value.ToString("yyyy-MM-dd HH:mm");
                                    }

                                    break;
                                }

                            case ColumnType.Number:
                                text = Double.Parse(rawValue.ToString()).ToString("N0");
                                break;
                        }
                    }


                    row.Columns.Add(new GridColumn
                    {
                        ColumnLabel = columnDefinition.Name,
                        FieldId = columnDefinition.Id,
                        Type = columnDefinition.Type,
                        Value = val,
                        Text = text,
                    });
                }
                Rows.Add(row);
            }
        }

        public void FillRows<T>(IEnumerable<T> list)
        {
            int no = 1;
            if (FilterList != null)
            {
                no = ((FilterList.Page - 1) * FilterList.Size) + 1;
            }


            Rows = new List<GridRow>();

            var type = typeof(T);
            var props = type.GetProperties();

            var idColumn = ColumnDefinitions.FirstOrDefault(o => o.Type == ColumnType.Id);
            System.Reflection.PropertyInfo idProp = null;
            if (idColumn != null)
            {
                idProp = props.FirstOrDefault(o => o.Name == idColumn.Id);
            }



            foreach (var item in list)
            {
                var row = new GridRow
                {
                    No = no++,
                    Columns = new List<GridColumn>(),
                };

                if (idProp != null)
                {
                    row.Id = idProp.GetValue(item)?.ToString();
                }


                var listProperty = props.ToList();
                if (idColumn != null)
                {
                    listProperty = props.Where(o => o.Name != idColumn.Id).ToList();
                }

                int i = 0;
                foreach (var columnDefinition in ColumnDefinitions)
                {
                    var property = listProperty.FirstOrDefault(o => o.Name == columnDefinition.Id);

                    if (property == null)
                    {
                        continue;
                    }
                    string val = "";
                    string text = "";
                    var rawValue = property.GetValue(item);

                    if (rawValue != null)
                    {
                        val = rawValue.ToString();
                        text = rawValue.ToString();
                        switch (columnDefinition.Type)
                        {
                            case ColumnType.Date:
                                {
                                    var isDateTime = (rawValue is DateTime);
                                    DateTime? dateTime;

                                    if (isDateTime)
                                    {
                                        dateTime = (DateTime?)rawValue;
                                    }
                                    else
                                    {
                                        dateTime = DateTime.Parse(val);
                                    }


                                    if (dateTime.HasValue && dateTime.Value != DateTime.MinValue)
                                    {
                                        text = dateTime.Value.ToString("dd-MMM-yyyy");
                                        val = dateTime.Value.ToString("yyyy-MM-dd");
                                    }
                                    else
                                    {
                                        text = "";
                                        val = "";
                                    }
                                    break;
                                }

                            case ColumnType.DateTime:
                                {
                                    var dateTime = ((DateTime?)rawValue);

                                    if (dateTime.HasValue)
                                    {
                                        text = dateTime.Value.ToString("dd-MMM-yyyy HH:mm");
                                        val = dateTime.Value.ToString("yyyy-MM-dd HH:mm");
                                    }

                                    break;
                                }

                            case ColumnType.Percentage:
                                {
                                    var valDecimal = ((decimal?)rawValue);

                                    if (valDecimal.HasValue)
                                    {
                                        text = valDecimal.Value.ToString("P");
                                    }

                                    break;
                                }

                            case ColumnType.Number:
                                text = Double.Parse(rawValue.ToString()).ToString("N0");
                                break;
                        }
                    }

                    if (row.Id == null)
                    {
                        if (i == 0)
                        {
                            row.Id = val;
                        }
                       
                    }

                    i++;
                    row.Columns.Add(new GridColumn
                    {
                        FieldId = columnDefinition.Id,
						ColumnLabel = columnDefinition.Name,
						Type = columnDefinition.Type,
                        Value = val,
                        Text = text
                    });
                }
                Rows.Add(row);
            }
        }
    }
}
