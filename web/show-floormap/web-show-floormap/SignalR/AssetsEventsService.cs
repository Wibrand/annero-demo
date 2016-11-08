using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Azure;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using web_show_floormap.Models;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace web_show_floormap.SignalR
{
    // Is inspired by this article http://serena-yeoh.blogspot.se/2014/04/serializing-enums-in-aspnet-signalr.html
    public class AssetsEventsService
    {

        private readonly static Lazy<AssetsEventsService> _instance = 
            new Lazy<AssetsEventsService>(() => new AssetsEventsService(GlobalHost.ConnectionManager.GetHubContext<AssetsEventsHub>().Clients));

        private readonly ConcurrentDictionary<string, Asset> _assets = new ConcurrentDictionary<string, Asset>();

        private readonly object _updateAssetsInfoLock = new object();
        private volatile bool _updatingAssetsInfo = false;

        private Random rnd = new Random();
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(250);

        private SubscriptionClient _serviceBusClient;

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public static AssetsEventsService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public IEnumerable<Asset> GetAllAssetsStatus()
        {
            return _assets.Values;
        }

        private AssetsEventsService(IHubConnectionContext<dynamic> clients)
        {
            System.Diagnostics.Trace.TraceInformation("AssetsEventsService: initialize");

            Clients = clients;

            initializeDeskInfoCol();

            string connectionString = ConfigurationManager.ConnectionStrings["Microsoft.ServiceBus.ConnectionString"].ConnectionString;
            string topic = ConfigurationManager.AppSettings["Microsoft.ServiceBus.Topic"];
            string subscription = ConfigurationManager.AppSettings["Microsoft.ServiceBus.Subscription"];

            _serviceBusClient = SubscriptionClient.CreateFromConnectionString(connectionString, topic, subscription, ReceiveMode.ReceiveAndDelete);

            _serviceBusClient.OnMessage((message) =>
            {
                try
                {
                    // Process message from subscription.
                    var assetsFromServiceBus = JsonConvert.DeserializeObject<AssetServiceBus>(message.GetBody<string>());
                    updateAssetState(assetsFromServiceBus.logicalName, assetsFromServiceBus.state, assetsFromServiceBus.time);

                }
                catch (Exception)
                {
                    // Indicates a problem, unlock message in subscription.
                    System.Diagnostics.Trace.TraceError("AssetsEventsService: can't read message from topic");
                }
            });


        }

        private void updateAssetState(string logicalName, string status, DateTime timeWhenRecorded)
        {
            lock (_updateAssetsInfoLock)
            {
                if (!_updatingAssetsInfo)
                {
                    _updatingAssetsInfo = true;

                    if (_assets.ContainsKey(logicalName))
                    {
                        _assets[logicalName].Status = status.ToLower() == "free"
                            ? AssetStatusEnum.Free : AssetStatusEnum.Occupied;
                        _assets[logicalName].TimeWhenRecorded = timeWhenRecorded;

                        BroadcastAssetStatusChanged(_assets[logicalName]);

                    }
                    _updatingAssetsInfo = false;
                }
            }
        }

        private void initializeDeskInfoCol()
        {
            _assets.Clear();

            System.Diagnostics.Trace.TraceInformation("AssetsEventsService:initializeDeskInfoCol call database");
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

            SqlConnection _databaseConnection = new SqlConnection(sqlConnectionString);

            var assetsFromSql = _databaseConnection.Query<AssetSqlModel>("dbo.GetLatestStatusForAssets", commandType: System.Data.CommandType.StoredProcedure);

            foreach (var asset in assetsFromSql)
            {
                _assets.TryAdd(asset.LogicalName.Trim(),
                    new Asset()
                    {
                        LogicalName = asset.LogicalName.Trim(),
                        FloorId = asset.Floorid.Trim(),
                        AreaId = asset.Areaid.Trim(),
                        Type = asset.Type.Trim(),
                        Status = string.IsNullOrEmpty(asset.StatusChar) ? 
                             AssetStatusEnum.None
                            : (asset.StatusChar.Trim().ToLower() == "free" ? AssetStatusEnum.Free : AssetStatusEnum.Occupied),
                        TimeWhenRecorded = asset.SampleTime
                    });

            }
        }

        private void BroadcastAssetStatusChanged(Asset asset)
        {
            Clients.All.assetStatusChanged(asset);
        }
    }
}