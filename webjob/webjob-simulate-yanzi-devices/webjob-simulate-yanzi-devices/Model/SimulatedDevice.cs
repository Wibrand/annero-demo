using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Model
{
    class SimulatedDevice
    {
        [DataMember(Name = "externalAssetId")]
        public string ExternalAssetId { get; set; }
        [DataMember(Name = "logicalName")]
        public string LogicalName { get; set; }
        [DataMember(Name = "category")]
        public string Category { get; set; }
        [DataMember(Name = "subCategory")]
        public string SubCategory { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "assetSampleType")]
        public string AssetSampleType { get; set; }
        [DataMember(Name = "areaId")]
        public string AreaId { get; set; }
        [DataMember(Name = "floorId")]
        public string FloorId { get; set; }
        [DataMember(Name = "buildingId")]
        public string BuildingId { get; set; }
    }
}
