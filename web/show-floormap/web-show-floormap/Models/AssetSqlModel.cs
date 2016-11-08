using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_show_floormap.Models
{
    public class AssetSqlModel
    {
        public string LogicalName { get; set; }
        public string BuildingId { get; set; }
        public string Floorid { get; set; }
        public string Areaid { get; set; }
        public string StatusChar { get; set; }
        public DateTime SampleTime { get; set; }
        public string Type { get; set; }
    }
}