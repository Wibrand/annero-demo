using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using webjob_simulate_yanzi_devices.Util;

namespace webjob_simulate_yanzi_devices.Model
{
    [DataContract]
    public class Sample
    {
        [DataMember(Name = "resourceType")]
        public string ResourceType { get; set; }

        [DataMember(Name = "sampleTime")]
        public long SampleTime { get; set; }

    }

    [DataContract]
    public class SampleEvent : Sample
    {
        [DataMember(Name = "assetState")]
        public AssetState AssetState { get; set; }
    }

    [DataContract]
    public class SampleState : Sample
    {
        [DataMember(Name = "value")]
        public double Value { get; set; }
    }

    [DataContract]
    public class SampleStateInt : Sample
    {
        [DataMember(Name = "value")]
        public int Value { get; set; }
    }

}
