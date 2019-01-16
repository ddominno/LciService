using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLCITests.DataSources.Models
{

    public class PostBinAdjustQuantity
    {
        public string BinId { get; set; }
        public string EventDate { get; set; }
        public int NewQty { get; set; }
        public string ChangedBy { get; set; }
        public PostBinAdjustreason AdjustReason { get; set; }
        public string IncidentDescript { get; set; }
    }

    public class PostBinAdjustreason
    {
        public string Code { get; set; }
        public string Descript { get; set; }
    }

}
