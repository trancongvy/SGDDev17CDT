using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using CusData;
using CDTLib;
using CDTControl;
using Plugins;
using CusCDTData;
using System.IO;
using System.Runtime.Remoting;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevControls;
namespace CusForm
{
    public partial class FrmMasterDetailDt : CDTForm
    {
        private bool _x;
        public FrmMasterDetailDt(CDTData data)
        {
            InitializeComponent();

            this._data = data;
            _data.SetDetailValue += new EventHandler(_data_SetDetailValue);
            this._frmDesigner = new FormDesigner(this._data);
            _frmDesigner.formAction = FormAction.New;
            _bindingSource.DataSource = this._data.DsData;

            _frmDesigner.bindingSource = _bindingSource;
            dataNavigatorMain.DataSource = _frmDesigner.bindingSource;
            dxErrorProviderMain.DataSource = _frmDesigner.bindingSource;
            _bindingSource.AddNew();

            InitializeLayout();
            this.Load += new EventHandler(FrmMasterDetailCt_Load);
            if (Config.GetValue("Language").ToString() == "1")
                DevLocalizer.Translate(this);
            else
                this.dataNavigatorMain.TextStringFormat = "Mục {0} của {1}";
            dataNavigatorMain.PositionChanged += new EventHandler(dataNavigatorMain_PositionChanged);
        }

        void _data_SetDetailValue(object sender, EventArgs e)
        {
            List<object> ob = sender as List<object>;

            string FieldName=ob[0].ToString();
            string value = ob[1].ToString();
            GridColumn col = gvMain.Columns[FieldName];
            CDTRepGridLookup tmp = col.ColumnEdit as CDTRepGridLookup;
            if (tmp == null) return;
            this._frmDesigner.setRepFilter(tmp,value);
        }
        private List<ICDTData> _lstICDTData = new List<ICDTData>();
        string _pluginPath = "";
        
        private void AddICDTData(CDTData Data)
        {
            if (Config.GetValue("DuongDanPlugins") != null)
                _pluginPath = Config.GetValue("DuongDanPlugins").ToString() + "\\" + Config.GetValue("Package").ToString() + "\\";
            else
                _pluginPath = System.Windows.Forms.Application.StartupPath + "\\Plugins\\" + Config.GetValue("Package").ToString() + "\\";

            if (!Directory.Exists(_pluginPath))
                return;
            string[] dllFiles = Directory.GetFiles(_pluginPath, "*.dll");
            foreach (string str in dllFiles)
            {
                FileInfo f = new FileInfo(str);
                string t = f.Name.Split(".".ToCharArray())[0];
                string pluginName = t + "." + t;
                ObjectHandle oh = Activator.CreateComInstanceFrom(str, pluginName);
                ICDTData pluginClass = oh.Unwrap() as ICDTData;
                if (pluginClass != null)
                {
                    if (!_lstICDTData.Contains(pluginClass))
                    {
                        _lstICDTData.Add(pluginClass);
                        pluginClass.gc = this.gcMain;
                        pluginClass.gv = this.gvMain;
                        pluginClass.be = this._frmDesigner._BaseList;
                        pluginClass.lo = this._frmDesigner._LayoutList;
                        pluginClass.glist = this._frmDesigner._glist;
                        pluginClass.rlist = this._frmDesigner.rlist;
                        pluginClass.Refresh += new EventHandler(pluginClass_Refresh);
                        pluginClass.data = Data;
                    }
                }
            }
        }

        void pluginClass_Refresh(object sender, EventArgs e)
        {
            this._frmDesigner.RefreshDataLookupForColChanged();
            //this._frmDesigner.RefreshDataForLookup();
        }
        public FrmMasterDetailDt(FormDesigner frmDesigner)
        {
            InitializeComponent();
            this._data = frmDesigner.Data;
            _data.SetDetailValue += new EventHandler(_data_SetDetailValue);
            this._frmDesigner = frmDesigner;
            this._bindingSource = frmDesigner.bindingSource;
            dataNavigatorMain.DataSource = this._bindingSource;
            dxErrorProviderMain.DataSource = this._bindingSource;

            InitializeLayout();
            this.Load += new EventHandler(FrmMasterDetailCt_Load);
            if (Config.GetValue("Language").ToString() == "1")
                DevLocalizer.Translate(this);
            dataNavigatorMain.PositionChanged += new EventHandler(dataNavigatorMain_PositionChanged);
            AddICDTData(_data);
            this.WindowState = FormWindowState.Maximized;
        }

        void dataNavigatorMain_PositionChanged(object sender, EventArgs e)
        {
            if (_frmDesigner.formAction != FormAction.Delete && _frmDesigner.formAction != FormAction.View && _frmDesigner.formAction != FormAction.Copy)
                SetCurrentData();
        }

