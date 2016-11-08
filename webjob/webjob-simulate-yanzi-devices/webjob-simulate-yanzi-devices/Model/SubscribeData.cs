using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Model
{
    public class SubscribeData
    { 
        public string MessageType { get; set; }
        public long TimeSent { get; set; }
        public SampleList[] List { get; set; }
    }
}
