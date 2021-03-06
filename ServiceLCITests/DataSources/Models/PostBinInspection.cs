﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLCITests.DataSources.Models
{

    public class PostBinInspection
    {
        public string InspectedBy { get; set; }
        public string BinAirTemp { get; set; }
        public string BinHumidity { get; set; }
        public string TempMonitorId { get; set; }
        public string SubstrateMoisturePct { get; set; }
        public string SubstrateEc { get; set; }
        public string SubstratePh { get; set; }
        public string BotrytisLevel { get; set; }
        public string BinId { get; set; }
        public string EventDate { get; set; }
    }

}
