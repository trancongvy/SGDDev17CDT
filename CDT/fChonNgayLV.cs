using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
namespace CDT
{
    public partial class fChonNgayLV : DevExpress.XtraEditors.XtraForm
    {
        public fChonNgayLV()
        {
            InitializeComponent();
        }

        private void fChonNgayLV_Load(object sender, EventArgs e)
        {
            try
            {
                vDateEdit1.EditValue = DateTime.Parse(Config.GetValue("NgayHethong").ToString());
            }
            catch { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (vDateEdit1.EditValue != null)
                Config.NewKeyValue("NgayHethong", vDateEdit1.EditValue);
            this.Dispose();
        }
    }
}