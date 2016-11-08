using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Model
{
    [DataContract]
    public class VariableName
    {
        [DataMember(Name = "resourceType")]
        public string ResourceType { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
