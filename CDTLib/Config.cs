using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace CDTLib
{
    public class Config
    {
        static Hashtable _variables = new Hashtable();

        public static Hashtable Variables
        {
            get { return Config._variables; }
            set { Config._variables = value; }
        }

        public static void InitData(DataTable dtData)
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if(dtData.Rows[i] != null)
                    try
                    {
                        _variables.Add(dtData.Rows[i]["_Key"], dtData.Rows[i]["_Value"]);
                    }
                    catch (Exception ex)
                    {
                    }

                   
            }
        }

        public static void NewKeyValue(object key, object value)
        {
            if (_variables.Contains(key))
                _variables.Remove(key);
            _variables.Add(key, value);
        }

        public static object GetValue(string key)
        {
            return (_variables[key]);
        }
    }
}