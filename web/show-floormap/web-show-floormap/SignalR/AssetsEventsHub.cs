using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_show_floormap.Models;

namespace web_show_floormap.SignalR
{
    public class AssetsEventsHub : Hub
    {
        private readonly AssetsEventsService _assetsEventsService;

        public AssetsEventsHub() : this(AssetsEventsService.Instance) { }

        public AssetsEventsHub(AssetsEventsService assetsEventsService)
        {
            _assetsEventsService = assetsEventsService;
        }

        public IEnumerable<Asset> GetAllAssetsStatus()
        {
            return _assetsEventsService.GetAllAssetsStatus();
        }
    }
}