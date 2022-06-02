using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalOnDemand.model
{
    public class DuracaoPipeline
    {
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSaida { get; set; }
        public TimeSpan TempoPermanencia() => HoraSaida - HoraEntrada;
    }
}