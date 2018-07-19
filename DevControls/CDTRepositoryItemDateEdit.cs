using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
namespace DevControls
{
    public partial class CDTRepositoryItemDateEdit : RepositoryItemDateEdit
    {
        public CDTRepositoryItemDateEdit()
        {

            this.DisplayFormat.FormatType = FormatType.DateTime;
            this.DisplayFormat.FormatString = "dd/MM/yyyy";

            this.EditMask = "dd/MM/yyyy";
            this.Mask.UseMaskAsDisplayFormat = true;
            //this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(CDTRepositoryItemDateEdit_KeyUp);
            this.Leave += new EventHandler(CDTRepositoryItemDateEdit_Leave);
            this.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(CDTRepositoryItemDateEdit_EditValueChanging);
        }

        void CDTRepositoryItemDateEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if ( DateTime.Parse(e.OldValue.ToString()) == DateTime.Parse(DateTime.Now.ToShortDateString()) && (this.EditFormat.FormatString.Contains("HH")||this.EditFormat.FormatString.Contains("hh")))
                e.NewValue = DateTime.Now;
                
        }

        void CDTRepositoryItemDateEdit_Leave(object sender, EventArgs e)
        {
            i = 0;
        }

        void CDTRepositoryItemDateEdit_KeyUp(object sender, KeyEventArgs e)
        {

            //if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.D1
            //   || e.KeyCode == Keys.D2 || e.KeyCode == Keys.D3
            //   || e.KeyCode == Keys.D4 || e.KeyCode == Keys.D5
            //   || e.KeyCode == Keys.D6 || e.KeyCode == Keys.D7
            //   || e.KeyCode == Keys.D8 || e.KeyCode == Keys.D9
            //   || e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.NumPad1
            //   || e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.NumPad3
            //   || e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.NumPad5
            //   || e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.NumPad7
            //   || e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.NumPad9)
            //    ++i;

            //if (i == 2)
            //{
            //    if (this.te.SelectionStart == 0)
            //    {
            //        i = 0;
            //        this.Select(3, 2);
            //        return;
            //    }
            //    if (this.SelectionStart == 3)
            //    {
            //        i = 0;
            //        this.Select(6, 4);
            //    }
            //    try
            //    {
            //        if (this.SelectionStart == 11)
            //        {
            //            i = 0;
            //            this.Select(14, 2);
            //        }
            //        if (this.SelectionStart == 14)
            //        {
            //            i = 0;
            //            this.Select(17, 2);
            //        }
            //    }
            //    catch { }
            //}
            //if (i == 4)
            //{
            //    if (this.SelectionStart == 6)
            //    {
            //        i = 0;
            //        this.Select(11, 2);
            //    }
            //}
        }
       
        private int i = 0;

    }
           
    
}
