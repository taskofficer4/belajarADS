using System;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using Wmo.Extension;

namespace Actris.Abstraction.Model.Entities
{
    public class DbContextMapper : ActrisContext
    {

        public DbContextMapper() : base(GetConnectionString("ActrisContext"))
        {
        }

        public static string GetConnectionString(string connectionName)
        {
            try
            {
                string connectionStringRaw = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                string encrypted = new EntityConnectionStringBuilder(connectionStringRaw).ProviderConnectionString;
                string decrypted = decConn(encrypted);
                connectionStringRaw = connectionStringRaw.Replace(encrypted, decrypted);                
                return connectionStringRaw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private static string decConn(string con)
        {

            Encryption Enc = Encryption.GetInstance;

            var key = ConfigurationManager.AppSettings["Con:Key"];

            if (string.IsNullOrEmpty(key))
            {
                return con;
            }

            return Enc.Decrypt(con, Enc.Decrypt(key));

        }

    }
}
