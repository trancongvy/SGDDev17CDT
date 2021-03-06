using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using System.Data;
using System.Data.SqlClient;
namespace sysCollect
{
    class CollectData
    {
        private DateTime _Tungay;
        private DateTime _Denngay;
        private int PackageId = 11;
        private string GlPackageName;
        DataTable SysColected;
        private Database _Data=Database.NewCustomDatabase("server =Kythuat3; database = CBA15; User = sa; Password = sa");
        private Database _StructData = Database.NewCustomDatabase("server =Kythuat3; database = CDT; User = sa; Password = sa");
        public CollectData(DateTime tuNgay, DateTime denNgay)
        {
            _Tungay = tuNgay;
            _Denngay = denNgay;           

        }
        public bool Collect()
        {

            string sql = " select * from syscollected where GLPackage=11";
            SysColected = _StructData.GetDataTable(sql);
            sql = " select * from sysTable where sysPackageid=" + PackageId.ToString() + " and collectType is not null";
            DataTable SysTable = _StructData.GetDataTable(sql);
            //DataRow[] exitsNgayCt = SysTable.Select("TableName='dmkh'");
            try
            {
                GlPackageName = _StructData.GetValue("select DBName from sysPackage where syspackageid=" + PackageId.ToString()).ToString();
                _Data.BeginMultiTrans();
                //Xóa dữ liệu cũ
                // Deleted.Clear();
                foreach (DataRow drT in SysTable.Rows)
                {
                    ExecuteDelete(drT);
                }
                foreach (DataRow drT in SysTable.Rows)
                {
                    ExecuteCollect(drT);
                }
                _Data.EndMultiTrans();
            }
            catch
            {
                //_Data.RollbackMultiTrans();
            }
            return true;
        }
        List<int> Deleted=new List<int>();
        List<int> Colected = new List<int>();
        private bool ExecuteDelete(DataRow drT)
        {
            if (Deleted.Contains(int.Parse(drT["systableid"].ToString()))) return true;
            int collectType = int.Parse(drT["CollectType"].ToString());
            if (collectType != 1 && collectType != 2 && collectType != 3) return true;

            DataTable Reftable = GetReftable(drT["TableName"].ToString());

            if (Reftable.Rows.Count > 0)
            {
                foreach (DataRow dr in Reftable.Rows)
                {
                    ExecuteDelete(dr);
                }
            }
            try
            {
                string sql = CreateDeleteSql(drT);
                _Data.UpdateByNonQuery(sql);
            }
            finally
            {
                Deleted.Add(int.Parse(drT["systableid"].ToString()));
            }
            return true;
        }
        private DataTable GetReftable(string TableName)
        {
            string sql = "";
            sql = " select * from systable where sysPackageid=" + PackageId.ToString() + " and sysTableID in ";
            sql += "(select SysTableID from sysField where refTable='" + TableName + "')";
            return _StructData.GetDataTable(sql);
        }
        private string CreateDeleteSql(DataRow drT)
        {
            string TableName = drT["TableName"].ToString().Trim();            
            string sql="";
            DataTable listField = GetField(int.Parse(drT["sysTableId"].ToString()));
            DataRow[] exitsNgayCt = listField.Select("FieldName='ngayct'");
            int collectType = int.Parse(drT["CollectType"].ToString());
            if (collectType == 1 || collectType == 2)
            {
                if (exitsNgayCt.Length > 0)
                {
                    sql = "delete " + TableName + " where ngayct between '" + _Tungay.ToShortDateString() + "' and '" + _Denngay.ToShortDateString() + "'";
                }
                else
                {
                    string MarterTable = drT["MasterTable"].ToString().Trim();
                    sql = "select pk from systable where TableName='" + MarterTable + "' and sysPackageid=" + PackageId;
                    string MarterPk = _StructData.GetValue(sql).ToString();
                    sql = "delete " + TableName + " where " + MarterPk + " in (select " + MarterPk + " from " + MarterTable + " where ngayct between '" + _Tungay.ToShortDateString() + "' and '" + _Denngay.ToShortDateString() + "')";
                }
            }
            else if (collectType==3)
            {
                sql = "delete " + TableName;
            }
            return sql;
        }
        private bool ExecuteCollect(DataRow drT)
        {
            if (Colected.Contains(int.Parse(drT["systableid"].ToString()))) return true;
            Colected.Add(int.Parse(drT["systableid"].ToString()));
            DataTable Reftable = GetKeytable(int.Parse(drT["sysTableId"].ToString()));
            if (int.Parse(drT["CollectType"].ToString()) > 3) return true;
            if (Reftable.Rows.Count > 0)           
            {
                
                foreach (DataRow dr in Reftable.Rows)
                {
                    ExecuteCollect(dr);
                }
            }
            foreach (DataRow drSub in SysColected.Rows)
            {
                string SubPackageName = _StructData.GetValue("select DBName from sysPackage where syspackageid=" + drSub["SubPackage"].ToString()).ToString();
                string sql = CreateSql(SubPackageName, drT);
                if (sql !=string.Empty)
                _Data.UpdateByNonQuery(sql);
                
            }
            return true;
        }
        private DataTable GetKeytable(int sysTableID)
        {
            string sql = "";
            sql = " select * from systable where sysPackageid=" + PackageId.ToString() + " and TableName in ";
            sql += "(select RefTable from sysField where systableId=" + sysTableID + " and RefTable is not null)";
            return _StructData.GetDataTable(sql);
        }
        private string CreateSql(string SubP,DataRow drT)
        {
            //trừ kiểu số nguyên tự tăng
            string TableName = drT["TableName"].ToString().Trim();
            int collectType = int.Parse(drT["CollectType"].ToString());
            int TableID = int.Parse(drT["sysTableId"].ToString());
            string pk = drT["Pk"].ToString();
            string ListField = GetFieldString(TableID);
            string sql = "";
            if (collectType == 1 || collectType == 2)
            {

                DataTable listField = GetField(int.Parse(drT["sysTableId"].ToString()));
                DataRow[] exitsNgayCt = listField.Select("FieldName='NgayCt'");
                if (exitsNgayCt.Length > 0)
                {
                    sql = " insert into " + TableName + "(" + ListField + ") select " + ListField + " from " + SubP.Trim() + ".dbo." + TableName + " where ngayCt between '" + _Tungay.ToShortDateString() + "' and '" + _Denngay.ToShortDateString() + "'";
                }
                else
                {
                    string MarterTable = drT["MasterTable"].ToString().Trim();
                    sql = "select pk from systable where TableName='" + MarterTable + "' and sysPackageid=" + PackageId;
                    string MarterPk = _StructData.GetValue(sql).ToString();
                    sql = " insert into " + TableName + "(" + ListField + ") select " + ListField + " from " + SubP.Trim() + ".dbo." + TableName;
                    sql += " where " + MarterPk + " in (select " + MarterPk + " from " + SubP.Trim() + ".dbo." + MarterTable + " where  ngayCt between '" + _Tungay.ToShortDateString() + "' and '" + _Denngay.ToShortDateString() + "')";
                }
            }
            else if (collectType == 0)
            {
                sql = " insert into " + TableName + "(" + ListField + ") select " + ListField + " from " + SubP.Trim() + ".dbo." + TableName + " where " + pk + " not in (select " + pk + " from " + TableName + ")";

            }
            else if (collectType == 3)
            {
                sql = " insert into " + TableName + "(" + ListField + ") select " + ListField + " from " + SubP.Trim() + ".dbo." + TableName;
            }
            return sql;
        }
        private string GetFieldString(int SystableID)
        {
            string sql="";
            DataTable listField = GetField(SystableID);
            foreach (DataRow dr in listField.Rows)
            {
                sql += dr["FieldName"].ToString().Trim() + ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            return sql;
        }
        private DataTable GetField(int SystableID)
        {
            string sql = "select * from sysField where systableid=" + SystableID + " and type <>3";
            return _StructData.GetDataTable(sql);
        }

    }
}
