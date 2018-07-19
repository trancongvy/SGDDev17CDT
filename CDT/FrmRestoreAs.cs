using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CDT
{
    public partial class FrmRestoreAs : DevExpress.XtraEditors.XtraForm
    {
        public DateTime d;
        public string DataAnother;
        public FrmRestoreAs()
        {
            InitializeComponent();
        }

        private void FrmDateSelect_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = DateTime.Now;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (dateEdit1.EditValue != null && (txtDataAnother.Text.IndexOf("CDT") == 0 || txtDataAnother.Text.IndexOf("CBA") == 0))
            {
                d = DateTime.Parse(dateEdit1.EditValue.ToString());
                DataAnother = txtDataAnother.Text;
                this.Dispose();
            }
            else
            {
                
            }
        }
    }
}