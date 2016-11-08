using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_show_floormap.Models
{
    public class Asset
    {
        public string LogicalName { get; set; }
        public string AreaId { get; set; }
        public string FloorId { get; set; }
        public AssetStatusEnum Status { get; set; }
        public DateTime TimeWhenRecorded { get; set; }
        public string Type { get; set; }
    }

    public enum AssetStatusEnum
    {
        Occupied,
        Free,
        None = -1
    }
}