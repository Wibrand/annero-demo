using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using webjob_simulate_yanzi_devices.Model;

namespace webjob_simulate_yanzi_devices
{
    class Dispatcher
    {
        public List<SimulatedDevice> DevicesJsonInput { get; set; }
        public AreaDispatcher AreaDispatcher { get; set; }
        public TemperatureDispatcher TemperatureDispatcher { get; set; }
        public CO2Dispatcher CO2Dispatcher { get; set; }

        Timer areaDispatcherTimer;
        Timer temperatureDispatcherTimer;
        Timer CO2DispatcherTimer;

        public Dispatcher()
        {
            AreaDispatcher = new AreaDispatcher();
            TemperatureDispatcher = new TemperatureDispatcher();
            CO2Dispatcher = new CO2Dispatcher();
        }

        public void Start()
        {
            areaDispatcherTimer = new Timer(AreaDispatcher.Run, null, 0, 10000);
            temperatureDispatcherTimer = new Timer(TemperatureDispatcher.Run, null, 0, 60000);
            CO2DispatcherTimer = new Timer(CO2Dispatcher.Run, null, 0, 60000);
        }
    }
}
