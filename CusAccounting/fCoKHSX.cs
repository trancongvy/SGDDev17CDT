using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using CDTLib;
namespace CusAccounting
{
    public partial class fCoKHSX : DevExpress.XtraEditors.XtraForm
    {
        public DataTable MainData;
        private Database db = CDTDatabase.Database.NewDataDatabase();
        private DateTime ngayct1;
        private DateTime ngayct2;
        private DataTable btKho;
        public fCoKHSX()
        {
            InitializeComponent();
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            ngayct1 = DateTime.Parse(dateEdit1.EditValue.ToString());
        }

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            ngayct2 = DateTime.Parse(dateEdit2.EditValue.ToString());
        }

        private void fCoKHSX_Load(object sender, EventArgs e)
        {
            btKho = db.GetDataTable("select MaKho, TenKho from dmkho");
            gKhoNL.Properties.DataSource = btKho;
            gKhoNL.Properties.ValueMember = "MaKho";
            gKhoNL.Properties.DisplayMember= "TenKho";
            gKhoTP.Properties.DataSource = btKho;
            gKhoTP.Properties.ValueMember = "MaKho";
            gKhoTP.Properties.DisplayMember = "TenKho";
        }

        private void btGetdata_Click(object sender, EventArgs e)
        {
            if(ngayct1 !=null && ngayct2 !=null)
            {
                MainData = db.GetDataSetByStore("GetCoKHSX", new string[] {"@ngayct1", "@ngayct2" }, new object[] {ngayct1, ngayct2 });
                if (MainData == null) return;
                gridControl1.DataSource = MainData;

            }
        }
    }
}