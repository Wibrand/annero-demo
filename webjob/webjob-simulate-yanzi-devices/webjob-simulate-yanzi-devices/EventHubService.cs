using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webjob_simulate_yanzi_devices.Util;

namespace webjob_simulate_yanzi_devices
{
    class EventHubService
    {
        string connectionString = MyConfigurationManager.GetConnectionString("EventHubSendConnectionString", MyConfigurationManager.ConnectionType.Custom);
        EventHubClient eventHubClient;

        public EventHubService()
        {
            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString);
        }

        public async Task SendAsync(string message)
        {
            await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
        } 
    }
}
