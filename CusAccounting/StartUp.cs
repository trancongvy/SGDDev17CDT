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
namespace CusAccounting
{
    public partial class StartUp : DevExpress.XtraEditors.XtraForm
    {
        public StartUp()
        {
            InitializeComponent();
        }
        Database _db = Database.NewDataDatabase();
        DateTime ngayct1 = DateTime.Now;
        DateTime ngayct2 = DateTime.Now;
        private void cbKY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKY.SelectedIndex == 0)
            {
                cbThu.Properties.Items.Clear();
                cbThu.Properties.Items.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
                cbThu.SelectedIndex = DateTime.Now.Month - 1;
            }
            else
            {
                cbThu.Properties.Items.Clear();
                cbThu.Properties.Items.AddRange(new int[] { 1, 2, 3, 4 });
                cbThu.SelectedIndex = (DateTime.Now.Month - 1) / 3;
            }
            cbThu_SelectedIndexChanged(cbThu, e);
            GetData4DTCP();
        }

        private void cbThu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbThu.SelectedIndex < 0) return;
            
                if (cbKY.SelectedIndex == 0)
            {
                ngayct1 = DateTime.Parse((cbThu.SelectedIndex+1).ToString("00") + "/01/" + Config.GetValue("NamLamViec").ToString());
                ngayct2 = ngayct1.AddMonths(1).AddDays(-1);
            }
            else
            {
                ngayct1 = DateTime.Parse(((cbThu.SelectedIndex*3)+1).ToString("00") + "/01/" + Config.GetValue("NamLamViec").ToString());
                ngayct2 = ngayct1.AddMonths(3).AddDays(-1);
            }
            Ngayct1.EditValue = ngayct1;
            Ngayct2.EditValue = ngayct2;
            Config.NewKeyValue("@NgayCT1", ngayct1);
            Config.NewKeyValue("@NgayCT2", ngayct2);
            GetData4DT_NHOMKH();
            GetData4DT_NHOMVT();
            GetData4MaPhi();
        }
        private void GetData4DTCP()
        {
            DataTable tb=new DataTable() ;
            DateTime NgayDN= DateTime.Parse("01/01/"+Config.GetValue("NamLamViec").ToString());
            DateTime NgayCN= DateTime.Parse("12/31/"+Config.GetValue("NamLamViec").ToString());
            if (cbKY.SelectedIndex == 0)
                tb = _db.GetDataSetByStore("GetData4DTCP_thang", new string[] { "Ngayct1", "Ngayct2" }, new object[] { NgayDN, NgayCN });
            else
                tb = _db.GetDataSetByStore("GetData4DTCP_Quy", new string[] { "Ngayct1", "Ngayct2" }, new object[] { NgayDN, NgayCN });
     
            ChDTCP.DataSource = tb.DefaultView;
            ChDTCP.Series[0].ArgumentDataMember = "thangnam";
            ChDTCP.Series[1].ArgumentDataMember = "thangnam";
            ChDTCP.Series[0].ValueDataMembers.AddRange(new string[] { "DT" });
            ChDTCP.Series[1].ValueDataMembers.AddRange(new string[] { "CP" });
           // ChDTCP.CreateGraphics();
            
                }
        private void GetData4DT_NHOMKH()
        {
            DataTable tb = new DataTable();
            tb = _db.GetDataSetByStore("GetData4DT_NHOMKH", new string[] { "Ngayct1", "Ngayct2" }, new object[] { ngayct1, ngayct2 });
            chKH.DataSource = tb.DefaultView;
            chKH.Series[0].ArgumentDataMember = "Nhomkh";
            chKH.Series[0].ValueDataMembers.AddRange(new string[] { "DT" });
        }
        private void GetData4DT_NHOMVT()
        {
            DataTable tb = new DataTable();
            tb = _db.GetDataSetByStore("GetData4DT_NHOMVT", new string[] { "Ngayct1", "Ngayct2" }, new object[] { ngayct1, ngayct2 });
            chHH.DataSource = tb.DefaultView;
            chHH.Series[0].ArgumentDataMember = "nhomVT";
            chHH.Series[0].ValueDataMembers.AddRange(new string[] { "DT" });
        }
        private void GetData4MaPhi()
        {
            string sql = "select a.maphi, b.tenphi, sum(a.psno) as psno from bltk a inner join dmphi b on a.maphi=b.maphi  where tk like '6%' and ngayct between '" + ngayct1.ToShortDateString() + "' and '" + ngayct2.ToShortDateString() + "'  group by a.maphi, b.tenphi";
            DataTable tb = _db.GetDataTable(sql);
            gridControl1.DataSource = tb;
        }
        private void StartUp_Load(object sender, EventArgs e)
        {
            cbKY.SelectedIndex = 0;
        }
    }
}