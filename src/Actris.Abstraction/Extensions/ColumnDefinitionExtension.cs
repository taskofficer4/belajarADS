using System;
using System.Linq;
using Actris.Abstraction.Extensions;
using Actris.Abstraction.Model.View;

namespace Actris.Abstraction.Helper
{
    public static class ColumnDefinitionExtension
    {
        public static string GetValue(this ColumnDefinition columnDefinition, Object modelDto)
        {

            var listProperty = modelDto.GetType().GetProperties();
            var property = listProperty.FirstOrDefault(o => o.Name == columnDefinition.Id);
            if (property == null)
            {
                return "";
            }

            object rawValue = property.GetValue(modelDto);

            if (rawValue == null)
            {
                return "";
            }
            string val = rawValue.ToString();

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
                            val = dateTime.ToDisplay();
                        }
                        else
                        {
                            val = "";
                        }
                        break;
                    }

                case ColumnType.DateTime:
                    {
                        var dateTime = ((DateTime?)rawValue);

                        if (dateTime.HasValue)
                        {
                            val = dateTime.Value.ToString("yyyy-MM-dd HH:mm");
                        }

                        break;
                    }



                case ColumnType.Number:
                    val = Double.Parse(rawValue.ToString()).ToString("N");
                    break;
            }

            return val;
        }
    }
}
