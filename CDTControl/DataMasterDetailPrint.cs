using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CDTDatabase;
using ErrorManager;
using CDTLib;
namespace CDTControl
{
    public class DataMasterDetailPrint
    {
        private string _mtTableID = string.Empty;
        private string _dtTableID = string.Empty;
        private string _mtID = string.Empty;

        Database db = Database.NewStructDatabase();
        string _dtTableName = string.Empty;
        string _mtTableName = string.Empty;
        string _mtKey = string.Empty;
        string _dtRefKey = string.Empty;
        public string GetSQL = string.Empty;
        struct TableListRelation
        {
            public string _tableName;
            public string _fkTableName;
            public string _listName;
            public string _listAlias;
            public string _fkListName;
            public TableListRelation(string tableName, string fkTableName, string listName, string listAlias, string fkListName)
            {
                _tableName = tableName;
                _fkTableName = fkTableName;
                _listName = listName;
                _listAlias = listAlias;
                _fkListName = fkListName;
            }
        }
        List<TableListRelation> lstTableListRelation = new List<TableListRelation>();

        public DataMasterDetailPrint(string mtTableID, string dtTableID)
        {
            _mtTableID = mtTableID;
            _dtTableID = dtTableID;
        }

        public DataTable GetData(string mtID)
        {
            lstTableListRelation.Clear();
            _mtID = mtID;
            string sql = GenSql();
            LogFile.LogTruyXuatDL(sql);
            GetSQL = sql;
            Database dbData = Database.NewDataDatabase();
            return (dbData.GetDataTable(sql));
        }
        public DataTable GetData(string mtID, string _Script)
        {
            lstTableListRelation.Clear();
            _mtID = mtID;
            string sql = _Script;
            
            Database dbData = Database.NewDataDatabase();
            return (dbData.GetDataTable(sql));
        }
        private void UpdateRelationList(string tableID, string tableName, ref int stt)
        {
            
            string sql = "select * from sysField where sysTableID = " + tableID;
            DataTable dtField = db.GetDataTable(sql);
            if (dtField == null)
                return;
            foreach (DataRow dr in dtField.Rows)
            {
                string refTable ;//= dr["RefTable"].ToString();
                //if (dr["RootTable"] != DBNull.Value)
                //    refTable = dr["RootTable"].ToString();
                //else
                    refTable = dr["RefTable"].ToString();
                if (refTable == string.Empty || refTable.ToUpper() == _mtTableName.ToUpper())
                    continue;
                stt++;
                string alias = refTable + stt.ToString();
                string fieldName = dr["FieldName"].ToString();
                string refField = dr["refField"].ToString();

                if (refTable.Substring(0, 1) != "w")
                    lstTableListRelation.Add(new TableListRelation(tableName, fieldName, refTable, alias, refField));
                else
                {
                    refTable = dr["RootTable"].ToString();
                    lstTableListRelation.Add(new TableListRelation(tableName, fieldName, refTable, alias, refField));
                }
            }
        }

        private string GetMasterDetailTableInfor()
        {
            string s = "select ";
            object oDtTableName = db.GetValue("select TableName from sysTable where sysTableID = " + _dtTableID);
            if (_dtTableName == null)
                return s;
            _dtTableName = oDtTableName.ToString();
            DataTable dtMtTable = db.GetDataTable("select * from sysTable where sysTableID = " + _mtTableID);
            if (dtMtTable == null || dtMtTable.Rows.Count == 0)
                return s;
            _mtTableName = dtMtTable.Rows[0]["TableName"].ToString();
            _mtKey = dtMtTable.Rows[0]["Pk"].ToString();
            _dtRefKey = _mtKey;
            DataTable mtFields = db.GetDataTable("select * from sysField where  sysTableID = " + _mtTableID);//Visible = 1 and
            if (mtFields != null)
                foreach (DataRow drField in mtFields.Rows)
                    s += _mtTableName + "." + drField["FieldName"].ToString() + " as m_" + drField["FieldName"].ToString() + ", ";
            DataTable dtFields = db.GetDataTable("select * from sysField where sysTableID = " + _dtTableID);// Visible = 1 and 
            DataRow[] ldrdt = dtFields.Select("RefField='" + _mtKey + "'");
            if (ldrdt.Length > 0) _dtRefKey = ldrdt[0]["FieldName"].ToString();
            if (dtFields != null)
                foreach (DataRow drField in dtFields.Rows)
                    s += _dtTableName + "." + drField["FieldName"].ToString() + " as d_" + drField["FieldName"].ToString() + ", ";
            s += _dtTableName + ".stt as d_stt,";
            return s;
        }


        public string GenSql()
        {
            string sSelect = GetMasterDetailTableInfor();
            int stt = 0;
            UpdateRelationList(_mtTableID, _mtTableName, ref stt);
            UpdateRelationList(_dtTableID, _dtTableName, ref stt);

            string sFrom = " from " + _mtTableName + " inner join " + _dtTableName + " on  " + _mtTableName + "." + _mtKey + " = " + _dtTableName + "." + _mtKey;
            string sWhere = " where ";

            foreach (TableListRelation tlr in lstTableListRelation)
            {
                sSelect += tlr._listAlias + ".*, ";
                sFrom += " left join " + tlr._listName + " " + tlr._listAlias + " on  " + tlr._tableName + "." + tlr._fkTableName + " = " + tlr._listAlias + "." + tlr._fkListName;
                //sWhere += " and " + tlr._tableName + "." + tlr._fkTableName + " = " + tlr._listAlias + "." + tlr._fkListName;
            }
            sSelect = sSelect.Substring(0, sSelect.Length - 2);
            //sFrom = sFrom.Substring(0, sFrom.Length - 2);
            sWhere +=  _mtTableName + "." + _mtKey + " = '" + _mtID + "'";
            string sql = sSelect + " " + sFrom + " " + sWhere;
            return sql;
            
        }

    }
}
