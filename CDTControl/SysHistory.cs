using System;
using System.Collections.Generic;
using System.Data;
using CDTDatabase;
using CDTControl;
using CDTLib;

namespace CDTControl
{
    public class SysHistory
    {
        private Database _dbStruct = Database.NewStructDatabase();

        public object InsertHistory(string sysMenuID, string action, string pkValue, string content)
        {
            object o;
            try
            {
                string sysUserID = Config.GetValue("sysUserID").ToString();
                string syspackageID = Config.GetValue("sysPackageID").ToString();
                string sql = "insert into sysHistory(hDateTime, sysUserID, sysPackageID, sysMenuID, Action, PkValue, OldContent, ComputerName) " +
                    " values(getdate()," + sysUserID + "," + syspackageID + "," + sysMenuID + ",N'" + action + "',N'" + pkValue + "',N'" + content + "','" + Config.GetValue("ComputerName") + "')";
                _dbStruct.BeginMultiTrans();
                _dbStruct.UpdateByNonQuery(sql);
                 o= _dbStruct.GetValue("select @@identity");
                _dbStruct.EndMultiTrans();
            }
            finally
            {
                if (_dbStruct.Connection.State != ConnectionState.Closed)
                    _dbStruct.Connection.Close();
            }
            return o;

        }
        public object InsertHistory(string sysMenuID, string action, string pkValue, string content, string sysTableID)
        {
            object o;
            try
            {
                string sysUserID = Config.GetValue("sysUserID").ToString();
                string syspackageID = Config.GetValue("sysPackageID").ToString();
                string sql = "insert into sysHistory(hDateTime, sysUserID, sysPackageID, sysMenuID, Action, PkValue, OldContent, sysTableID,ComputerName) " +
                    " values(getdate()," + sysUserID + "," + syspackageID + "," + sysMenuID + ",N'" + action + "',N'" + pkValue + "',N'" + content + "'," + sysTableID + ",'" + Config.GetValue("ComputerName") + "')";
                _dbStruct.BeginMultiTrans();
                _dbStruct.UpdateByNonQuery(sql);
                o = _dbStruct.GetValue("select @@identity");
                _dbStruct.EndMultiTrans();

            }
            finally
            {
                if (_dbStruct.Connection.State != ConnectionState.Closed)
                    _dbStruct.Connection.Close();
            }
            return o;
        }
        public void InsertHistoryDt(string sysHistoryID, string sysFieldID, string NewValue, string OldValue)
        {
            try
            {
                string sysUserID = Config.GetValue("sysUserID").ToString();
                string syspackageID = Config.GetValue("sysPackageID").ToString();
                string sql = "insert into sysHistoryDt(sysHistoryID, sysFieldID, Giatri, Giatricu) " +
                    " values(" + sysHistoryID + "," + sysFieldID + ",N'" + NewValue + "',N'" + OldValue + "')";
                _dbStruct.UpdateByNonQuery(sql);
            }
            finally
            {
                if (_dbStruct.Connection.State != ConnectionState.Closed)
                    _dbStruct.Connection.Close();
            }


        }
        //public CDTData GetDataForReport(string sysTableID, string pkValue)
        //{
        //    string sysPackageID = Config.GetValue("sysPackageID").ToString();
        //    DataReport data = new DataReport("83");
        //    data.PsString = "h.sysPackageID = " + sysPackageID + " and h.pkValue = '" + pkValue + "' and h.sysMenuID in (select sysMenuID from sysMenu where sysTableID = " + sysTableID + ")";
        //    data.DbData = _dbStruct;
        //    return data;
        //}
    }
}
