using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using DataFactory;
using CDTLib;
using CDTControl;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevControls;
namespace CusCDTData
{
    public interface ICDTData
    {
        CDTData data { set;get;}
        DevExpress.XtraGrid.GridControl gc { set;get;}
        DevExpress.XtraGrid.Views.Grid.GridView gv { set;get;}
        List<DevExpress.XtraEditors.BaseEdit> be { set;get;}
        List<DevExpress.XtraLayout.LayoutControlItem> lo { get;set;}
        List<CDTRepGridLookup> rlist { get;set;}
        List<CDTGridLookUpEdit> glist { get;set;}
        List<DevExpress.XtraGrid.GridControl> gridList { set;get;}
        void AddEvent();
        event EventHandler Refresh;
        string Name { get;set;}
    }
}
