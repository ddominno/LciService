using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLCITests.Utilities
{
    using System.Configuration;
    using System.Data;
    using System.Runtime.Remoting.Channels;

    using DriscTAF;
    public class DataRetriever
    {
        private BaseSqlUtil gbdb = new BaseSqlUtil(ConfigurationManager.AppSettings["connectionString"]);

        public DataTable GetInventory(string binId)
        {
            DataTable returnedValues = this.gbdb.DoQuery(
                $"SELECT TOP 1 [BinId],[UnitQty],[Uom],[UnitSize],[TermLength],[WarehouseNbr],[WarehouseSublocation] FROM [LCI].[dbo].[BinInventory] WHERE BinId = '{binId}'ORDER BY Id DESC");
            return returnedValues;
        }

        public DataTable GetInspectionDataTable()
        {
            DataTable returnedValues = this.gbdb.DoQuery($"SELECT TOP 2 [Id],[BinId] FROM [LCI].[dbo].[BinEvent] WHERE BinId != '' and EventType = 'Inspection' ORDER BY Id DESC");
            return returnedValues;
        }

        public DataTable GetEventTypeDataTable(string eventType)
        {
            DataTable returnedValues = this.gbdb.DoQuery($"SELECT TOP 2 [Id],[BinId] FROM [LCI].[dbo].[BinEvent] WHERE BinId != '' and EventType = '{eventType}' ORDER BY Id DESC");
            return returnedValues;
        }

        public DataTable GetEventById(string Id)
        {
            DataTable returnedValues = this.gbdb.DoQuery($"SELECT TOP 1 [Id], [BinId] FROM [LCI].[dbo].[BinEvent] WHERE Id = '{Id}'");
            return returnedValues;
        }
    }
}
