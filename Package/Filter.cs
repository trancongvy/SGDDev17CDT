using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Package
{
    public partial class Filter : DevExpress.XtraEditors.XtraForm
    {
        public Filter()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            dupplicationPack dp = new dupplicationPack(textEditCnnSource.Text, textEdit1.Text, textEdit2.Text);
            MessageBox.Show("Ok");
        }
    }
}