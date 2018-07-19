using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CBSControls;
namespace sysCollect
{
    public partial class fFilter : DevExpress.XtraEditors.XtraForm
    {
        public fFilter()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            CollectData coll = new CollectData(DateTime.Parse(cDateEdit1.EditValue.ToString()), DateTime.Parse(cDateEdit2.EditValue.ToString()));
            coll.Collect();
        }
    }
}