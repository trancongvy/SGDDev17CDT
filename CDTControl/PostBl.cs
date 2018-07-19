using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CDTDatabase;

namespace CDTControl
{
    public class PostBl
    {
        DateTime tuNgay;
        DateTime denNgay;
        private Database dataDb;
        private Database strucDb = Database.NewStructDatabase();
        private string _mtTableID;

        List<string> _LField=new List<string>();
        public PostBl(Database _dbData, string systableid, DateTime _tungay, DateTime _denngay)
        {
            tuNgay = _tungay;
            denNgay = _denngay;
            dataDb = _dbData;
            _mtTableID = systableid;
        }
        public PostBl(Database _dbData, string systableid, DateTime _tungay, DateTime _denngay,List<string> LField)
        {
            tuNgay = _tungay;
            denNgay = _denngay;
            dataDb = _dbData;
            _mtTableID = systableid;
            _LField = LField;
        }
     
        public void Post()
        {
            DataTable tbNhomdk = GetDtConfig();
            DataTable dtNhomdk;
            string sqlUpdate;
            foreach (DataRow drNhomdk in tbNhomdk.Rows)
            {
                dtNhomdk=GetDtConfigDetail(drNhomdk["blConfigID"].ToString());

                {
                    if (_LField.Count == 0)
                        sqlUpdate = GenUpdateString(dtNhomdk, drNhomdk);
                    else
                        sqlUpdate = GenUpdateString(dtNhomdk, drNhomdk, _LField);
                    if (sqlUpdate != string.Empty)
                        dataDb.UpdateByNonQuery(sqlUpdate);
                }                           
            }
        }

