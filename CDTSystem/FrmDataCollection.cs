using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CBSControls;
using CDTControl;

namespace CDTSystem
{
    public partial class FrmDataCollection : DevExpress.XtraEditors.XtraForm
    {
        public FrmDataCollection()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            DataCollection coll = new DataCollection(DateTime.Parse(cDateEdit1.EditValue.ToString()), DateTime.Parse(cDateEdit2.EditValue.ToString()));
            coll.Collect();
        }

        private void FrmDataCollection_Load(object sender, EventArgs e)
        {

        }
    }
}