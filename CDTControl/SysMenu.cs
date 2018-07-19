using System;
using System.Collections.Generic;
using System.Data;
using CDTDatabase;
using CDTLib;
using Plugins;

namespace CDTControl
{
    public class SysMenu
    {
        Database _dbStruct = Database.NewStructDatabase();

        public DataTable GetMenu()
        {
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            string sysUserID = Config.GetValue("sysUserID").ToString();
            string sysDBID = Config.GetValue("sysDBID").ToString();
            DataTable dtUserPackage = _dbStruct.GetDataTable("select * from sysUserPackage  where sysUserGroupID in (select sysUserGroupID from sysuser where sysUserID = " + sysUserID + ") and sysDBID = " + sysDBID);
            if (dtUserPackage == null)
                return null;
            if (dtUserPackage.Rows.Count == 0)
                return (GetMenuForAdmin());
            string sysUserPackageID = dtUserPackage.Rows[0]["sysUserPackageID"].ToString();
            //Config.NewKeyValue("sysUserPackageID", sysUserPackageID);
            bool isAdmin = Boolean.Parse(dtUserPackage.Rows[0]["isAdmin"].ToString());
            if (isAdmin)
            {
                Config.NewKeyValue("Admin", true);
                return (GetMenuForAdmin());
            }
            else
            {
                Config.NewKeyValue("Admin", false);
                string sql = "select t.*,r.*,m.*,ut.* from sysMenu m left join sysTable t on m.sysTableID = t.sysTableID left join sysReport r on  m.sysReportID = r.sysReportID " +
                    " left join  sysUserMenu um on m.sysMenuID = um.sysMenuID left join sysUserTable ut on t.sysTableID=ut.sysTableID and um.sysUserPackageID=ut.sysUserPackageID" +
                    " where (sysMenuParent is null or sysmenuParent in (select sysmenuid from sysmenu where isVisible=1))  and " +
                    " (isVisible=1 and  um.Executable = 1 and um.sysUserPackageID = " + sysUserPackageID +// " and um.sysUserPackageID = " + sysUserPackageID +
                    ") order by m.MenuOrder";
                DataTable dtMenu = _dbStruct.GetDataTable(sql);
                return (dtMenu);
                //isVisible=1 and
            }
        }

        private DataTable GetMenuForAdmin()
        {
            Config.NewKeyValue("Admin", true);
                string sysPackageID = Config.GetValue("sysPackageID").ToString();
            DataTable dtMenu = _dbStruct.GetDataTable("select t.*,r.*,m.* from sysMenu m left join sysTable t on m.sysTableID = t.sysTableID left join sysReport r on  m.sysReportID = r.sysReportID " +
                    " where (sysMenuParent is null or sysmenuParent in (select sysmenuid from sysmenu where isVisible=1) ) and " +
                    "  isVisible=1 and   (m.sysPackageID is null or m.sysPackageID = " + sysPackageID +
                    ") order by m.sysPackageID, m.MenuOrder");

            return (dtMenu);
        }

        public void ModifyMenu(DataRow dr)
        {
            if (dr["CustomType"].ToString() != string.Empty)
                dr["Type"] = dr["CustomType"];
            if (dr["ExtraSql"].ToString() != string.Empty)
            {
                string extraSql = dr["ExtraSql"].ToString();
                string fieldName = string.Empty;
                if (extraSql.Contains("@@"))
                {
                    int i = extraSql.IndexOf("@@") + 2;
                    fieldName = extraSql.Substring(i, extraSql.Length - i);
                }
                object value = Config.GetValue(fieldName);
                if (value != null )
                    extraSql = extraSql.Replace("@@" + fieldName, "'" + value + "'");
                dr["ExtraSql"] = extraSql;
            }
        }
        public void SynMenuforUser()
        {
            _dbStruct.UpdateDatabyStore("SynMenu", new string[] { "@sysPackageid" }, new object[] { int.Parse(Config.GetValue("sysPackageID").ToString()) });
        }
        public void SynchronizeMenuWithPlugins(PluginManager pm)
        {
            string query = "select * from sysMenu where MenuPluginID is not null and PluginName is not null and sysPackageID = " + Config.GetValue("sysPackageID").ToString();
            DataTable dtPluginsMenu = _dbStruct.GetDataTable(query);
            pm.AddICustom();
            //xoa cac menu khong con ton tai trong plugin
            foreach (DataRow drMenu in dtPluginsMenu.Rows)
            {
                bool found = false;
                foreach (StructInfo si in pm.LstStructInfo)
                {
                    if (Int32.Parse(drMenu["MenuPluginID"].ToString()) == si.MenuId && drMenu["PluginName"].ToString() == si.DllName)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                  //  drMenu["isVisible"] = 0;
                 //   drMenu.Delete();
                }
            }
            //them cac menu moi trong plugin vao menu
            foreach (StructInfo si in pm.LstStructInfo)
            {
                bool found = false;
                foreach (DataRow drMenu in dtPluginsMenu.Rows)
                {
                    if (drMenu.RowState == DataRowState.Unchanged && Int32.Parse(drMenu["MenuPluginID"].ToString()) == si.MenuId && drMenu["PluginName"].ToString() == si.DllName)
                    {
                        found = true;
                       // drMenu["isVisible"] = 1;
                        break;
                    }
                }
                if (!found)
                {

                    DataRow drNew = dtPluginsMenu.NewRow();
                    drNew["MenuPluginID"] = si.MenuId;
                    // drNew["sysMenuParent"] = si.MenuIdParent;
                    drNew["isVisible"] = 1;
                    drNew["MenuName"] = si.MenuName;
                    drNew["PluginName"] = si.DllName;
                    drNew["sysPackageID"] = Config.GetValue("sysPackageID").ToString();
                    dtPluginsMenu.Rows.Add(drNew);
                }
            }
            _dbStruct.UpdateDataTable(query, dtPluginsMenu);
        }

        public DataTable GetRecentFunction()
        {
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            string sysUserID = Config.GetValue("sysUserID").ToString();
            DataTable dtMenu = _dbStruct.GetDataTable("select * from sysMenu where sysMenuID in"
                                + "(select sysMenuID from "
                                + "(select top 5 sysMenuID, max(hDateTime) as hDateTime "
                                + "from sysHistory "
                                + "where sysPackageID = " + sysPackageID + " and sysUserID = " + sysUserID
                                + " group by sysMenuID "
                                + "order by hDateTime desc) as tmp)");
            return (dtMenu);
        }
    }
}
