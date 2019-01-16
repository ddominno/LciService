using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriscTAF.Utilities;
using ServiceLCITests.DataSources.Models;
using Newtonsoft.Json;

namespace ServiceLCITests.Utilities
{
    using System.IdentityModel.Tokens;
    using System.Windows.Forms.VisualStyles;

    using MongoDB.Bson.IO;

    using Newtonsoft.Json;

    using RestSharp.Authenticators;

    public class PayloadGenerator
    {
        //private static string binId;
        
        //DTAP an active ranch number needs to be retrieved from MDR database
        private static string ranchNbr = "0008704";

        //private static string payload = "";

        public static string TimeStamp()
        {
            return DateTime.Now.ToString("yyyy'-'MM'-'dd HH:mm:ss");
        }

        public static string PostReceipt(string binIdValue)
        {
            PostReceiptInspection InspectionValues =
                new PostReceiptInspection()
                    {
                        EventType = "Inspection",
                        InspectedBy = "autotest.inspector",
                        BinAirTemp = "30",
                        BinHumidity = "50",
                        TempMonitorId = "3000",
                        SubstrateMoisturePct = "50",
                        SubstrateEc = "1",
                        SubstratePh = "3",
                        BotrytisLevel = "3",
                        BinId = binIdValue,
                        EventDate = TimeStamp()
                    };

            List<Binids> binidlistValues = new List<Binids>();
            Binids binIdValueForList = new Binids()
                                            {
                                                BinId = binIdValue,
                                                UnitQty = 20,
                                                ExpectedReleaseDate = TimeStamp(),
                                                substrateEc = "5",
                                                substrateMoisturePct = "50",
                                                binAirTemp = "20",
                                                tempMonitorId = "2000"
                                            };

            binidlistValues.Add(binIdValueForList);

            PostReceipt postModel = new PostReceipt
                                        {
                                            EventType = "ReceiptCreate",
                                            WarehouseNbr = "0001",
                                            Sublocation = "sublocTest",
                                            ReceivingTagNbr = binIdValue,
                                            ReceiptDate = TimeStamp(),
                                            CoolingEntryDate = TimeStamp(),
                                            ExpectedReleaseDate = TimeStamp(),
                                            UnitQty = "25",
                                            ReceivedBy = "autotest.receiver",
                                            RanchNbr = ranchNbr,
                                            Block = "A1",
                                            Inspection = InspectionValues,
                                            BinId = binIdValue,
                                            BinIdList = binidlistValues,
                                            EventDate = TimeStamp()
                                        };

            return Newtonsoft.Json.JsonConvert.SerializeObject(postModel);
        }    

        public static string PostBinInspectionRemove(string binIdValue, string id)
        {
            PostBinInspectionRemove postModel =
                new PostBinInspectionRemove()
                    {
                        BinId = binIdValue,
                        DeletedBy = "autotest.deleter",
                        EventDate = TimeStamp(),
                        EventType = "Inspection",
                        Id = id
                    };

            return Newtonsoft.Json.JsonConvert.SerializeObject(postModel);
        }

        public static string PostBinInspection(string binIdValue)
        {
            PostBinInspection postModel =
                new PostBinInspection()
                    {
                        BinAirTemp = "32",
                        BinHumidity = "50",
                        BinId = binIdValue,
                        BotrytisLevel = "5",
                        EventDate = TimeStamp(),
                        InspectedBy = "autotest.inspector",
                        SubstrateEc = "5",
                        SubstrateMoisturePct = "50",
                        SubstratePh = "5",
                        TempMonitorId = "60608075"
                    };
            return Newtonsoft.Json.JsonConvert.SerializeObject(postModel);
        }

