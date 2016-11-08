using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob_admin_worker
{
    public class AdminMessageSB
    {
        public string messageType { get; set; }
        public string externalAssetid { get; set; }
        public string logicalName { get; set; }
        public string type { get; set; }
        public string category { get; set; }
        public string subCategory { get; set; }
        public string areaId { get; set; }
        public string floorId { get; set; }
        public string buildingId { get; set; }
    }
}
