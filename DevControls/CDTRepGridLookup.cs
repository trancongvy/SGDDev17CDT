using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DataFactory;
namespace DevControls
{
    public partial class CDTRepGridLookup : DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    {
        public CDTRepGridLookup()
        {
            InitializeComponent();
            this.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(CDTRepGridLookup_ButtonClick);

        }
        



        void CDTRepGridLookup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.RaiseButton_click(sender,e);
        }
        string _DymicCondition;
        string _refTable;
        string _condition;
        string _activeFilter;
        public GridLookUpEdit GridLookup;
        public GridView MainView;
        public DataTable MainStruct;
        public bool isFiltered = false;
        public int bsCur = 0;
        public event DevExpress.XtraEditors.Controls.ButtonPressedEventHandler Button_click;

        public void RaiseButton_click(object sender,DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.GridLookup =  sender as GridLookUpEdit;
            GridLookup.Tag = this;
            if (Button_click != null) Button_click(this, e);
            
        }
        private CDTData _data;
        public int DataIndex = -1;
        public CDTData Data
        {
            get { return _data; }
            set { _data = value; }
        }
        public string DymicCondition
        {
            get { return _DymicCondition; }
            set { _DymicCondition = value; }
        }
        public string ActiveFilter
        {
            get { return _activeFilter; }
            set { _activeFilter=value; }
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
       

    }
}
