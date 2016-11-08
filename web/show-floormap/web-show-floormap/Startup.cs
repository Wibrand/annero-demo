using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(web_show_floormap.Startup))]

namespace web_show_floormap
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // to get the text from the enum that is transfered to the client
            // TODO: Do we need this!
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new StringEnumConverter());
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);

            app.MapSignalR();
        }
    }
}