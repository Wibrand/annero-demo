using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Model
{
    class SampleCO2
    {
        private SampleCO2() { }

        public static SubscribeData Create(string did, int value)
        {
            var time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            var s = new SubscribeData()
            {
                MessageType = "SubscribeData",
                TimeSent = time,
                List = new SampleList[]
                {
                    new SampleList()
                    {
                        ResourceType = "SampleList",
                        DataSourceAddress = new DataSourceAddress()
                        {
                            ServerDid = Constants.SERVERDID,
                            LocationId = Constants.LOCATIONID,
                            Did = did,
                            InstanceNumber = 0,
                            ResourceType = "DataSourceAddress",
                            VariableName = new VariableName()
                            {
                                ResourceType = "VariableName",
                                Name = "temperature"
                            }
                        },
                        List = new SampleStateInt[] { new SampleStateInt
                        {
                            ResourceType = "SampleCO2",
                            SampleTime = time,
                            Value = value
                        }
                        }
                    }
                }
            };

            return s;
        }
    }
}
