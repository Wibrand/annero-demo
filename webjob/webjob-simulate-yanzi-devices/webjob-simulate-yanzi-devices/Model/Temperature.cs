using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webjob_simulate_yanzi_devices.Util;

namespace webjob_simulate_yanzi_devices.Model
{
    class Temperature : Sensor
    {

        public double CurrentTemperature { get; set; }

        public Temperature(string id) : base(id)
        {
            CurrentTemperature = RandomFunctions.InitialTemperatureValue();
        }

        public void NextRandomTemp()
        {
            CurrentTemperature = CurrentTemperature + RandomFunctions.DeltaTemperatureValue();
        }

        public override string ToString()
        {
            return $"{DeviceInformation.LogicalName} - {CurrentTemperature}";
        }

    }

}
