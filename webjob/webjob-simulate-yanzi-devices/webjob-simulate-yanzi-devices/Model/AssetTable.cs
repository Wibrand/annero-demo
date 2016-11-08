using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_simulate_yanzi_devices.Model
{
    class AssetTable
    {
        [JsonProperty(PropertyName = "internalAssetId")]
        public int InternalAssetId { get; set; }

        [JsonProperty(PropertyName = "externalAssetId")]
        public string ExternalAssetId { get; set; }

        [JsonProperty(PropertyName = "logicalName")]
        public string LogicalName { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "subCategory")]
        public string SubCategory { get; set; }

        [JsonProperty(PropertyName = "areaId")]
        public string AreaId { get; set; }

        [JsonProperty(PropertyName = "floorId")]
        public string FloorId { get; set; }

        [JsonProperty(PropertyName = "buildingId")]
        public string BuildingId { get; set; }

        [JsonProperty(PropertyName = "lastUpdated")]
        public DateTime LastUpdated { get; set; }

    }
}
