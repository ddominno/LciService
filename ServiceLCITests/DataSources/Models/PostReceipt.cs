using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLCITests.DataSources.Models
{
    public class PostReceipt
    {
        public string EventType { get; set; }
        public string WarehouseNbr { get; set; }
        public string Sublocation { get; set; }
        public string ReceivingTagNbr { get; set; }
        public string ReceiptDate { get; set; }
        public string CoolingEntryDate { get; set; }
        public string ExpectedReleaseDate { get; set; }
        public string UnitQty { get; set; }
        public string ReceivedBy { get; set; }
        public string RanchNbr { get; set; }
        public string Block { get; set; }
        public PostReceiptInspection Inspection { get; set; }
        public string BinId { get; set; }
        public List<Binids> BinIdList { get; set; }
        public string EventDate { get; set; }
    }

    public class PostReceiptInspection
    {
        public string EventType { get; set; }
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

    public class Binids
    {
        public string BinId { get; set; }
        public int UnitQty { get; set; }
        public string ExpectedReleaseDate { get; set; }
        public string substrateEc { get; set; }
        public string substrateMoisturePct { get; set; }
        public string binAirTemp { get; set; }
        public string tempMonitorId { get; set; }
    }
}
