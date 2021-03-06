namespace FormFactory
{
    partial class FrmSingleDt
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
            this.components = new System.ComponentModel.Container();
            this.dxErrorProviderMain = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.dataNavigatorMain = new DevExpress.XtraEditors.DataNavigator();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btSaveLayout = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.pMInsetToDetail = new DevExpress.XtraBars.PopupMenu(this.components);
            this.oFD = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this._bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMInsetToDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dxErrorProviderMain
            // 
            this.dxErrorProviderMain.ContainerControl = this;
            // 
            // dataNavigatorMain
            // 
            this.dataNavigatorMain.Buttons.Append.Visible = false;
            this.dataNavigatorMain.Buttons.CancelEdit.Hint = "ESC - Bỏ qua";
            this.dataNavigatorMain.Buttons.EndEdit.Hint = "F12 - Lưu";
            this.dataNavigatorMain.Buttons.First.Hint = "Ctrl+PageUp - Mục đầu tiên";
            this.dataNavigatorMain.Buttons.Last.Hint = "Ctrl+PageDown - Mục sau cùng";
            this.dataNavigatorMain.Buttons.Next.Hint = "PageDown - Mục tiếp theo";
            this.dataNavigatorMain.Buttons.NextPage.Visible = false;
            this.dataNavigatorMain.Buttons.Prev.Hint = "PageUp - Mục trước";
            this.dataNavigatorMain.Buttons.PrevPage.Visible = false;
            this.dataNavigatorMain.Buttons.Remove.Visible = false;
            this.dataNavigatorMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataNavigatorMain.Location = new System.Drawing.Point(0, 458);
            this.dataNavigatorMain.Name = "dataNavigatorMain";
            this.dataNavigatorMain.ShowToolTips = true;
            this.dataNavigatorMain.Size = new System.Drawing.Size(708, 24);
            this.dataNavigatorMain.TabIndex = 3;
            this.dataNavigatorMain.Text = "dataNavigator1";
            this.dataNavigatorMain.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.dataNavigatorMain.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.dataNavigatorMain_ButtonClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btSaveLayout,
            this.barButtonItem1});
            this.barManager1.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(708, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 482);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(708, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 482);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(708, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 482);
            // 
            // btSaveLayout
            // 
            this.btSaveLayout.Caption = "Save Layout";
            this.btSaveLayout.Id = 3;
            this.btSaveLayout.Name = "btSaveLayout";
            this.btSaveLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btSaveLayout_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Reload Layout from file";
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // pMInsetToDetail
            // 
            this.pMInsetToDetail.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btSaveLayout),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.pMInsetToDetail.Manager = this.barManager1;
            this.pMInsetToDetail.Name = "pMInsetToDetail";
            // 
            // oFD
            // 
            this.oFD.FileName = "openFileDialog1";
            this.oFD.Filter = "XML |*.xml";
            // 
            // FrmSingleDt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 482);
            this.Controls.Add(this.dataNavigatorMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.KeyPreview = true;
            this.Name = "FrmSingleDt";
            this.Text = "FrmSingleDt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSingleDt_FormClosing);
            this.Load += new System.EventHandler(this.FrmSingleDt_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSingleDt_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this._bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMInsetToDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProviderMain;
        private DevExpress.XtraEditors.DataNavigator dataNavigatorMain;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem btSaveLayout;
        private DevExpress.XtraBars.PopupMenu pMInsetToDetail;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private System.Windows.Forms.OpenFileDialog oFD;
    }
}