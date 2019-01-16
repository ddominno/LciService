using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLCITests.DataSources.Models
{
    public class PostBinMove
    {
        public string TargetWarehouseNbr { get; set; }
        public string TargetSublocation { get; set; }
        public string MovedBy { get; set; }
        public List<string> Bins { get; set; }
        public string EventDate { get; set; }
    }

}
