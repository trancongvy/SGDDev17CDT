using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CDTSystem
{
    public partial class selectCert : DevExpress.XtraEditors.XtraForm
    {
        DataTable _tb;
        public selectCert(DataTable tb)
        {
            _tb = tb;
            InitializeComponent();
        }
        public int SIndex=-1;
        private void Form_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = _tb;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                SIndex = gridView1.GetSelectedRows()[0];
                this.Dispose();
            }
        }
    }
}