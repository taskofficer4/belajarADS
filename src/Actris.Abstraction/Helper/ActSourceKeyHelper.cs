using System;

namespace Actris.Abstraction.Helper
{
    public static class ActSourceKeyHelper
    {
        private static string DelimiterKey = "~~~~";
        public static string ToKeyString(string source, string directorate, string division, string subDivision, string department)
        {

            return $"{source}{DelimiterKey}{directorate}{DelimiterKey}{division}{DelimiterKey}{subDivision}{DelimiterKey}{department}";
        }

        public static (string source, string directorateID, string divisionID, string subDivisionID, string departmentID) FromKeyString(string strKey)
        {
            var splitKey = strKey.Split(new string[] { DelimiterKey }, StringSplitOptions.None);
            if (splitKey.Length != 5)
            {
                throw new Exception("key not valid");
            }
            var source= splitKey[0];
            var directorateID = splitKey[1];
            var divisionID = splitKey[2];
            var subDivisionID = splitKey[3];
            var departmentID = splitKey[4];

            return (source,directorateID,divisionID,subDivisionID,departmentID);
        }
    }

}
