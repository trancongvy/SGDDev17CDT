using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using CDTDatabase;
namespace CusNissan
{
    public partial class fCRQuestion : DevExpress.XtraEditors.XtraForm
    {
        Database db = Database.NewDataDatabase();
        public fCRQuestion()
        {
            InitializeComponent();
            dNgayCt.EditValue = DateTime.Now;
            this.KeyDown += new KeyEventHandler(fCRQuestion_KeyDown);
        }

        void fCRQuestion_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    tbUpdate_Click(tbUpdate, new EventArgs());
                    break;
                case Keys.F5:
                    btAccept_Click(btAccept, new EventArgs());
                    break;
                case Keys.Escape:
                    this.Dispose();
                    break;
            }
        }
        DateTime ngayct;
        DataSet ds;
        BindingSource bin = new BindingSource();
        private void btAccept_Click(object sender, EventArgs e)
        {
            if (dNgayCt.EditValue == null) return;
            db.UpdateDatabyStore("TotalCR", new string[] { }, new object[] { });

            ngayct = DateTime.Parse(dNgayCt.EditValue.ToString());
            string sql = "insert into ctcauhoi (mtroid, stt)" +
                " select a.mtroid,b.stt   from tmpToTalCR a, dmcauhoi b " +
                " where charindex('PM',a.MaLHDV)>0  and datediff(d,a.ngayct,'" + ngayct.ToShortDateString() + "')=3 and a.mtroid not in (select mtroid from ctcauhoi)";
            db.UpdateByNonQuery(sql);
            sql = "select a.*   from tmpToTalCR a	where charindex('PM',a.MaLHDV)>0  and datediff(d,a.ngayct,'" + ngayct.ToShortDateString() + "')=3;" + 
                " select a.*, b.noidung,b.thangdiem   from Ctcauhoi a inner join dmcauhoi b on a.stt=b.stt" +
                " where a.mtroid in (select a.mtroid from tmpToTalCR a	where charindex('PM',a.MaLHDV)>0 and datediff(d,a.ngayct,'" + ngayct.ToShortDateString() + "')=3  ) ";
                
            ds = db.GetDataSet(sql);
            if (ds.Tables.Count < 2) return;
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["MTROID"] };
            DataRelation dre = new DataRelation("Relation", ds.Tables[0].Columns["MTROID"], ds.Tables[1].Columns["MTROID"], true);
            ds.Relations.Add(dre);
            bin.DataSource = ds;
            bin.DataMember = ds.Tables[0].TableName;
            grMain.DataSource = bin;
            grDetail.DataSource = bin;
            grDetail.DataMember = "Relation";
            ds.Tables[1].ColumnChanged += new DataColumnChangeEventHandler(fCRQuestion_ColumnChanged);
            dxErrorProvider1.DataSource = ds;
        }

        void fCRQuestion_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Column.ColumnName == "Diem")
            {
                if (double.Parse(e.Row["Diem"].ToString()) > double.Parse(e.Row["ThangDiem"].ToString()))
                {
                    e.Row.SetColumnError("Diem", "Điểm nhập lớn hơn thang điểm");
                }
                else
                {
                    e.Row.SetColumnError("Diem", string.Empty);
                }
            }
        }

        private void tbUpdate_Click(object sender, EventArgs e)
        {
            if (ds.HasErrors) return;
            DataView dv = ds.Tables[1].DefaultView;
            dv.RowStateFilter = DataViewRowState.ModifiedCurrent;
            try
            {
                foreach (DataRowView drv in dv)
                {
                    if (drv["Diem"]== DBNull.Value) continue;
                    string sql = "update ctcauhoi set diem=" + drv["Diem"].ToString() + ", Nhanxet=N'" + drv["Nhanxet"].ToString() + "'";
                    if (drv["Ngaygd1"] != DBNull.Value) sql += ", Ngaygd1='" + drv["Ngaygd1"].ToString() + "'";
                    if (drv["Ngaygd2"] != DBNull.Value) sql += ", Ngaygd2='" + drv["Ngaygd2"].ToString() + "'";
                    if (drv["Ngaygd3"] != DBNull.Value) sql += ", Ngaygd3='" + drv["Ngaygd3"].ToString() + "'";
                    sql += " where MTROID='" + drv["MTROID"].ToString() + "' and stt=" + drv["Stt"].ToString();
                    db.UpdateByNonQuery(sql);
                }
                if (!db.HasErrors)
                    MessageBox.Show("Cập nhật thành công", "Thông báo");
            }
            catch { }
            dv.RowStateFilter=DataViewRowState.CurrentRows;
          
        }
    }
}