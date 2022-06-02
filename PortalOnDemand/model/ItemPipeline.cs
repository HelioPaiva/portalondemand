using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalOnDemand.model
{
    public class ItemPipeline
    {
        public string PipelineName { get; set; }
        public string PipelineID { get; set; }
        public string PipelineInicio { get; set; }
        public string PipelineDuracao { get; set; }
        public string PipelineTrigger { get; set; }
        public string PipelineStatus { get; set; }
        public string PipelineErro { get; set; }

    }
}