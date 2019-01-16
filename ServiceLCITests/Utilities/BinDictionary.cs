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
    class BinDictionary
    {
        public static LCIEntities context;
        private List<BinEvent> binEvents = new List<BinEvent>();
        public Dictionary<string, BinEvent> binEventData = new Dictionary<string, BinEvent>();

        public List<BinEvent> BinEvents
        {
            get
            {
                return binEvents;
            }

            set
            {
                binEvents = new List<BinEvent>();
            }
        }
        public void PopulateBinEvents()
        {
  
            context = new LCIEntities();

            var binEvnts = context.BinEvents;
     
            foreach (BinEvent item in binEvnts)
            {
                Console.WriteLine("----ITEM :"+item.BinId);
              //  binEventData.Add(item.BinId, item);
                binEvents.Add(item);
            }
        }
    
    }
}
