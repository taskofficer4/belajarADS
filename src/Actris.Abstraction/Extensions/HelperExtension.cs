using Actris.Abstraction.Model.View;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Actris.Abstraction.Extensions
{
   public static class HelperExtension
   {
      public static string ToTitleCase(this string val)
      {
         if (val == null)
         {
            return "";
         }
         return string.Concat(val.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
      }

      public static string ToDisplay(this decimal? val, int? decimalLength = null)
      {
         if (val == null)
         {
            return "-";
         }
         if (decimalLength != null)
         {
            return val.Value.ToString("N" + decimalLength);
         }
         return val.Value.ToString("N");
      }

      public static string ToDisplay(this string val)
      {
         if (string.IsNullOrEmpty(val))
         {
            return "-";
         }

         return val;
      }

      public static string ToDisplay(this int? val)
      {
         if (val == null)
         {
            return "-";
         }

         return val.ToString();
      }



      public static LookupList ToLookupList(this IEnumerable<LookupItem> items)
      {
         var result = new LookupList(items);
         return result;
      }

   }
}
