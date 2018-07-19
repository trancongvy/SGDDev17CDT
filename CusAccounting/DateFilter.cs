using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CusAccounting
{
    public partial class DateFilter : DevExpress.XtraEditors.XtraForm
    {
        public DateFilter()
        {
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(DateFilter_KeyUp);
            vDateEdit1.EditValue=DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
            vDateEdit2.EditValue=DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
        }

        void DateFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Dispose();
            if (e.KeyCode == Keys.F12) simpleButton1_Click(simpleButton1, new EventArgs());
        }
        public DateTime TuNgay;
        public DateTime DenNgay;
        public bool isAccept = false;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            TuNgay = DateTime.Parse(vDateEdit1.EditValue.ToString());
            DenNgay = DateTime.Parse(vDateEdit2.EditValue.ToString());
            isAccept = true;
            this.Dispose();
        }

    }
}