using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTControl;
using CDTDatabase;
namespace CusNissan
{
    public partial class fImExcel : DevExpress.XtraEditors.XtraForm
    {
        public fImExcel()
        {
            InitializeComponent();
        }
        ImportExcel IEx;
        DataTable MapStruct ;
        Database _db = Database.NewDataDatabase();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "AllExel|*.xls";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tFileName.EditValue = dialog.FileName;
                IEx = new ImportExcel(dialog.FileName);
                List<string> sheets = IEx.GetSheets();
                lSheet.Properties.Items.Clear();
                lSheet.Properties.Items.AddRange(sheets.ToArray());
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string sql;
            if (IEx == null || IEx.Db == null) return;
            if (MapStruct == null) return;
            if (MapStruct.Rows[0]["ColName"].ToString() == string.Empty ) return;
            if (MapStruct.Rows[1]["ColName"].ToString() == string.Empty) return;
            _db.BeginMultiTrans();
            foreach (DataRow dr in IEx.Db.Rows)
            {
                if (dr[MapStruct.Rows[0]["ColName"].ToString()] == DBNull.Value) continue;
                if (dr[MapStruct.Rows[0]["ColName"].ToString()].ToString() == string.Empty) continue;
                if (dr[MapStruct.Rows[1]["ColName"].ToString()] == DBNull.Value) continue;
                if (dr[MapStruct.Rows[1]["ColName"].ToString()].ToString() ==string.Empty) continue;
                sql = "update dmvt set giaban=" + dr[MapStruct.Rows[1]["ColName"].ToString()].ToString() + " where mavt='" + dr[MapStruct.Rows[0]["ColName"].ToString()].ToString() + "'";
                _db.UpdateByNonQuery(sql);
                if (_db.HasErrors) { _db.RollbackMultiTrans(); return; }
                sql = "update dmdv set dongia2=dongia1 where madv='" + dr[MapStruct.Rows[0]["ColName"].ToString()].ToString() + "'";
                _db.UpdateByNonQuery(sql);
                if (_db.HasErrors) { _db.RollbackMultiTrans(); return; }
                sql = "update dmdv set dongia1=dongia where madv='" + dr[MapStruct.Rows[0]["ColName"].ToString()].ToString() + "'";
                _db.UpdateByNonQuery(sql);
                if (_db.HasErrors) { _db.RollbackMultiTrans(); return; }
                sql = "update dmdv set dongia=" + dr[MapStruct.Rows[1]["ColName"].ToString()].ToString() + " where madv='" + dr[MapStruct.Rows[0]["ColName"].ToString()].ToString() + "'";
                _db.UpdateByNonQuery(sql);
                if (_db.HasErrors) { _db.RollbackMultiTrans(); return; }
               
            }
            if (!_db.HasErrors)
            {
                _db.EndMultiTrans();
                MessageBox.Show("Update thành công");
            }
        }

        private void lSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lSheet.EditValue == null) return;
            List<string> cols= IEx.GetCol(lSheet.EditValue.ToString());
            if (cols == null) return;
            RiCom.Items.AddRange(cols.ToArray());
            MapStruct = new DataTable();
            MapStruct.Columns.Add("FieldName", typeof(string));
            MapStruct.Columns.Add("ColName", typeof(string));
            DataRow dr = MapStruct.NewRow();
            dr["FieldName"] = "MaVt";
            MapStruct.Rows.Add(dr);
            dr = MapStruct.NewRow();
            dr["FieldName"] = "GiaBan";
            MapStruct.Rows.Add(dr);
            gridControl1.DataSource = MapStruct;
            gridControl1.DataMember = MapStruct.TableName;
        }
    }
}