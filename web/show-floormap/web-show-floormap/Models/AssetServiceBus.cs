using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_show_floormap.Models
{
    public class AssetServiceBus
    {
        public string logicalName { get; set; }
        public string floorid { get; set; }
        public string areaid { get; set; }
        public string state { get; set; }
        public DateTime time { get; set; }
    }

}