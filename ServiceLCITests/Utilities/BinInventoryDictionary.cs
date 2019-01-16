using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity;
using DriscTAF.Utilities;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
namespace ServiceLCITests.Utilities
{
    class BinInventoryDictionary
    {
        public static LCIEntities context;
        private List<BinInventory> binInventories= new List<BinInventory>();
        public Dictionary<string, BinInventory> binInventoryData = new Dictionary<string, BinInventory>();

        public List<BinInventory> BinInventories
        {
            get
            {
                return binInventories;
            }

            set
            {
                binInventories = new List<BinInventory>();
            }
        }
        public void PopulateInventories()
        {
  
            context = new LCIEntities();

            var binInventories = context.BinInventories;
     
            foreach (BinInventory item in binInventories)
            {
                Console.WriteLine("----ITEM :"+item.BinId);
                //  binEventData.Add(item.BinId, item);
                binInventories.Add(item);
            }
        }
    
    }
}
