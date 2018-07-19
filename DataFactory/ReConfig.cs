using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
namespace DataFactory
{

    public class ReConfig
    {
        private Hashtable _variables = new Hashtable();

        public Hashtable Variables
        {
            get { return _variables; }
            set { _variables = value; }
        }
        public Hashtable Copy()
        {
            Hashtable h = new Hashtable();
            foreach (string i in _variables.Keys)
            {
                h.Add(i, _variables[i]);
            }
            return h;
        }
        public void InitData(DataTable dtData)
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (dtData.Rows[i] != null)
                    try
                    {
                        _variables.Add(dtData.Rows[i]["_Key"], dtData.Rows[i]["_Value"]);
                    }
                    catch (Exception ex)
                    {
                    }
            }
        }

        public void NewKeyValue(object key, object value)
        {
            if (_variables.Contains(key))
                _variables.Remove(key);
            _variables.Add(key, value);
        }

        public object GetValue(string key)
        {
            return (_variables[key]);
        }
    }
}