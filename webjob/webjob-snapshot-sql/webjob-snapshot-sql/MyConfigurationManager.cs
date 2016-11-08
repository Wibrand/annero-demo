using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_snapshot_sql
{
    class MyConfigurationManager
    {
        internal enum ConnectionType 
        {
            SqlServer = 1,
            MySql,
            AzureSqlDatabase,
            Custom
        }

        internal static string GetAppSettings(string name)
        {
            var setting = Environment.GetEnvironmentVariable(name);
            if(string.IsNullOrEmpty(setting))
            {
                return ConfigurationManager.AppSettings[name];
            }
            else
            {
                return setting;
            }
        }

        internal static string GetConnectionString(string name, ConnectionType connectionType)
        {
            string settingprefix;

            switch (connectionType)
            {
                case ConnectionType.SqlServer:
                    settingprefix = "SQLCONNSTR_";
                    break;
                case ConnectionType.MySql:
                    settingprefix = "MYSQLCONNSTR_";
                    break;
                case ConnectionType.AzureSqlDatabase:
                    settingprefix = "SQLAZURECONNSTR_";
                    break;
                case ConnectionType.Custom:
                    settingprefix = "CUSTOMCONNSTR_";
                    break;
                default:
                    settingprefix = "SQLAZURECONNSTR_";
                    break;
            }

            var setting = Environment.GetEnvironmentVariable(settingprefix + name);
            if(string.IsNullOrEmpty(setting))
            {
                return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            else
            {
                return setting;
            }
        }
    }
}
