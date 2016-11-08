using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Util
{
    class RandomFunctions
    {
        static Random rnd = new Random();

        internal static DateTime NextTimeOfDayFromCurrentTime(DateTime? date = null)
        {
            var dateToStartWith = date == null ? DateTime.Now : (DateTime)date;
            return dateToStartWith.AddMinutes(rnd.Next(20));
        }

        internal static double InitialTemperatureValue()
        {
            return (double) rnd.Next(180, 220) / 10;
        }

        internal static double DeltaTemperatureValue()
        {
            return (double) rnd.Next(-100, 100) / 100;
        }

        internal static int InitialCO2()
        {
            return rnd.Next(500, 700);
        }

        internal static int DeltaCO2Value()
        {
            return rnd.Next(-100, 100);
        }
    }
}
