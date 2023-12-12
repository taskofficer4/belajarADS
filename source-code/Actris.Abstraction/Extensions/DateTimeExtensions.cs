using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CoverPageProps;

namespace Actris.Abstraction.Extensions
{
    public static class DateHelper
    {
        /// <summary>
        /// Waktu Indonesia Barat (+7)
        /// Untuk menghindari perbedaan zona waktu server
        /// - hosting di localhost UTC+7
        /// - hosting di server biasanya UTC+0
        /// dengan method ini datetime now di convert ke +7 dari manapun zona waktu nya
        /// </summary>
        public static DateTime WibNow
        {
            get
            {
                var dff = 7 - TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).Hours;
                return DateTime.Now.AddHours(dff);
            }
        }
        public static DateTime SetDay(this DateTime date, int day)
        {
            return new DateTime(date.Year, date.Month, day);
        }

        public static DateTime SetTime(this DateTime date, int h, int m, int s)
        {
            return new DateTime(date.Year, date.Month, date.Day, h, m , s);
        }

        public static string TimeStampNow()
        {
           return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
        }

        public static int TimeStampNowInt()
        {
            return (int) new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }

        public static string ToDisplay(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToDisplay();
            }
            return "";
        }

        public static string ToDisplay(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MMM-yyyy");
        }
    }
}
