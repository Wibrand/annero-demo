using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Host;
using Dapper;

namespace webjob_admin_worker
{
    public class Functions
    {
        private readonly static Lazy<DatabaseConnection> _databaseConnection =
            new Lazy<DatabaseConnection>(() => new DatabaseConnection(MyConfigurationManager.GetConnectionString("SqlConnectionString", MyConfigurationManager.ConnectionType.AzureSqlDatabase)));


        // TODO: Make ServiceBusTrigger configurable in config
        public static void ProcessServiceBusMessages([ServiceBusTrigger("admin-messages", "admin-messages-subscription")] string message, TextWriter logger)
        {
            var adminFromServiceBus = JsonConvert.DeserializeObject<AdminMessageSB>(message);
            UpdateDatabase(adminFromServiceBus);

            logger.Write("Admin message: " + adminFromServiceBus);
        }

        public static void UpdateDatabase(AdminMessageSB message)
        {
            _databaseConnection.Value.Connection.Execute("dbo.UpdateAsset", commandType: System.Data.CommandType.StoredProcedure, 
                param: new {
                    externalAssetId = message.externalAssetid,
                    logicalName = message.logicalName,
                    type = message.type,
                    category = message.category,
                    subCategory = message.subCategory,
                    areaId = message.areaId,
                    floorId = message.floorId,
                    buildingId = message.buildingId
            });
        }
    }
}
