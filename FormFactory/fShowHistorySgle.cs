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
    public partial class fShowHistorySgle : DevExpress.XtraEditors.XtraForm
    {
        DataSingle _Data;
        Database _dbData = Database.NewStructDatabase();
        DataSet dbSet = new DataSet();
        public fShowHistorySgle(DataSingle _data)
        {
            _Data = _data;
            InitializeComponent();
            Getdata();
        }
        private void Getdata()
        {
            string sql = "select a.*, b.UserName from sysHistory a inner join sysuser b on a.sysUserID=b.sysUserID where a.sysTableID=" + _Data.DrTable["sysTableID"].ToString() + " and pkValue='" + _Data.DrCurrentMaster[_Data.PkMaster.FieldName] + "'";
            DataTable tb = _dbData.GetDataTable(sql);
            if (tb != null) dbSet.Tables.Add(tb);
            else return;
            tb.PrimaryKey = new DataColumn[] { tb.Columns["sysHistoryID"] };
            sql = "select a.*, c.FieldName,c.LabelName from sysHistoryDt a inner join sysHistory b on a.sysHistoryID =b.sysHistoryID inner join sysField c on a.sysFieldID=c.sysFieldID where b.sysTableID=" + _Data.DrTable["sysTableID"].ToString() + " and pkValue='" + _Data.DrCurrentMaster[_Data.PkMaster.FieldName] + "'";
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
        }
    }
}