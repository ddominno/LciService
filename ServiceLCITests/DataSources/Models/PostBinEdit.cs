using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLCITests.DataSources.Models
{

    public class PostBinEdit
    {
        public string BinId { get; set; }
        public int UnitQty { get; set; }
        public string Uom { get; set; }
        public string UnitSize { get; set; }
        public string TermLength { get; set; }
        public string EventDate { get; set; }
        public string EditedBy { get; set; }
        public Origin Origin { get; set; }
        public Destination Destination { get; set; }
        public Warehouselocation WarehouseLocation { get; set; }
        public Adjustreason AdjustReason { get; set; }
    }

    public class Origin
    {
        public string ApprovedForStorageBy { get; set; }
        public string PackDate { get; set; }
        public string GrowerNbr { get; set; }
        public string RanchNbr { get; set; }
        public Block Block { get; set; }
    }

    public class Block
    {
        public string BlockDesignator { get; set; }
        public string Variety { get; set; }
        public string FieldType { get; set; }
        public string BerryType { get; set; }
        public string Treatment { get; set; }
    }

    public class Destination
    {
        public string ExpectedReleaseDate { get; set; }
        public string GrowerPickupDate { get; set; }
        public object GrowerNbr { get; set; }
        public object RanchNbr { get; set; }
        public object Block { get; set; }
    }

    public class Warehouselocation
    {
        public string WarehouseNbr { get; set; }
        public string Sublocation { get; set; }
    }

    public class Adjustreason
    {
        public string Code { get; set; }
        public string Descript { get; set; }
    }

}
