using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLCITests.DataSources.Models
{
    public class PostBinInspectionRemove
    {
        public string EventType { get; set; }
        public string DeletedBy { get; set; }
        public string BinId { get; set; }
        public string Id { get; set; }
        public string EventDate { get; set; }
    }

}
