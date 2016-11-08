using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webjob_simulate_yanzi_devices.Model;

namespace webjob_simulate_yanzi_devices
{
    class AreaDispatcher
    {
        private const string OCCUPIED = "occupied";
        private const string FREE = "free";

        EventHubService eventHubService;

        public List<Area> Areas { get; set; }

        public AreaDispatcher()
        {
            Areas = new List<Area>();
            eventHubService = new EventHubService();
        }

        public void Run(object s)
        {
            Trace.TraceInformation($"[{DateTime.Now}] Check to see if any areas need to change Status");

            foreach (var area in Areas)
            {

                var currentTime = DateTime.Now;

                if (currentTime > area.FreeTimeStart && area.Status != FREE)
                {
                    Trace.TraceInformation($"[{DateTime.Now}] Area: {area.DeviceInformation.LogicalName} --> FREE");

                    area.Status = FREE;
                    sendToEventHub(area);

                    area.whenShouldNextItBeOccupied();

                }
                else if (currentTime > area.OccupiedTimeStart && area.Status != OCCUPIED)
                {
                    Trace.TraceInformation($"[{DateTime.Now}] Area: {area.DeviceInformation.LogicalName} --> OCCUPIED");

                    area.Status = OCCUPIED;
                    sendToEventHub(area);

                }
            }
        }

        private void sendToEventHub(Area area)
        {
            SubscribeData subscribeData = SampleAsset.Create(area.DeviceInformation.ExternalAssetId, area.Status);
            var subscribeDataJsonString = JsonConvert.SerializeObject(subscribeData);
            eventHubService.SendAsync(subscribeDataJsonString).Wait();
        }
    }
}
