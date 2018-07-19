using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraPrinting;
namespace Plugins
{
    public interface ICustomControl
    {
        XtraForm MainForm { set;}
        List<GridControl> LstGrid { get; set;}
        List<GridColumn> LstGColumn { get; set;}
        List<LayoutControlItem> LstLCItem { get; set;}
        List<BaseEdit> LstBaseEdit { get; set;}
        StructPara Info { set;}
        void AddEvent();
    }
}
