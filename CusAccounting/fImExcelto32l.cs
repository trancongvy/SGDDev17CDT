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
using CDTLib;
namespace CusAccounting
{
    public partial class fImExcelto32 : DevExpress.XtraEditors.XtraForm
    {
        public fImExcelto32()
        {
            InitializeComponent();

            MapStruct = new DataTable();

            MapStruct.Columns.Add("FieldName", typeof(string));
            MapStruct.Columns.Add("Type", typeof(int));
            MapStruct.Columns.Add("ColName", typeof(string));
            MapStruct.Columns.Add("DefaultValue", typeof(string));
            MapStruct.Columns.Add("AllowNull", typeof(bool));

            DataRow dr0 = MapStruct.NewRow();
            dr0["FieldName"] = "Stt";
            dr0["Type"] = 5;
            dr0["DefaultValue"] = 0;
            dr0["AllowNull"] = 0;
            MapStruct.Rows.Add(dr0);

            DataRow dr = MapStruct.NewRow();
            dr["FieldName"] = "MaVT";
            dr["Type"] = 1;
            dr["DefaultValue"] = DBNull.Value;
            dr["AllowNull"] = 0;
            MapStruct.Rows.Add(dr);

            DataRow dr1 = MapStruct.NewRow();
            dr1["FieldName"] = "SoLuong";
            dr1["Type"] = 8;
            dr1["DefaultValue"] = 0;
            dr1["AllowNull"] = 0;
            MapStruct.Rows.Add(dr1);
            DataRow dr2 = MapStruct.NewRow();
            dr2["FieldName"] = "DonGia";
            dr2["Type"] = 8;
            dr2["DefaultValue"] = 0;
            dr2["AllowNull"] = 0;
            MapStruct.Rows.Add(dr2);
            DataRow dr3 = MapStruct.NewRow();
            dr3["FieldName"] = "ThanhTien";
            dr3["Type"] = 8;
            dr3["DefaultValue"] = 0;
            dr3["AllowNull"] = 0;
            MapStruct.Rows.Add(dr3);
            DataRow dr4 = MapStruct.NewRow();
            dr4["FieldName"] = "Loai";
            dr4["Type"] = 2;
            dr4["DefaultValue"] = "P";
            dr4["AllowNull"] = 0;
            MapStruct.Rows.Add(dr4);
            gridControl1.DataSource = MapStruct;
            gridControl1.DataMember = MapStruct.TableName;
        }
        public DataTable dbEx = null;
        ImportExcel IEx;
        public DataTable MapStruct;
        Database _db = Database.NewDataDatabase();
        Database _dbStruct = Database.NewStructDatabase();        
        DataTable dataType;
        DateTime ngayct=DateTime.Now;
        DataTable Khongtimthay;
        DataTable Conphaixuat;
        private void fImExcel_Load(object sender, EventArgs e)
        {
        
        }
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
            if (IEx != null && IEx.Db != null)
            {
                dbEx = IEx.Db;
                //this.Dispose();
            }
            else
            {
                MessageBox.Show("Không nhận được dữ liệu");
            }
            if (dateEdit1.EditValue != null) ngayct = DateTime.Parse(dateEdit1.EditValue.ToString());
            ImportDetailFromExcel(dbEx, MapStruct);
            
        }

        private void ImportDetailFromExcel(DataTable dataTable, DataTable MapStruct)
        {
            //Insert  dữ liệu vào file tmp
            //Chạy proc 
            string str = "delete mt32tmp";
            _db.UpdateByNonQuery(str);
            foreach (DataRow drdata in dataTable.Rows)
            {
                bool stop = false;
                string sql = "insert into MT32TMP (";
                string value = " Values(";
                foreach (DataRow drMap in MapStruct.Rows)
                {
                    if (drMap["FieldName"].ToString() == "Stt" && drdata[drMap["ColName"].ToString()] == DBNull.Value)
                    {
                        stop = true;
                    }
                    string note = "";
                    if (int.Parse(drMap["Type"].ToString()) < 3) note = "'";
                    sql = sql + drMap["FieldName"].ToString() + ",";
                    if (drMap["ColName"] != DBNull.Value)

                        value = value + note + drdata[drMap["ColName"].ToString()].ToString() + note + ",";
                    else
                        value = value + note + drMap["DefaultValue"].ToString() + note + ",";
                    
                }
                if (stop) break;
                sql = sql.Substring(0, sql.Length - 1) + ") " + value.Substring(0, value.Length - 1) + ")";
                _db.UpdateByNonQuery(sql);
                if (_db.HasErrors) break;
            }
            Khongtimthay= _db.GetDataSetByStore("CreateMT32fromExcel", new string[] { "@ngayct" }, new object[] { ngayct });
            if (Khongtimthay!=null)
            gridControl3.DataSource = Khongtimthay;

            Conphaixuat = _db.GetDataTable("select * from mt32conxuat");
            if (Conphaixuat != null)
                gridControl2.DataSource = Conphaixuat;
        }
        

        private void lSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lSheet.EditValue == null) return;
            List<string> cols= IEx.GetCol(lSheet.EditValue.ToString());
            if (cols == null) return;
            RiCom.Items.AddRange(cols.ToArray());
            
        }
        DataTable dmField;


        private void tFileName_EditValueChanged(object sender, EventArgs e)
        {

        }
        SaveFileDialog f;
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            f = new SaveFileDialog();
            f.Filter = "AllExel|*.xls";
            f.ShowDialog();

            if (Khongtimthay != null && f.FileName != string.Empty)
                gridControl3.ExportToXls(f.FileName);

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            f = new SaveFileDialog();
            f.Filter = "AllExel|*.xls";
            f.ShowDialog();
            if (Conphaixuat != null && f.FileName != string.Empty )
                gridControl2.ExportToXls(f.FileName);
        }


    }
}