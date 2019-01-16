using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLCITests.DataSources.Models
{

    public class PostBinIssuance
    {
        public string IssuanceTagNbr { get; set; }
        public string EventDate { get; set; }
        public string IssuanceDate { get; set; }
        public string IssuedBy { get; set; }
        public string GrowerNbr { get; set; }
        public string RanchNbr { get; set; }
        public List<string> Bins { get; set; }
        public BinIssuanceInspection Inspection { get; set; }
    }

    public class BinIssuanceInspection
    {
        public string InspectedBy { get; set; }
        public string BinAirTemp { get; set; }
        public string SubstrateMoisturePct { get; set; }
        public string SubstrateEc { get; set; }
        public string SubstratePh { get; set; }
        public string BotrytisLevel { get; set; }
        public string BinId { get; set; }
        public string EventDate { get; set; }
    }

}
