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
    class CO2Dispatcher
    {
        public List<CO2> CO2s { get; set; }

        EventHubService eventHubService;

        public CO2Dispatcher()
        {
            CO2s = new List<CO2>();
            eventHubService = new EventHubService();
        }

        public void Run(object s)
        {
            Trace.TraceInformation($"[{DateTime.Now}] Update CO2s");

            foreach (var CO2 in CO2s)
            {
                CO2.NextRandomCO2();
                sendToEventHub(CO2);
            }
        }

        private void sendToEventHub(CO2 CO2)
        {
            SubscribeData subscribeData = SampleCO2.Create(CO2.DeviceInformation.ExternalAssetId, CO2.CurrentCO2); 
            var subscribeDataJsonString = JsonConvert.SerializeObject(subscribeData);
            eventHubService.SendAsync(subscribeDataJsonString).Wait();
        }
    }
}