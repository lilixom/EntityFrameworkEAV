using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP.FileService.Infrastructure
{
    public class ConnectionManager
    {
        private static DbProviderFactory dbProviderFactory;
        private static string[] providerNames = new string[] { "Oracle.DataAccess.Client", "Oracle.ManagedDataAccess.Client", "System.Data.OracleClient" };

        static ConnectionManager()
        {
            dbProviderFactory = GetDbProviderFactory();
        }

        public static IDbConnection GetConnection(string configName)
        {
            var connectionString = GetConnectionString(configName);
            var connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        private static string GetConnectionString(string configName)
        {
            var connectionString = string.Empty;
            if (ConfigurationManager.ConnectionStrings[configName] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings[configName].ConnectionString;
            }
            else if (ConfigurationManager.AppSettings[configName] != null)
            {
                connectionString = ConfigurationManager.AppSettings[configName];
            }
            else
            {
                var domainPath = AppDomain.CurrentDomain.BaseDirectory;
                TAP.Utility.Logger.LoggerManager.Debug("domainPath:" + domainPath);
                var dataSourceFileName = Path.Combine(domainPath, "DataSources.config");
                if (File.Exists(dataSourceFileName))
                {
                    var docment = System.Xml.Linq.XDocument.Load(dataSourceFileName);
                    var dataSourceNodes = docment.Descendants("DataSource").ToList();
                    var sourceNode = dataSourceNodes.FirstOrDefault(t => t.Element("Name").Value == configName);
                    if (sourceNode != null)
                    {
                        connectionString = sourceNode.Element("ConnectionString").Value;
                    }
                    else
                    {
                        throw new Exception(string.Format("在DataSources.config文件中，找不到Name等于{0}的配置。", configName));
                    }
                }
                else
                {
                    throw new Exception(string.Format("在运用程序配置文件中找不到{0}的配置，并且不存在DataSources.config文件。", configName));
                }
            }
            return connectionString;
        }

        private static DbProviderFactory GetDbProviderFactory()
        {
            foreach (var providerName in providerNames)
            {
                try
                {
                    var factory = DbProviderFactories.GetFactory(providerName);
                    if (factory != null)
                    {
                        return factory;
                    }
                }
                catch (ArgumentException)
                {
                }
            }
            return null;
        }
    }
}