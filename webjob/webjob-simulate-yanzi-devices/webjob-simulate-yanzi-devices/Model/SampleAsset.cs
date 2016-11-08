using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Model
{
    class SampleAsset
    {
        private SampleAsset() { }

        public static SubscribeData Create(string did, string name)
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
                                Name = "unitState"
                            }
                        },
                        List = new SampleEvent[] { new SampleEvent
                        {
                            ResourceType = "SampleAsset",
                            SampleTime = time,
                            AssetState = new AssetState()
                            {
                                ResourceType = "AssetState",
                                Name = name
                            }
                        }
                        }
                    }
                }
            };

            return s;
        }

    }
}
