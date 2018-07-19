using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views;
using DataFactory;
namespace DevControls
{
    public partial class CDTGridLookUpEdit : DevExpress.XtraEditors.GridLookUpEdit
    {
        public CDTGridLookUpEdit()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();
            this.EnterMoveNextControl = true;
            this.Properties.NullText = string.Empty;
            this.Properties.View.OptionsBehavior.AllowIncrementalSearch = true;
            this.Properties.View.OptionsView.ShowAutoFilterRow = true;
            this.Properties.ImmediatePopup = true;
            this.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            InitializeComponent();
        }
        
        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here
            if (this.FindForm() != null)
                this.BackColor = this.FindForm().BackColor;
            // Calling the base class OnPaint
            base.OnPaint(pe);

            ControlPaint.DrawBorder(pe.Graphics, pe.ClipRectangle,
            this.Parent.BackColor, 0, ButtonBorderStyle.Solid,
            this.Parent.BackColor, 0, ButtonBorderStyle.Solid,
            this.Parent.BackColor, 0, ButtonBorderStyle.Solid,
            System.Drawing.Color.Black, 1, ButtonBorderStyle.Dotted);
        }
        string _DymicCondition;
        string _refTable;
        string _condition;
        string _activeFilter;
        CDTData _Data;
        bool _Allownull;
        public bool isFiltered = false;
        public int DataIndex=-1;
        public string fieldName;
        public string ActiveFilter
        {
            get { return _activeFilter; }
            set { _activeFilter = value; }
        }
        public CDTData Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        public string DymicCondition
        {
            get { return _DymicCondition; }
            set { _DymicCondition = value; }
        }
        public string refTable
        {
            get { return _refTable; }
            set { _refTable = value; }
        }
        public string Condition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        public bool Allownull
        {
            get { return _Allownull; }
            set { _Allownull = value; }
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Delete)
                this.EditValue = null;
        }
    }
}
