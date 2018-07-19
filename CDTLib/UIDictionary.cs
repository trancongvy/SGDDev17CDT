using System;
using System.Data;
using System.Collections;

namespace CDTLib
{
    public class UIDictionary
    {
        static Hashtable _contents = new Hashtable();

        public static Hashtable Contents
        {
            get { return UIDictionary._contents; }
            set { UIDictionary._contents = value; }
        }

        public static void InitData(DataTable dtData)
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (dtData.Rows[i] != null)
                    _contents.Add(dtData.Rows[i]["Content"].ToString().ToUpper(), dtData.Rows[i]["Content2"]);
            }
        }

        public static string Translate(string key)
        {
            string keyUpper = key.ToUpper();
            if (_contents.Contains(keyUpper))
                return _contents[keyUpper].ToString();
            else
                return key;
        }
    }
}
