using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using DataFactory;
namespace FormFactory
{
    public partial class fShowHistoryMtdt : DevExpress.XtraEditors.XtraForm
    {
        DataMasterDetail _Data;
        Database _dbData = Database.NewStructDatabase();
        DataSet dbSet = new DataSet();
        public fShowHistoryMtdt(DataMasterDetail _data)
        {
            _Data = _data;
            InitializeComponent();
            Getdata();
        }
        private void Getdata()
        {
            string sql = "select a.*, b.UserName from sysHistory a inner join sysuser b on a.sysUserID=b.sysUserID where a.sysTableID=" + _Data.DrTableMaster["sysTableID"].ToString() + " and pkValue='" + _Data.DrCurrentMaster[_Data.PkMaster.FieldName] + "'";
            DataTable tb = _dbData.GetDataTable(sql);
            if (tb != null) dbSet.Tables.Add(tb);
            else return;
            tb.PrimaryKey = new DataColumn[] { tb.Columns["sysHistoryID"] };
            sql = "select a.*, c.FieldName,c.LabelName from sysHistoryDt a inner join sysHistory b on a.sysHistoryID =b.sysHistoryID inner join sysField c on a.sysFieldID=c.sysFieldID where b.sysTableID=" + _Data.DrTableMaster["sysTableID"].ToString() + " and pkValue='" + _Data.DrCurrentMaster[_Data.PkMaster.FieldName] + "'";
            DataTable tb1 = _dbData.GetDataTable(sql);
            if (tb1 != null) dbSet.Tables.Add(tb1);
            tb.TableName = "Master";
            tb1.TableName = "Detail";
            tb1.PrimaryKey = new DataColumn[] { tb1.Columns["sysHistoryDtID"] };
            DataRelation re = new DataRelation("Re", tb.Columns["sysHistoryID"], tb1.Columns["sysHistoryID"]);
            dbSet.Relations.Add(re);
            BindingSource bs = new BindingSource();
            bs.DataSource = dbSet;
            bs.DataMember = tb.TableName;
            gridControl1.DataSource = bs;
            gridControl2.DataSource = bs;
            gridControl2.DataMember = "Re";
            string idlist = "(";
            foreach (DataRow drDt in _Data.LstDrCurrentDetails)
            {
                string pkDt = _Data.DrTable["pk"].ToString();
                idlist += "'" + drDt[pkDt].ToString() + "',";

            }
            idlist =idlist.Substring(0,idlist.Length-1) + ")";
            sql = "select a.*, b.UserName from sysHistory a inner join sysuser b on a.sysUserID=b.sysUserID where a.sysTableID=" + _Data.DrTable["sysTableID"].ToString() + " and pkValue in " + idlist;
            DataTable tbDt = _dbData.GetDataTable(sql);
            DataSet dbSetDt=new DataSet();
            if (tbDt != null) dbSetDt.Tables.Add(tbDt);
            else return;
            tbDt.PrimaryKey = new DataColumn[] { tbDt.Columns["sysHistoryID"] };
            sql = "select a.*, c.FieldName,c.LabelName from sysHistoryDt a inner join sysHistory b on a.sysHistoryID =b.sysHistoryID inner join sysField c on a.sysFieldID=c.sysFieldID where ";
            sql += " a.sysHistoryID in (select sysHistoryID from sysHistory where  sysTableID=" + _Data.DrTable["sysTableID"].ToString() + " and pkValue in " + idlist + ")";
            DataTable tbDt1 = _dbData.GetDataTable(sql);
            if (tbDt1 != null) dbSetDt.Tables.Add(tbDt1);
            tbDt.TableName = "Master";
            tbDt1.TableName = "Detail";
            tbDt1.PrimaryKey = new DataColumn[] { tbDt1.Columns["sysHistoryDtID"] };
            DataRelation reDt = new DataRelation("ReDt", tbDt.Columns["sysHistoryID"], tbDt1.Columns["sysHistoryID"]);
            dbSetDt.Relations.Add(reDt);
            BindingSource bsDt = new BindingSource();
            bsDt.DataSource = dbSetDt;
            bsDt.DataMember = tbDt.TableName;
            gridControl3.DataSource = bsDt;
            gridControl4.DataSource = bsDt;
            gridControl4.DataMember = "ReDt";

        }
    }
}