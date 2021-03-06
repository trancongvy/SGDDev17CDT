using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList;
using FormFactory;
using CDTControl;
using ReportFactory;
using DataFactory;
using CDTLib;

namespace CDTSystem
{
    public partial class CheckData : CDTForm
    {
        private SysTable _sysTable = new SysTable();
        private bool _isUpdate;
        public CheckData(bool isUpdate)
        {
            InitializeComponent();
            _isUpdate = isUpdate;
            if (!_isUpdate)
                this.Text = "Xem lưu vết số liệu";
            if (Config.GetValue("Language").ToString() == "1")
                DevLocalizer.Translate(this);
        }

        /// <summary>
        /// Lấy dữ liệu từ bảng sysTable đưa lên listbox
        /// </summary>
        private void FrmTest_Load(object sender, EventArgs e)
        {
            if (_isUpdate)
                _sysTable.GetAllForPackage();
            else
                _sysTable.GetUserTableForPackage();
            if (_sysTable.DtTable != null)
            {
                lbcTable.DataSource = _sysTable.DtTable;
                lbcTable.DisplayMember = Config.GetValue("Language").ToString() == "0" ? "DienGiai" : "DienGiai2";
                lbcTable.ValueMember = "TableName";
            }
            layoutControl1.Visible = false;
            lciUpdate.Visibility = _isUpdate ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lciViewHistory.Visibility = _isUpdate ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }

        /// <summary>
        /// Mở bảng trong listbox theo Type mặc định
        /// </summary>
        private void lbcTenForm_DoubleClick(object sender, EventArgs e)
        {
            gcMain = new GridControl();
            gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            tlMain = new TreeList();
            gvMain.OptionsBehavior.Editable = _isUpdate;
            tlMain.OptionsBehavior.Editable = _isUpdate;
            _data = DataFactory.DataFactory.Create(DataType.Single, _sysTable.DtTable.Rows[lbcTable.SelectedIndex]);

            if (_data.DsData == null)
                _data.GetData(); 
            _bindingSource = new BindingSource();
            _frmDesigner = new FormDesigner(_data, _bindingSource);

            gcMain = _frmDesigner.GenGridControl(_data.DsStruct.Tables[0], true, DockStyle.Fill);
            gvMain = gcMain.ViewCollection[0] as DevExpress.XtraGrid.Views.Grid.GridView;
            this.Controls.Add(gcMain);

            if (_data.DrTable["ParentPk"].ToString() == string.Empty)
            {
                layoutControlItemTreeList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            layoutControl1.Visible = true;
            gcMain.BringToFront();
            layoutControl1.SendToBack();
            if (_isUpdate)
                GetRelativeFunction();

            DisplayData();
        }

        private void GetRelativeFunction()
        {
            DataTable dtCNLQ = _data.GetRelativeData();
            if (dtCNLQ == null || dtCNLQ.Rows.Count == 0)
            {
                layoutControlItemCNLQ.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                return;
            }
            layoutControlItemCNLQ.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lookUpEditCNLQ.Properties.DataSource = dtCNLQ;
            lookUpEditCNLQ.Properties.Columns[0].FieldName = Config.GetValue("Language").ToString() == "0" ? "DienGiai" : "DienGiai2";
            lookUpEditCNLQ.Properties.Columns[0].Caption = string.Empty;
            lookUpEditCNLQ.Properties.DisplayMember = Config.GetValue("Language").ToString() == "0" ? "DienGiai" : "DienGiai2";
        }

        private void lookUpEditCNLQ_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.OK)
            {
                LookUpEdit lue = sender as LookUpEdit;
                if (lue.ItemIndex < 0)
                    return;
                if (gvMain.SelectedRowsCount == 0)
                {
                    XtraMessageBox.Show("Vui lòng chọn một đối tượng trên danh sách để xem thông tin chi tiết!");
                    return;
                }
                string pkName = _data.DrTable["Pk"].ToString();
                string pkValue = gvMain.GetFocusedRowCellValue(pkName).ToString();
                if (pkValue == string.Empty)
                    return;
                DataTable dtTable = lue.Properties.DataSource as DataTable;
                DataRow dr = dtTable.Rows[lue.ItemIndex];

                CDTData data1 = DataFactory.DataFactory.Create(DataType.Single, dr);
                data1.Condition = pkName + " = '" + pkValue + "'";
                //FrmSingle frm = new FrmSingle(data1);
                CDTForm frm = FormFactory.FormFactory.Create(FormType.Single, data1);
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// Sự kiện Enter trên listbox
        /// </summary>
        private void lbcTenForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                lbcTenForm_DoubleClick(lbcTable, (EventArgs)e);
        }

        private void DisplayData()
        {
            if (_data.DsData != null)
            {
                _bindingSource.DataSource = _data.DsData.Tables[0];
                _frmDesigner.bindingSource = _bindingSource;
                gcMain.DataSource = _bindingSource;
                tlMain.DataSource = _bindingSource;
                gvMain.BestFitColumns();
            }
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sự kiện cập nhật toàn bộ bảng số liệu (Dùng cho Type1: Cập nhật trên lưới)
        /// </summary>
        private void simpleButtonUpdate_Click(object sender, EventArgs e)
        {
            gvMain.RefreshData();
            if (_data.DsData.Tables[0].HasErrors)
            {
                XtraMessageBox.Show("Số liệu chưa hợp lệ, vui lòng kiểm tra lại trước khi lưu!");
                return;
            }
            if (!_data.UpdateData())
                DisplayData();
            else
            {
                XtraMessageBox.Show("Hoàn tất cập nhật bảng số liệu");
            }
        }

        private void checkEditTreeView_CheckedChanged(object sender, EventArgs e)
        {
            base.CheckTreeList();
        }

        private void simpleButtonPreview_Click(object sender, EventArgs e)
        {
            if (gcMain.Visible)
                gcMain.ShowPrintPreview();
            else
                tlMain.ShowPrintPreview();
        }

        private void simpleButtonSearch_Click(object sender, EventArgs e)
        {
            gvMain.ShowFilterEditor(gvMain.Columns[0]);
            if (gvMain.RowFilter != string.Empty)
            {
                SqlSearching sSearch = new SqlSearching();
                string sql = sSearch.GenSqlFromGridFilter(gvMain.RowFilter);
                _data.Condition = sql;
                _data.GetData();
                DisplayData();
                gvMain.ClearColumnsFilter();
                XtraMessageBox.Show("Kết quả tìm kiếm: " + gvMain.DataRowCount.ToString() + " mục số liệu");
            }
        }

        private void FrmCheckData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void simpleButtonTrace_Click(object sender, EventArgs e)
        {
            string sysTableID = _data.DrTable["sysTableID"].ToString();
            string pk = _data.DrTable["Pk"].ToString();
            string pkValue = gvMain.GetFocusedRowCellValue(pk).ToString();
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            DataReport data = new DataReport("83", true);
            data.PsString = "h.sysPackageID = " + sysPackageID + " and h.pkValue = '" + pkValue + "' and h.sysMenuID in (select sysMenuID from sysMenu where sysTableID = " + sysTableID + ")";
            ReportFactory.ReportPreview rp = new ReportFactory.ReportPreview(data);
            rp.ShowDialog();
        }

    }
}