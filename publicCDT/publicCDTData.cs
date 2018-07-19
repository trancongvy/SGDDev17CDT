using System;
using System.Collections.Generic;
using System.Text;
using DataFactory;
namespace publicCDT
{
    public class publicCDTData
    {
        static List<CDTData> lCDTData=new List<CDTData>();
        public static CDTData findCDTData(string tableName, string condition, string DynCondition)
        {
            foreach (CDTData c in lCDTData)
            {
                if (c._tableName == tableName && condition == c.Condition && c.DynCondition == DynCondition)
                    return c;
            }
            return null;
        }
        public static void AddCDTData(CDTData c)
        {
            if (!lCDTData.Exists(data => data._tableName == c._tableName && data.Condition == c.Condition && c.DynCondition == data.DynCondition))
            {
                lCDTData.Add(c);
            }
        }

    }
}
