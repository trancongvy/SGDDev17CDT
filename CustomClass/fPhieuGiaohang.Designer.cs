namespace CustomClass
{
    partial class fPhieuGiaohang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btFind = new DevExpress.XtraEditors.SimpleButton();
            this.btDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btNew = new DevExpress.XtraEditors.SimpleButton();
            this.gcDt = new DevExpress.XtraGrid.GridControl();
            this.gvDt = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.gcMt = new DevExpress.XtraGrid.GridControl();
            this.gvMt = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 492);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(981, 41);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btPrint);
            this.panelControl2.Controls.Add(this.btFind);
            this.panelControl2.Controls.Add(this.btDelete);
            this.panelControl2.Controls.Add(this.btEdit);
            this.panelControl2.Controls.Add(this.btNew);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(309, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(670, 37);
            this.panelControl2.TabIndex = 5;
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(573, 5);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(75, 23);
            this.btPrint.TabIndex = 9;
            this.btPrint.Text = "F7 - In";
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btFind
            // 
            this.btFind.Location = new System.Drawing.Point(470, 4);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(75, 23);
            this.btFind.TabIndex = 8;
            this.btFind.Text = "F6 - Tìm kiếm";
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(368, 5);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 23);
            this.btDelete.TabIndex = 7;
            this.btDelete.Text = "F4 - Xóa";
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btEdit
            // 
            this.btEdit.Location = new System.Drawing.Point(261, 5);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(75, 23);
            this.btEdit.TabIndex = 6;
            this.btEdit.Text = "F3 - Sửa";
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btNew
            // 
            this.btNew.Location = new System.Drawing.Point(148, 5);
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(75, 23);
            this.btNew.TabIndex = 5;
            this.btNew.Text = "F2 - Thêm";
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // gcDt
            // 
            this.gcDt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gcDt.EmbeddedNavigator.Name = "";
            this.gcDt.Location = new System.Drawing.Point(0, 292);
            this.gcDt.MainView = this.gvDt;
            this.gcDt.Name = "gcDt";
            this.gcDt.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCalcEdit2});
            this.gcDt.Size = new System.Drawing.Size(981, 200);
            this.gcDt.TabIndex = 1;
            this.gcDt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDt});
            // 
            // gvDt
            // 
            this.gvDt.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn3});
            this.gvDt.GridControl = this.gcDt;
            this.gvDt.Name = "gvDt";
            this.gvDt.OptionsBehavior.Editable = false;
            this.gvDt.OptionsView.ColumnAutoWidth = false;
            this.gvDt.OptionsView.EnableAppearanceEvenRow = true;
            this.gvDt.OptionsView.EnableAppearanceOddRow = true;
            this.gvDt.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Vật tư";
            this.gridColumn1.FieldName = "MaVT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 139;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Số lượng";
            this.gridColumn2.ColumnEdit = this.repositoryItemCalcEdit2;
            this.gridColumn2.FieldName = "Soluong2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 87;
            // 
            // repositoryItemCalcEdit2
            // 
            this.repositoryItemCalcEdit2.AutoHeight = false;
            this.repositoryItemCalcEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit2.DisplayFormat.FormatString = "### ### ### ##0";
            this.repositoryItemCalcEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemCalcEdit2.EditFormat.FormatString = "### ### ### ##0";
            this.repositoryItemCalcEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemCalcEdit2.Mask.EditMask = "### ### ### ##0";
            this.repositoryItemCalcEdit2.Name = "repositoryItemCalcEdit2";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "ĐVT";
            this.gridColumn4.FieldName = "MaDVT2";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 70;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Đơn giá";
            this.gridColumn5.ColumnEdit = this.repositoryItemCalcEdit2;
            this.gridColumn5.FieldName = "DonGia2";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 91;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Thành tiền";
            this.gridColumn3.ColumnEdit = this.repositoryItemCalcEdit2;
            this.gridColumn3.FieldName = "TienCC";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 140;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(0, 286);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(981, 6);
            this.splitterControl1.TabIndex = 2;
            this.splitterControl1.TabStop = false;
            // 
            // gcMt
            // 
            this.gcMt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMt.EmbeddedNavigator.Name = "";
            this.gcMt.Location = new System.Drawing.Point(0, 0);
            this.gcMt.MainView = this.gvMt;
            this.gcMt.Name = "gcMt";
            this.gcMt.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCalcEdit1,
            this.repositoryItemDateEdit1});
            this.gcMt.Size = new System.Drawing.Size(981, 286);
            this.gcMt.TabIndex = 3;
            this.gcMt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMt});
            // 
            // gvMt
            // 
            this.gvMt.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14});
            this.gvMt.CustomizationFormBounds = new System.Drawing.Rectangle(1132, 474, 216, 178);
            this.gvMt.GridControl = this.gcMt;
            this.gvMt.Name = "gvMt";
            this.gvMt.OptionsBehavior.Editable = false;
            this.gvMt.OptionsSelection.MultiSelect = true;
            this.gvMt.OptionsView.ColumnAutoWidth = false;
            this.gvMt.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMt.OptionsView.EnableAppearanceOddRow = true;
            this.gvMt.OptionsView.ShowAutoFilterRow = true;
            this.gvMt.OptionsView.ShowDetailButtons = false;
            this.gvMt.OptionsView.ShowFooter = true;
            this.gvMt.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Số ct";
            this.gridColumn6.FieldName = "SoCt";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Ngày chứng từ";
            this.gridColumn7.ColumnEdit = this.repositoryItemDateEdit1;
            this.gridColumn7.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn7.FieldName = "NgayCt";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 106;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.repositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy";
            this.repositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.Mask.EditMask = "dd/MM/yyyy";
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Số xe";
            this.gridColumn8.FieldName = "MaXe";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Khách hàng";
            this.gridColumn9.FieldName = "MaKH";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            this.gridColumn9.Width = 119;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Ông bà";
            this.gridColumn10.FieldName = "OngBa";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            this.gridColumn10.Width = 176;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Tiền hàng";
            this.gridColumn11.ColumnEdit = this.repositoryItemCalcEdit1;
            this.gridColumn11.FieldName = "TTienH";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.SummaryItem.DisplayFormat = "{0:### ### ### ##0}";
            this.gridColumn11.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn11.SummaryItem.Tag = 0;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 5;
            this.gridColumn11.Width = 81;
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.DisplayFormat.FormatString = "### ### ### ##0";
            this.repositoryItemCalcEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemCalcEdit1.EditFormat.FormatString = "### ### ### ##0";
            this.repositoryItemCalcEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemCalcEdit1.Mask.EditMask = "### ### ### ##0";
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Tiền phí";
            this.gridColumn12.ColumnEdit = this.repositoryItemCalcEdit1;
            this.gridColumn12.FieldName = "TThue";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.SummaryItem.DisplayFormat = "{0:### ### ### ##0}";
            this.gridColumn12.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Tiền thuế";
            this.gridColumn13.ColumnEdit = this.repositoryItemCalcEdit1;
            this.gridColumn13.FieldName = "TPhi";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.SummaryItem.DisplayFormat = "{0:### ### ### ##0}";
            this.gridColumn13.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 7;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Tổng tiền";
            this.gridColumn14.ColumnEdit = this.repositoryItemCalcEdit1;
            this.gridColumn14.FieldName = "TTien";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.SummaryItem.DisplayFormat = "{0:### ### ### ##0}";
            this.gridColumn14.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 8;
            // 
            // fPhieuGiaohang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 533);
            this.Controls.Add(this.gcMt);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.gcDt);
            this.Controls.Add(this.panelControl1);
            this.KeyPreview = true;
            this.Name = "fPhieuGiaohang";
            this.Text = "Phiếu giao hàng công nợ";
            this.Load += new System.EventHandler(this.fPhieuGiaohang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcDt;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDt;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraGrid.GridControl gcMt;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMt;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btPrint;
        private DevExpress.XtraEditors.SimpleButton btFind;
        private DevExpress.XtraEditors.SimpleButton btDelete;
        private DevExpress.XtraEditors.SimpleButton btEdit;
        private DevExpress.XtraEditors.SimpleButton btNew;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
    }
}