using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webjob_simulate_yanzi_devices.Util;

namespace webjob_simulate_yanzi_devices.Model
{
    class Area : Sensor
    {
        public DateTime OccupiedTimeStart { get; private set; }
        public DateTime FreeTimeStart { get; private set; }
        public string Status { get; set; }

        public Area(string id) : base(id)
        {
            whenShouldNextItBeOccupied();
        }

        public void whenShouldNextItBeOccupied()
        {
            var currentTime = DateTime.Now;
            if (currentTime.Hour > 18) // evening, all have gone home to play badminton
            {
                // no new occupied today, wait until next day
                var t = currentTime.AddDays(1);
                var earlistTimeTomorrow = new DateTime(t.Year, t.Month, t.Day, 6, 0, 0);
                OccupiedTimeStart = RandomFunctions.NextTimeOfDayFromCurrentTime(earlistTimeTomorrow);
            }
            else
            {
                OccupiedTimeStart = RandomFunctions.NextTimeOfDayFromCurrentTime();
            }

            FreeTimeStart = RandomFunctions.NextTimeOfDayFromCurrentTime(OccupiedTimeStart);
        }

        public override string ToString()
        {
            return $"{DeviceInformation.LogicalName} - {OccupiedTimeStart} -> {FreeTimeStart}";
        }
    }
}
