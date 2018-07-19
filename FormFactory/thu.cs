using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
namespace FormFactory
{
    public partial class thu : DevExpress.XtraEditors.XtraForm
    {
        public thu()
        {
            InitializeComponent();
        }

        private void thu_Load(object sender, EventArgs e)
        {
            //XRBinding binding = new XRBinding("Text", dsProducts1, "Products.UnitPrice");

            
        }

        private void checkedListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}