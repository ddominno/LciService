using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechTalk.SpecFlow;

namespace DriscTAF.Utilities

{
[Binding]
public class BinIDGenerator
{

    public static List<int> AllIndexesOf(string str, string value)
    {
        if (String.IsNullOrEmpty(value))
            throw new ArgumentException("the string to find may not be empty", "value");
        List<int> indexes = new List<int>();
        for (int index = 0; ; index += value.Length)
        {
            index = str.IndexOf(value, index);
            if (index == -1)
                return indexes;
            indexes.Add(index);
        }
    }

    public static string GenerateBinID()
    {
        Random random = new Random();
        string binID = "";
        int i;
        for (i = 1; i < 11; i++)
        {
            binID += random.Next(0, 9).ToString();
        }
        return binID;
    }


    private static string TenUniqueDigits(int seed)
    {
        // Locks the method in parallized runs
        //lock (obj)
        {
            DateTime today = DateTime.Now;

            // only using a 6 year span to generate tags
            int y = today.Year - 2000;   // 2 digits for year
            int mth = today.Month;
            int d = today.Day;
            int h = today.Hour;
            int m = today.Minute;
            int s = today.Second;
            int milliSecond = today.Millisecond;
            int l = seed % 50; // 0-49 unique seeds per second

            // max value 9642240000 -> ten digits
            //          0-5        1-12              1-31             0-23            0-59             0-59                0-49
            long mash = (y % 6) + ((mth - 1) * 6) + ((d - 1) * 72) + (h * 2232) + (m * 53568L) + (s * 3214080L) + (l * 192844800L);

            if (mash < 0)
            {
                throw new Exception("Generate Digits Failed: " + mash.ToString() + "\nYear: " + (y % 30).ToString() + "\nMonth: " + ((mth - 1) * 30).ToString() + "\nDay: " + ((d - 1) * 360).ToString() + "\nHour: " + (h * 11160).ToString() + "\nMinute: " + (m * 267840).ToString() + "\nSecond: " + (s * 16070400).ToString() + "\nMillisecond: " + (l * 964224000L).ToString());
            }

            string unq = mash.ToString();

            // add leading zeros
            for (int i = unq.Length; i < 10; i++)
            {
                unq = "0" + unq;
            }

            return unq;
        }
    }

}
}

