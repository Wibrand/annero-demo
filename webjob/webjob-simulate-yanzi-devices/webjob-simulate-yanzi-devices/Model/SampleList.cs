using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Model
{
    [DataContract]
    public class SampleList
    {
        [DataMember(Name = "resourceType")]
        public string ResourceType { get; set; }

        [DataMember(Name = "dataSourceAddress")]
        public DataSourceAddress DataSourceAddress { get; set; }

        [DataMember(Name = "list")]
        public Sample[] List { get; set; }
    }
}
