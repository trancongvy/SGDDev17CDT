using System;
using System.Collections.Generic;
using System.Data;
using CDTLib;
using CDTDatabase;

namespace CDTControl
{
    public class SysTable
    {
        private Database _dbStruct = Database.NewStructDatabase();
        private DataTable _dtTable;

        public DataTable DtTable
        {
            get { return _dtTable; }
            set { _dtTable = value; }
        }

        public void GetAllForPackage()
        {
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            string queryString = "select * from sysTable where sysPackageID = " + sysPackageID + " order by DienGiai";
            _dtTable = _dbStruct.GetDataTable(queryString);
        }

        public void GetUserTableForPackage()
        {
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            string queryString = "select * from sysTable where Type <> 3 and Type <> 6 and sysPackageID = " + sysPackageID + " order by DienGiai";
            _dtTable = _dbStruct.GetDataTable(queryString);
        }
        public void UpdateColWidth(DataRow dr, int w)
        {
            string sql = "update sysField set ColWidth=" + w.ToString() + " where sysfieldID=" + dr["sysFieldID"].ToString();
            _dbStruct.UpdateByNonQuery(sql);
        }
        public void UpdateColIndex(DataRow dr, int inx)
        {
            string sql = "update sysField set  TabIndex=" + inx.ToString() + " where sysfieldID=" + dr["sysFieldID"].ToString();
            _dbStruct.UpdateByNonQuery(sql);
        }
        public void UpdateColVisible(DataRow dr, int v)
        {
            string sql = "update sysField set Visible=" + v.ToString() + " where sysfieldID=" + dr["sysFieldID"].ToString();
            _dbStruct.UpdateByNonQuery(sql);
        }
    }
}