        public static string PostBinAdjustQuantity(string binIdValue)
        {
            PostBinAdjustreason adjReason = new PostBinAdjustreason() { Code = "testAdjustCode", Descript = "Test Adjustment Code" };

            PostBinAdjustQuantity postModel =
                new PostBinAdjustQuantity()
                    {
                        AdjustReason = adjReason,
                        BinId = binIdValue,
                        ChangedBy = "autotest.adjuster",
                        EventDate = TimeStamp(),
                        IncidentDescript = "this is a test of the adjustment service",
                        NewQty = 1
                    };

            return Newtonsoft.Json.JsonConvert.SerializeObject(postModel);
        }

        public static string PostBinMove(string warehouseNbr, string sublocationNbr, string binIdValue)
        {
            List<string> binList = new List<string> { binIdValue };


            PostBinMove postModel = new PostBinMove()
                                        {
                                            TargetWarehouseNbr = warehouseNbr,
                                            TargetSublocation = sublocationNbr,
                                            MovedBy = "autotest.mover",
                                            Bins = binList,
                                            EventDate = TimeStamp()
                                        };

            return Newtonsoft.Json.JsonConvert.SerializeObject(postModel);
        }

        public static string PostBinIssuance(string issueTagNbr, string binIdValue)
        {
            BinIssuanceInspection inspectDets =
                new BinIssuanceInspection()
                    {
                        InspectedBy = "autotest.inspector",
                        BinAirTemp = "30",
                        SubstrateMoisturePct = "50",
                        SubstrateEc = "5",
                        SubstratePh = "5",
                        BotrytisLevel = "1",
                        BinId = binIdValue,
                        EventDate = TimeStamp()
                    };

            List<string> binList = new List<string> { binIdValue };

            PostBinIssuance postModel =
                new PostBinIssuance()
                    {
                        IssuanceTagNbr = issueTagNbr,
                        EventDate = TimeStamp(),
                        IssuanceDate = TimeStamp(),
                        IssuedBy = "autotest.issuer",
                        // DTAP add a way to dynamically retrieve grower and ranch values from MDR active ranches
                        // this should be based on available inventory and the ranches related to them
                        GrowerNbr = "0992000",
                        RanchNbr = "0003000",
                        Bins = binList,
                        Inspection = inspectDets
                    };

            return Newtonsoft.Json.JsonConvert.SerializeObject(postModel);
        }

        public static string PostBinEdit(string binIdValue)
        {
            Block blockVals = new Block()
                                  {
                                      //DTAP add a way to draw this information dynamically
                                      BlockDesignator = "A1",
                                      Variety = "V1C",
                                      FieldType = "Conventional",
                                      BerryType = "RASP",
                                      Treatment = "Mowdown"
                                  };

            Origin oriVals = new Origin()
                                 {
                                     ApprovedForStorageBy = "autotest.approver",
                                     PackDate = TimeStamp(),
                                     //DTAP add a way to draw this information dynamically
                                     GrowerNbr = "0991011",
                                     RanchNbr = "0007477",
                                     Block = blockVals
                                 };

            Destination destinVals = new Destination()
                                         {
                                             ExpectedReleaseDate = TimeStamp(),
                                             GrowerPickupDate = TimeStamp(),
                                             GrowerNbr = "0991011",
                                             RanchNbr = "0007477",
                                             Block = "A1"
                                         };

            PostBinEdit postModel = new PostBinEdit()
                                        {
                                            BinId = binIdValue,
                                            UnitQty = 15,
                                            Uom = "Pot",
                                            UnitSize = "Standard",
                                            TermLength = "Short",
                                            EventDate = TimeStamp(),
                                            EditedBy = "autotest.editor",
                                            Origin = oriVals,
                                            Destination = destinVals,
                                            WarehouseLocation = new Warehouselocation() { WarehouseNbr = "0001", Sublocation = "AT01" }, 
                                            AdjustReason = new Adjustreason() { Code = "TestCode", Descript = "this is a test adjust reason"}
                                         };

            return Newtonsoft.Json.JsonConvert.SerializeObject(postModel);
        }
    } 
}