        void FrmMasterDetailCt_Load(object sender, EventArgs e)
        {
            _x = true;
            SetCurrentData();
            if (_frmDesigner.formAction == FormAction.Copy)
            {
                _data.CloneData();
                _bindingSource.Position = _data.DsData.Tables[0].Rows.Count - 1;
            }
            _frmDesigner.RefreshViewForLookup();
            _frmDesigner.RefreshFormulaDetail();
            foreach (ICDTData pl in _lstICDTData)
                pl.AddEvent();
        }

        private void SetCurrentData()
        {
            DataRowView drv = (_bindingSource.Current as DataRowView);
            if (drv != null)
                this._data.DrCurrentMaster = drv.Row;
            List<DataRow> drDetails = new List<DataRow>();

            for (int i = 0; i < gvMain.DataRowCount; i++)
                drDetails.Add(gvMain.GetDataRow(i));

            this._data.LstDrCurrentDetails = drDetails;
        }

        private void InitializeLayout()
        {
            this.SetFormCaption();
            this.AddLayoutControl();

            dataNavigatorMain.SendToBack();
        }

        private void AddLayoutControl()
        {
            LayoutControl lcMain;
            int fieldCount = _data.DsStruct.Tables[0].Rows.Count;
            if(fieldCount>12)
                lcMain = _frmDesigner.GenLayout3(ref gcMain, true);
            else if (fieldCount > 6)
                lcMain = _frmDesigner.GenLayout2(ref gcMain, true);
            else
                lcMain = _frmDesigner.GenLayout1(ref gcMain, true);
            gcMain.DataSource = _bindingSource;
            gcMain.DataMember = this._data.DrTable["TableName"].ToString();
            this.Controls.Add(lcMain);

            gvMain = gcMain.ViewCollection[0] as DevExpress.XtraGrid.Views.Grid.GridView;
            gvMain.OptionsView.ShowAutoFilterRow = false;
            gvMain.OptionsView.ShowGroupPanel = false;
            gvMain.OptionsView.ShowFooter = false;
            //gvMain.BestFitColumns();
        }

        private void dataNavigatorMain_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button == dataNavigatorMain.Buttons.EndEdit)
            {
                Config.NewKeyValue("Operation", "F12-Lưu");
                e.Handled = true;
                UpdateData();
            }
            if (e.Button == dataNavigatorMain.Buttons.CancelEdit)
            {
                Config.NewKeyValue("Operation", "ESC-Hủy");
                e.Handled = true;
                CancelUpdate();
            }
        }

        private void UpdateData()
        {
            _frmDesigner.FirstControl.Focus();
            gvMain.RefreshData();
            _bindingSource.EndEdit();
            //if (!_data.DataChanged)
            //    this.DialogResult = DialogResult.Cancel;
            //else
            //{
                DataAction dataAction = (_frmDesigner.formAction == FormAction.Edit) ? DataAction.Update : DataAction.Insert;
                _data.CheckRules(dataAction);
                if (dxErrorProviderMain.HasErrors || _data.DsData.Tables[1].HasErrors)
                {
                    XtraMessageBox.Show("Số liệu chưa hợp lệ, vui lòng kiểm tra lại trước khi lưu!");
                    return;
                }
                if (this._data.UpdateData(dataAction))
                {
                    _x = false;
                    this.DialogResult = DialogResult.OK;
                }
            //}
        }

        private void CancelUpdate()
        {
            _x = false;
            _bindingSource.EndEdit();
            if (!_data.DataChanged)
                this.DialogResult = DialogResult.Cancel;
            else
                if (XtraMessageBox.Show("Số liệu chưa được lưu, bạn có thật sự muốn quay ra?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _data.CancelUpdate();
                    _bindingSource.ResetBindings(false);
                    _bindingSource.DataSource = _data.DsData;
                    this.DialogResult = DialogResult.Cancel;
                }
        }

        private void FrmMasterDetailDt_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageUp:
                    if (e.Modifiers == Keys.Control)
                        dataNavigatorMain.Buttons.First.DoClick();
                    else
                        dataNavigatorMain.Buttons.Prev.DoClick();
                    break;
                case Keys.PageDown:
                    if (e.Modifiers == Keys.Control)
                        dataNavigatorMain.Buttons.Last.DoClick();
                    else
                        dataNavigatorMain.Buttons.Next.DoClick();
                    break;
                case Keys.Escape:
                    CancelUpdate();
                    break;
                case Keys.F12:
                    UpdateData();
                    break;
            }
        }

        private void FrmMasterDetailDt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_x)
                CancelUpdate();
            _data.Reset();
            _frmDesigner.formAction = FormAction.View;
            _frmDesigner.FirstControl.Focus();
        }
    }
}