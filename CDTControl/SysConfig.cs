using System;
using System.Collections.Generic;
using System.Data;
using CDTDatabase;
using CDTLib;

namespace CDTControl
{
    public class SysConfig
    {
        Database _dbStruct = Database.NewStructDatabase();
        DataSet _dsStartConfig;

        public DataSet DsStartConfig
        {
            get { return _dsStartConfig; }
            set { _dsStartConfig = value; }
        }

        public void GetUserConfig()
        {
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            string sysDBID = Config.GetValue("sysDBID").ToString();
            string sql = "select a.sysUserID, b.UserName from systable a inner join sysUser b on a.sysUserID=b.sysUserID where a.TableName='sysConfig' and a.sysUserID is not null";
            DataTable tb = _dbStruct.GetDataTable(sql);
            if (tb.Rows.Count == 0)
            {
                _dsStartConfig = _dbStruct.GetDataSet("select sysConfigID, _Key, _Value from sysConfig where IsUser = 1 and sysPackageID = " + sysPackageID + " and (sysDBID is null or sysDBID=" + sysDBID + ")");
            }
            else
            {
                string AdminID = tb.Rows[0]["UserName"].ToString();
                string UserName = Config.GetValue("UserName").ToString();
                if (AdminID == UserName)
                    _dsStartConfig = _dbStruct.GetDataSet("select sysConfigID, _Key, _Value from sysConfig where IsUser = 1 and sysPackageID = " + sysPackageID + " and (sysDBID is null or sysDBID=" + sysDBID + ")");
                else
                    _dsStartConfig = _dbStruct.GetDataSet("select sysConfigID, _Key, _Value from sysConfig  where   IsUser = 1 and sysPackageID = " + sysPackageID + " and (sysDBID is null or sysDBID=" + sysDBID + ") and sysUserID in (select sysUserID from sysUser where UserName='" + Config.GetValue("UserName").ToString() + "')");
            }
        }

        public void GetStartConfig(string sysPackageID, string sysDBID)
        {
            _dsStartConfig = _dbStruct.GetDataSet("select sysConfigID, _Key, _Value from sysConfig where StartConfig = 1 and sysPackageID = " + sysPackageID + " and (sysDBID is null or sysDBID=" + sysDBID + ")"  );
        }

        private void UpdateCurrentConfig()
        {
            if (_dsStartConfig == null) return;
            foreach (DataRow dr in _dsStartConfig.Tables[0].Rows)
            {
                if (dr.RowState == DataRowState.Modified)
                {
                    Config.Variables.Remove(dr["_Key"].ToString());
                    Config.NewKeyValue(dr["_Key"], dr["_Value"]);
                }
            }
        }

        public bool UpdateStartConfig()
        {
            UpdateCurrentConfig();
            if (_dsStartConfig == null) return true;
            return (_dbStruct.UpdateDataSet(_dsStartConfig));
        }
    }
}
