using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalOnDemand.model
{
    public class MonitorADF
    {
        public string PipelineName { get; set; }
        public string RunStart { get; set; }
        public string DurationInMs { get; set; }
        public string Status { get; set; }
        public string RunId { get; set; }
        public string RunEnd { get; set; }
    }
}