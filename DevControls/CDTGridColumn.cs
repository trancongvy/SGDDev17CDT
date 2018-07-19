using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
namespace DevControls
{
    public partial class CDTGridColumn : DevExpress.XtraGrid.Columns.GridColumn
    {
        public CDTGridColumn()
        {
            InitializeComponent();
        }

        DataRow _masterRow;
        string _refFilter;
        bool _isExCol = false;
        public DataRow MasterRow
        {
            get { return _masterRow; }
            set { _masterRow = value; }
        }
        public bool isExCol
        {
            get { return _isExCol; }
            set { _isExCol = value; }
        }
        public string refFilter
        {
            get { return _refFilter; }
            set { _refFilter = value; }
        }
    }
}