        private string GenUpdateString(DataTable dt,DataRow drNhomdk)
        {
            //mtdt: 0: update bang mt, 1: update bang dt, 2: update bang cong thuc(mt, dt)
            string s1 = drNhomdk["mtTableName"].ToString().Trim();
            string s2 = drNhomdk["dtTableName"].ToString().Trim();
            string bl = drNhomdk["blTableName"].ToString().Trim();
            string sql = " update " + bl + " set ";
            string giatri = "";

            foreach (DataRow dr in dt.Rows)
            {
               
                giatri = "";
                if (!(dr["mtFieldID"] is DBNull))
                {
                    giatri = s1 + "." + dr["mtFieldName"].ToString();
                }
                else if (!(dr["dtFieldID"] is DBNull))
                {
                    giatri = s2 + "." + dr["dtFieldname"].ToString();
                }
                else if (!(dr["Formula"] is DBNull) && dr["Formula"].ToString() != "")
                {
                    giatri =  dr["Formula"].ToString();
                }
                if (giatri != "")
                    sql += dr["blFieldName"].ToString() + " = " + giatri + " ,";
            }
            sql = sql.Substring(0, sql.Length - 1);

            if (s2 == "")
            {
                sql += " from " + bl + "," + s1 +  " where ";
                sql += bl + "." + drNhomdk["RootIDName"].ToString().Trim() + " = ";
                sql += s1 + "." + s1 + "ID ";
            }
            else
            {
                sql += " from " + bl + "," + s1 + "," + s2 + " where ";
                sql += bl + "." + drNhomdk["RootIDName"].ToString().Trim() + " = ";
                sql += s1 + "." + s1 + "ID and ";
                sql += bl + "." + drNhomdk["DTID"].ToString().Trim() + " = ";
                sql += s2 + "." + s2 + "ID ";
            }
            
            if (!(drNhomdk["nhomdk"] is DBNull))
            {
                sql += " and " + bl + ".nhomdk" + " = '" + drNhomdk["Nhomdk"].ToString() + "'";
            }
            if (!(drNhomdk["Condition"] is DBNull))
            {
                sql += " and " + drNhomdk["Condition"].ToString();
            }
            sql += " and (" + s1.Trim() + ".ngayct between cast('" + tuNgay.ToShortDateString() + "' as datetime) and cast('" + denNgay.ToShortDateString() + "' as datetime))";
            return sql;
            
        }
        private  string GenUpdateString(DataTable dt, DataRow drNhomdk,List<string> LField)
        {
            //mtdt: 0: update bang mt, 1: update bang dt, 2: update bang cong thuc(mt, dt)
            string s1 = drNhomdk["mtTableName"].ToString().Trim();
            string s2 = drNhomdk["dtTableName"].ToString().Trim();
            string bl = drNhomdk["blTableName"].ToString().Trim();

            string reTableName = drNhomdk["reTableName"].ToString();

            string dtPk = drNhomdk["dtPk"].ToString();
            string reKey = drNhomdk["reKey"].ToString();

            string mtPk = s1 + "ID";
            string dtRefPK = mtPk;
            string sql;
            sql = " select Pk from systable where tableName='" + s1 + "'";
            DataTable Ttmp =strucDb.GetDataTable(sql);
            if (Ttmp.Rows.Count > 0) mtPk = Ttmp.Rows[0]["Pk"].ToString();            

            if (s2 != null && s2 != string.Empty)
            {
                sql = "select fieldName from sysfield where  reftable='" + s1 + "' and sysTableID= (select systableID from systable where tableName='" + s2 + "')";
                Ttmp = strucDb.GetDataTable(sql);
                if (Ttmp.Rows.Count > 0) dtRefPK = Ttmp.Rows[0]["fieldName"].ToString();
            }
             sql = " update " + bl + " set ";
            string sqltmp = sql;
            string giatri =string.Empty ;
            bool hasChange = false;
            foreach (DataRow dr in dt.Rows)
            {
                giatri = string.Empty;
                if (!(LField.Contains(dr["mtFieldName"].ToString().Trim().ToUpper()) || LField.Contains(dr["dtFieldName"].ToString().Trim().ToUpper())))
                    continue;
                
                if (!(dr["mtFieldID"] is DBNull))
                {
                    giatri = s1 + "." + dr["mtFieldName"].ToString();
                }
                else if (!(dr["dtFieldID"] is DBNull))
                {
                    giatri = s2 + "." + dr["dtFieldname"].ToString();
                }
                else if (!(dr["Formula"] is DBNull) && dr["Formula"].ToString() != "")
                {
                    giatri = dr["Formula"].ToString();
                }
                if (giatri != "")
                {
                    hasChange = true;
                    sql += dr["blFieldName"].ToString() + " = " + giatri + " ,";
                }
            }
            if (!hasChange) 
                return string.Empty;
            sql = sql.Substring(0, sql.Length - 1);

            if (s2 == "")
            {
                if (reKey == "")
                {
                    sql += " from " + bl + "," + s1 + " where ";
                    sql += bl + "." + drNhomdk["RootIDName"].ToString().Trim() + " = ";
                    sql += s1 + "." + mtPk;
                }
                else
                {
                    sql += " from " + bl + "," + s1 + "," + reTableName + " where ";
                    sql += bl + "." + drNhomdk["RootIDName"].ToString().Trim() + " = ";
                    sql += s1 + "." + mtPk + " and " + s1 + "." + reKey + " = " + reTableName + "." + reKey;
                }

            }
            else

            {
                if (reKey == "")
                {
                    sql += " from " + bl + "," + s1 + "," + s2 + " where ";
                    sql += bl + "." + drNhomdk["RootIDName"].ToString().Trim() + " = ";
                    sql += s1 + "." + mtPk + " and ";
                    sql += bl + "." + drNhomdk["DTID"].ToString().Trim() + " = ";
                    sql += s2 + "." + dtPk ;
                }
                else
                {
                    sql += " from " + bl + "," + s1 + "," + s2 + "," + reTableName + " where ";
                    sql += bl + "." + drNhomdk["RootIDName"].ToString().Trim() + " = ";
                    sql += s1 + "." + mtPk + " and ";
                    sql += bl + "." + drNhomdk["DTID"].ToString().Trim() + " = ";
                    sql += s2 + "." + dtPk + "  and " + s2 + "." + reKey + " = " + reTableName + "." + reKey;
                }
            }

            if (!(drNhomdk["nhomdk"] is DBNull))
            {
                sql += " and " + bl + ".nhomdk" + " = '" + drNhomdk["Nhomdk"].ToString() + "'";
            }
            if (!(drNhomdk["Condition"] is DBNull))
            {
                sql += " and " + drNhomdk["Condition"].ToString();
            }
            sql += " and (" + s1.Trim() + ".ngayct between cast('" + tuNgay.ToShortDateString() + "' as datetime) and cast('" + denNgay.ToShortDateString() + "' as datetime))";

            return sql;

        }


        private DataTable GetDtConfig()
        {
            string s = "select bl.*, tb1.TableName as blTableName, tb2.TableName as mtTableName,  tb2.Pk as Pk, " +
                " tb3.TableName as dtTableName,tb4.TableName as reTableName,tb3.Pk as dtPk ,tb4.pk as rePk " +
                " from sysDataConfig bl left join  sysTable tb1 on bl.sysTableID = tb1.sysTableID left join  sysTable tb2 on bl.mtTableID = tb2.sysTableID " +
                " left join  sysTable tb3 on  bl.dtTableID = tb3.sysTableID left join  sysTable as tb4 on bl.reTable = tb4.systableid" +
                " where bl.mtTableID = " + _mtTableID ;
            Database db = Database.NewStructDatabase();
            return (db.GetDataTable(s));
        }
        private DataTable GetDtConfigDetail(string blConfigID)
        {
            string s = "select bld.*, sf1.FieldName as blFieldName, sf2.FieldName as mtFieldName, sf3.FieldName as dtFieldName " +
                " from sysDataconfig blm inner join sysDataConfigDt bld on blm.blConfigid=bld.blconfigid left join   sysField sf1 on bld.blFieldID = sf1.sysFieldID  left join   sysField sf2 on bld.mtFieldID = sf2.sysFieldID "+
                " left join   sysField sf3 on bld.dtfieldid = sf3.sysFieldID" + 
                " where bld.blConfigID = " + blConfigID ;
            Database db = Database.NewStructDatabase();
            return (db.GetDataTable(s));
        }
        


    }
}
