using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using CDTControl;
using CDTDatabase;
namespace CusNissan
{
    public partial class fCRManage : DevExpress.XtraEditors.XtraForm
    {
        private DateTime Ngayht;
        private int GoitruocPDI=7;
        private int GoitruocPM = 20;
        private int TgCSPM = -3;
        private int TgCSPDI= -1;
        DataTable tbMain;
        CDTDatabase.Database _db = CDTDatabase.Database.NewDataDatabase();
        BindingSource bs = new BindingSource();
        DataTable tblydo;
        public fCRManage()
        {
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(fCRManage_KeyUp);
            Ngayht = DateTime.Parse(Config.GetValue("NgayHethong").ToString());
            try
            {
                GoitruocPDI = int.Parse(Config.GetValue("GoitruocPDI").ToString());
                TgCSPM = int.Parse(Config.GetValue("TgCSRO").ToString());
                TgCSPDI = int.Parse(Config.GetValue("TgCSBH").ToString());
                GoitruocPM = int.Parse(Config.GetValue("GoitruocPM").ToString());
            }
            catch
            {
            }
            bandedGridColumn22.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            bandedGridColumn27.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            dNgayct.EditValue = Ngayht;
            getData();
        }

        void fCRManage_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    btESC_Click(btESC, new EventArgs());
                    break;
                case Keys.F12:
                    btUpdate_Click(btUpdate, new EventArgs());
                    break;
                case Keys.F5:
                    btRefresh_Click(btRefresh, new EventArgs());
                    break;
            }
        }

        private void getData()
        {
            tbMain = _db.GetDataSetByStore("GetTTCR", new string[] {"@goitruocPDI", "@goitruocPM", "@tgChamsocPDI", "@tgChamsocPM", "@ngayht" }, new object[] {GoitruocPDI, GoitruocPM, TgCSPDI, TgCSPM, Ngayht });
            bs.DataSource = tbMain;
            bs.DataMember = tbMain.TableName;
        }

        private void fCRManage_Load(object sender, EventArgs e)
        {
            this.grMain.DataSource = bs;
            string sql = "select * from dmlydo";
            tblydo = _db.GetDataTable(sql);
            this.riLydo.DataSource = tblydo;
            gvMain.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(gvMain_CustomDrawCell);

        }

        void gvMain_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                Brush brush = null;
                if (e.Column.FieldName != "Ngaygd") return;
                if (e.CellValue == DBNull.Value)
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.White, Color.Red, 90);
                else                    
                    return;
                Rectangle r;
                r = e.Bounds;
                e.Graphics.FillRectangle(brush, r);
                r.Inflate(0, 0);
                e.Appearance.DrawString(e.Cache, e.DisplayText, r);
                //if (isFocusedCell)
                //    DevExpress.Utils.Paint.XPaint.Graphics.DrawFocusRectangle(e.Graphics, e.Bounds, SystemColors.WindowText, e.Appearance.BackColor);
                e.Handled = true;
            }
            catch
            {
            }
        }

        private void btESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dNgayct_EditValueChanged(object sender, EventArgs e)
        {
            Ngayht = DateTime.Parse(dNgayct.EditValue.ToString());
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {

            string sql;
            DataView dvMain =tbMain.DefaultView;
            try
            {
                _db.BeginMultiTrans();
                dvMain.RowStateFilter = DataViewRowState.ModifiedOriginal;
                foreach (DataRowView dr in dvMain)
                {
                    //Trường hợp thêm mới mà chưa thêm ngày
                    if (dr.Row["ngaygd"] == DBNull.Value && dr["ngaygd"] == DBNull.Value)
                    {
                        dr.Row.SetColumnError(dr.Row.Table.Columns["ngaygd"], "dữ liệu chưa hợp lệ");
                        continue;
                    }//trường hợp thêm mới
                    else if (dr.Row["ngaygd"] != DBNull.Value && dr["ngaygd"] == DBNull.Value)
                    {
                        sql = genInsertString(dr.Row);
                        _db.UpdateByNonQuery(sql);
                    }
                    else if (dr.Row["ngaygd"] != DBNull.Value && dr["ngaygd"] != DBNull.Value)
                    {
                        sql = genUpdateString(dr.Row);
                        _db.UpdateByNonQuery(sql);
                        
                    }
                    if (_db.HasErrors)
                    {
                        break;
                    }
                    else
                    {
                        dr.Row.AcceptChanges();
                    }
                }
                if (_db.HasErrors)
                {
                    _db.RollbackMultiTrans();
                }
                else
                {
                    _db.EndMultiTrans();
                }
            }
            catch (Exception ex)
            {
                _db.RollbackMultiTrans();
            }
            dvMain.RowStateFilter = DataViewRowState.CurrentRows;
        }

        private string genUpdateString(DataRow dr)
        {
            string sql = "update ctcuochen set ngaygd='" + dr["ngaygd"].ToString() + "'";
            if (dr["isden"] != DBNull.Value) { sql += ",  isden ="; sql += dr["isden"].ToString() == "True" ? "1" : "0"; }
            if (dr["noidung"] != DBNull.Value) sql += ", noidung =N'" + dr["noidungCH"].ToString() + "'";
            if (dr["Ngayden"] != DBNull.Value) sql += ",ngayden ='" + dr["Ngayden"].ToString() + "'";
            if (dr["Ngaygd2"] != DBNull.Value) sql += ",ngaygd2 ='" + dr["Ngaygd2"].ToString() + "'";
            if (dr["Ngaygd3"] != DBNull.Value) sql += ",ngaygd3 ='" + dr["Ngaygd3"].ToString() + "'";
            if (dr["MaLydo"] != DBNull.Value && dr["isden"].ToString() != "True") sql += ", Malydo ='" + dr["MaLydo"].ToString() + "'";
            else sql += ", Malydo = Null";
            sql += " where stt='" + dr["stt"].ToString() + "'";
            return sql;
        }
        private string genInsertString(DataRow dr)
        {
            string sql = "insert into ctcuochen ([stt],[Ngayct],[MaXe],[MaKH],[Ngaygd],[Ngaygd2],[Ngaygd3],[isden],[noidung],[Ngayden],[MaLydo])" +
             " values('" + dr["stt"].ToString() + "','" + dr["ngayct"].ToString() + "','" + dr["maxe"].ToString() + "','" +
               dr["makh"].ToString() + "','" + dr["ngaygd"].ToString() + "'";
             
             if (dr["Ngaygd2"] == DBNull.Value)
             {
                 sql = sql.Replace(",[Ngaygd2]", "");
             }
             else
             {
                 sql += ",'" + dr["Ngaygd2"].ToString() + "'";
             }
             if (dr["Ngaygd3"] == DBNull.Value)
             {
                 sql = sql.Replace(",[Ngaygd3]", "");
             }
             else
             {
                 sql += ",'" + dr["Ngaygd3"].ToString() + "'";
             }
             if (dr["isden"] == DBNull.Value)
             {
                 sql = sql.Replace(",[isDen]", "");
             }
             else
             {
                 sql += ",";
                 sql += dr["isden"].ToString() == "True" ? "1" : "0";
             }
             if (dr["noidungCH"] == DBNull.Value)
             {
                 sql=sql.Replace(",[noidung]", "");
             }
             else
             {
                 sql += ",N'" + dr["noidungCH"].ToString() +"'";
             }
             if (dr["Ngayden"] == DBNull.Value)
             {
                 sql = sql.Replace(",[Ngayden]", "");
             }
             else
             {
                 sql += ",'" + dr["Ngayden"].ToString() + "'";
             }
             if (dr["MaLydo"] == DBNull.Value || dr["isden"].ToString() == "True")
             {
                 sql = sql.Replace(",[MaLydo]", "");
             }
             else
             {
                 sql += ",'" + dr["MaLydo"].ToString() + "'";
             }
             sql += ")";
             return sql;
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            getData();
            this.grMain.DataSource = bs;
        }




    }
}