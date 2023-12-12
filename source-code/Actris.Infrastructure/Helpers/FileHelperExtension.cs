using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Actris.Infrastructure.Helpers
{
    public static class FileHelperExtension
    {
        public static string[] AllowedImages = new[] { ".jpg", ".png", ".jpeg" };
        public static string[] AllowedFiles = new[] { ".jpg", ".png", ".jpeg", ".pdf", ".xlsx", ".pptx", ".xls", ".ppt" };


        private const long OneKb = 1024;
        private const long OneMb = OneKb * 1024;
        private const long OneGb = OneMb * 1024;
        private const long OneTb = OneGb * 1024;

        public static string ToPrettySize(this int value, int decimalPlaces = 1)
        {
            return ((long)value).ToPrettySize(decimalPlaces);
        }

        public static string ToPrettySize(this long value, int decimalPlaces = 1)
        {
            var asTb = Math.Round((double)value / OneTb, decimalPlaces);
            var asGb = Math.Round((double)value / OneGb, decimalPlaces);
            var asMb = Math.Round((double)value / OneMb, decimalPlaces);
            var asKb = Math.Round((double)value / OneKb, decimalPlaces);
            string chosenValue = asTb > 1 ? string.Format("{0}Tb", asTb)
                : asGb > 1 ? string.Format("{0}GB", asGb)
                : asMb > 1 ? string.Format("{0}MB", asMb)
                : asKb > 1 ? string.Format("{0}KB", asKb)
                : string.Format("{0} B", Math.Round((double)value, decimalPlaces));
            return chosenValue;
        }

        public static bool IsImage(this string fileName)
        {
           
            var ext = fileName.GetFileExtension();
            if (AllowedImages.Contains(ext.ToLower()))
            {
                return true;
            }

            return false;
        }

        public static bool IsAllowedFiles(this string fileName)
        {

            var ext = fileName.GetFileExtension();
    

            if (AllowedFiles.Contains(ext.ToLower()))
            {
                return true;
            }

            return false;
        }


        public static string GetFileExtension(this string fileName)
        {
            return  Path.GetExtension(fileName);
        }


        public static string GetFileUrl(string fileName)
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            if (!baseUrl.EndsWith("/"))
            {
                baseUrl += "/";
            }
            return baseUrl + "Upload/" + fileName;
        }
    }
}
