using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using Actris.Abstraction.Repositories;
using Actris.Infrastructure.Constant;
using Wmo.Extension;

namespace Actris.Web.Providers
{
    public class ConnectionStringProvider : IConnectionProvider
    {
        public string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ActrisContext"].ConnectionString;
            string encrypted = new EntityConnectionStringBuilder(connectionString).ProviderConnectionString;

            string result = decConn(encrypted);
            return result;
        }

        private static string decConn(string con)
        {

            Encryption Enc = Encryption.GetInstance;
            if (AimanConstant.ConUseEncryption == false)
            {
                return con;
            }
            var key = AimanConstant.ConKey;
            if (!string.IsNullOrEmpty(key))
            {
                return Enc.Decrypt(con, Enc.Decrypt(key));
            }
            else
            {
                return con;
            }


        }
    }
}