using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Model
{
    [DataContract]
    public class DataSourceAddress
    {
        [DataMember(Name = "resourceType")]
        public string ResourceType { get; set; }

        [DataMember(Name = "did")]
        public string Did { get; set; }

        [DataMember(Name = "locationId")]
        public string LocationId { get; set; }

        [DataMember(Name = "serverDid")]
        public string ServerDid { get; set; }

        [DataMember(Name = "variableName")]
        public VariableName VariableName { get; set; }

        [DataMember(Name = "instanceNumber")]
        public int InstanceNumber { get; set; }
    }
}
