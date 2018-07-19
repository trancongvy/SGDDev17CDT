using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using CDTDatabase;

namespace CDTControl
{
    public class Translation
    {
        public static Hashtable GetDictionary(string LocalizerType)
        {
            Database dbMain = Database.NewStructDatabase();
            Hashtable h = new Hashtable();
            string query = "Select * from DevLocalizer where LocalizerTypeID = '" + LocalizerType + "'";
            DataTable x = dbMain.GetDataTable(query);
            if (x == null)
                return null;
            for (int i = 0; i < x.Rows.Count; i++)
            {
                if(!h.Contains(x.Rows[i]["StringID"]))
                h.Add(x.Rows[i]["StringID"], x.Rows[i]["Content"]);
            }
            return h;
        }
        public static void updateDictionary(string LocalizerType, string id, string Content)
        {
            Database dbMain = Database.NewStructDatabase();
            string query = "insert into DevLocalizer( LocalizerTypeID,StringID,Content) values('" + LocalizerType + "'," + id + ",'" + Content + "')";
            dbMain.UpdateByNonQuery(query);
        }
    }
}
