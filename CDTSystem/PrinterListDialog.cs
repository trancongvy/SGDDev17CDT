using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Printing;
namespace CDTSystem
{
    public partial class PrinterListDialog : DevExpress.XtraEditors.XtraForm
    {
        public string printerName = "";
        public PrinterListDialog()
        {
            InitializeComponent();
        }

        private void PrinterListDialog_Load(object sender, EventArgs e)
        {
            listBoxControl1.Items.Clear();
            foreach (string prt in PrinterSettings.InstalledPrinters)
            {
                listBoxControl1.Items.Add(prt);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (listBoxControl1.SelectedIndex >= 0)
            {
                printerName = listBoxControl1.SelectedItem.ToString();
                this.Dispose();
            }
        }
    }
}