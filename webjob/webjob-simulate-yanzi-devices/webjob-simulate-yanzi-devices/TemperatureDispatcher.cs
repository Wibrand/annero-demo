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
    class TemperatureDispatcher
    {
        public List<Temperature> Temperatures { get; set; }

        EventHubService eventHubService;

        public TemperatureDispatcher()
        {
            Temperatures = new List<Temperature>();
            eventHubService = new EventHubService();
        }

        public void Run(object s)
        {
            Trace.TraceInformation($"[{DateTime.Now}] Update Temperatures");

            foreach (var temperature in Temperatures)
            {
                var oldTemperature = temperature.CurrentTemperature;
                temperature.NextRandomTemp();

                sendToEventHub(temperature);
            }
        }

        private void sendToEventHub(Temperature temperature)
        {
            SubscribeData subscribeData = SampleTemp.Create(temperature.DeviceInformation.ExternalAssetId, temperature.CurrentTemperature);
            var subscribeDataJsonString = JsonConvert.SerializeObject(subscribeData);
            eventHubService.SendAsync(subscribeDataJsonString).Wait();
        }
    }
}
