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
namespace CustomClass
{
    public partial class fNhapChisocongter : DevExpress.XtraEditors.XtraForm
    {
        Database _db = Database.NewDataDatabase();  
        DataTable tbChiso;
        DateTime today;
        public fNhapChisocongter()
        {
            InitializeComponent();
            today = DateTime.Parse(Config.GetValue("NgayHethong").ToString());
            getdata();
            
            this.gridControl1.DataSource = tbChiso;
            this.dxErrorProvider1.DataSource = new BindingSource(tbChiso,"Chiso2");
            dNgay.EditValue = today;
            if (tbChiso.Rows.Count > 0)
            {
                this.dNgay.EditValue = tbChiso.Rows[0]["NgayCt"];
                //this.labelControl2.Text = "Ca " + tbChiso.Rows[0]["Maca"].ToString();
            }
            if (DateTime.Parse(dNgay.EditValue.ToString()) > today)
            {
                MessageBox.Show("Ca trước đã được cập nhật");
                isUpdated = true;
            }
        }
        bool isUpdated = false;
        private void getdata()
        {
            tbChiso = _db.GetDataSetByStore("GetChisoCongter", new string[] { "@NgayCT" }, new object[] { today });
        }

        private void cUpdate_Click(object sender, EventArgs e)
        {
            if (isUpdated) return; 
            foreach (DataRow dr in tbChiso.Rows)
            {
                if (double.Parse(dr["Chiso1"].ToString()) > double.Parse(dr["Chiso2"].ToString()))
                { dr.SetColumnError("Chiso2", "Số nhập chưa hợp lệ");
                }
                else dr.SetColumnError("Chiso2", "");
            }
   
            if (!this.tbChiso.HasErrors)
            {
                string sql;
                try
                {
                    _db.BeginMultiTrans();
                    sql = "delete ctcongter where ngayct='" + today.ToString() + "'";
                    _db.UpdateByNonQuery(sql);
                    foreach (DataRow dr in tbChiso.Rows)
                    {
                      
                        sql = "insert into ctCongter(CTID,Ngayct,macongter, chiso) values(newid(), '";
                        sql += dr["NgayCt"].ToString() + "', '";
                        sql += dr["MaCongter"].ToString() + "',";
                        sql += dr["Chiso2"].ToString() + ")";
                        _db.UpdateByNonQuery(sql);
                        if (_db.HasErrors)
                        {
                            _db.RollbackMultiTrans();

                            break;
                        }
                    }
                    if (!_db.HasErrors)
                    {
                        _db.EndMultiTrans();
                        MessageBox.Show("Cập nhật thành công");
                        this.Dispose();
                    }
                }
                catch
                {
                    _db.RollbackMultiTrans();
                    MessageBox.Show("Cập nhật thành công");
                }
            }
        }




    }
}