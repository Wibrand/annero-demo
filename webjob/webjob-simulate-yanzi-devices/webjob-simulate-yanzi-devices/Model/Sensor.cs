using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Model
{
    class Sensor
    {
        public string Id { get; private set; }
        public SimulatedDevice DeviceInformation { get; set; }

        public Sensor(string id)
        {
            Id = id;
        }
    }

}
