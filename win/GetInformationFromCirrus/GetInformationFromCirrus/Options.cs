using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace GetInformationFromCirrus
{
    class Options
    {
        [Option('u', Required = true)]
        public string UserName { get; set; }

        [Option('p', Required = true)]
        public string Password { get; set; }

        [Option('l', Required = true)]
        public string LocationId { get; set; }

        [Option('f', Required = false)]
        public string FileName { get; set; }

        [Option('s', Required = false)]
        public string ServiceBusConnectionString { get; set; }

        [Option('e', Required = false)]
        public string ExcludeTypes { get; set; }

    }
}
