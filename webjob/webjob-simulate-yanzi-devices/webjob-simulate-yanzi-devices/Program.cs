using Microsoft.Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using webjob_simulate_yanzi_devices.Model;
using webjob_simulate_yanzi_devices.Util;
using Dapper;
using System.Data;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Diagnostics;

namespace webjob_simulate_yanzi_devices
{
    class Program
    {

        static void Main()
        {
            var autoEvent = new AutoResetEvent(false);

            var dispatcher = new Dispatcher();
            initializeSimulatorDevices(dispatcher);
            initializeDatabaseWithSimulatedData(dispatcher);

            dispatcher.Start();

            autoEvent.WaitOne();
        }

        private static void initializeDatabaseWithSimulatedData(Dispatcher dispatcher)
        {
            Trace.TraceInformation("Check if database is initialized");

            var dbConnection = new DatabaseConnection(MyConfigurationManager.GetConnectionString("SqlConnectionString", MyConfigurationManager.ConnectionType.AzureSqlDatabase));
            if (!isAlreadyDbIntialized(dbConnection))
            {
                Trace.TraceInformation("Initialize Database");
                initializeDatabase(dispatcher, dbConnection);
                createAsaRefDataBlob(dbConnection);
            }
        }

        private static void initializeDatabase(Dispatcher dispatcher, DatabaseConnection dbConnection)
        {
            foreach (var device in dispatcher.DevicesJsonInput)
            {
                dbConnection.Connection.Execute("dbo.UpdateAsset", commandType: System.Data.CommandType.StoredProcedure,
                   param: new
                   {
                       externalAssetId = device.ExternalAssetId,
                       logicalName = device.LogicalName,
                       type = device.Type,
                       category = device.Category,
                       subCategory = device.SubCategory,
                       areaId = device.AreaId,
                       floorId = device.FloorId,
                       buildingId = device.BuildingId
                   });
            }
        }

        private static void createAsaRefDataBlob(DatabaseConnection dbConnection)
        {
            Trace.TraceInformation("Create ref.data blob for Stream Analytics");

            var assets = dbConnection.Connection.Query<AssetTable>("dbo.GetChangesInAssetCollection", commandType: CommandType.StoredProcedure).ToList();

            if (assets.Count > 0)
            {
                var storageAccountName = MyConfigurationManager.GetConnectionString("storageAccountName", MyConfigurationManager.ConnectionType.Custom);
                var storageAccountKey = MyConfigurationManager.GetConnectionString("storageAccountKey", MyConfigurationManager.ConnectionType.Custom);

                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName, storageAccountKey), true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("asa-refdata");
                CloudBlockBlob blob = container.GetBlockBlobReference(string.Format("assets/{0:yyy-MM-dd'/'HH-mm}/asset.json", DateTime.UtcNow.AddMinutes(-30)));
                StringBuilder sb = new StringBuilder();

                foreach (var asset in assets)
                {
                    var jsonasset = JsonConvert.SerializeObject(asset);
                    sb.Append(jsonasset + "\n");
                }

                blob.UploadText(sb.ToString());
            }


        }

        private static bool isAlreadyDbIntialized(DatabaseConnection dbConnection)
        {
            var numberOfRecordsInAsset = dbConnection.Connection.ExecuteScalar<int>("select count(*) from Asset");
            return numberOfRecordsInAsset == 0 ? false : true;
        }

        static private void initializeSimulatorDevices(Dispatcher dispatcher)
        {
            Trace.TraceInformation("Initialize Devices");

            Uri url = new Uri(MyConfigurationManager.GetAppSettings("UrlToListOfSimulatedDevices"));

            dispatcher.DevicesJsonInput = JsonConvert.DeserializeObject<List<SimulatedDevice>>(downloadFile(url));
            buildInteralDeviceStructure(dispatcher);

        }

        static string downloadFile(Uri url)
        {
            try
            {
                var webClient = new WebClient();

                return webClient.DownloadString(url);
            }
            catch
            {
                Trace.TraceError("Can't download the file with simulated devices data");
                throw;
            }
        }

        static private void buildInteralDeviceStructure(Dispatcher dispatcher)
        {
            var areas = from device in dispatcher.DevicesJsonInput
                        where device.Type == "area" || device.Type == "desk" || device.Type == "conferenceRoom" || device.Type == "toilet"
                        select device;

            foreach (var area in areas)
            {
                dispatcher.AreaDispatcher.Areas.Add(new Area(area.ExternalAssetId)
                {
                    DeviceInformation = area
                });
            }

            var temperatures = from device in dispatcher.DevicesJsonInput
                               where device.Type == "temp"
                               select device;

            foreach (var temperature in temperatures)
            {
                dispatcher.TemperatureDispatcher.Temperatures.Add(new Temperature(temperature.ExternalAssetId)
                {
                    DeviceInformation = temperature
                });
            }

            var co2s = from device in dispatcher.DevicesJsonInput
                       where device.Type == "carbonDioxide"
                       select device;

            foreach (var co2 in co2s)
            {
                dispatcher.CO2Dispatcher.CO2s.Add(new CO2(co2.ExternalAssetId)
                {
                    DeviceInformation = co2
                });
            }
        }
    }
}
