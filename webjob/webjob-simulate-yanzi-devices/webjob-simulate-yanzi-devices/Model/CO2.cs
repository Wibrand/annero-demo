using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webjob_simulate_yanzi_devices.Util;

namespace webjob_simulate_yanzi_devices.Model
{
    class CO2 : Sensor
    {
        public int CurrentCO2 { get; set; }

        public CO2(string id) : base(id)
        {
            CurrentCO2 = RandomFunctions.InitialCO2();
        }

        public void NextRandomCO2()
        {
            CurrentCO2 = CurrentCO2 + RandomFunctions.DeltaCO2Value();
        }

        public override string ToString()
        {
            return $"{DeviceInformation.LogicalName} - {CurrentCO2}";
        }
    }
}
