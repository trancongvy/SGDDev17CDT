using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using DevExpress.XtraEditors;
using CDTControl;
using DevExpress.XtraLayout.Utils;
using CDTLib;
namespace CDTSystem
{
    public partial class CreateData : DevExpress.XtraEditors.XtraForm
    {
        string _ver;
        private bool ismorong = false;
        private CheckEdit cEis2005;

        public CreateData(string Ver)
        {
            this._ver = Ver;
            InitializeComponent();
            radioGroupType.SelectedIndex = 0;
            radioGroupCnnType.SelectedIndex = 0;

        }

        private void radioGroupCnnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textEditUser.Enabled = this.radioGroupCnnType.SelectedIndex == 0;
            this.textEditPassword.Enabled = this.radioGroupCnnType.SelectedIndex == 0;
            if (this.radioGroupCnnType.SelectedIndex == 1)
            {
                this.textEditUser.EditValue = "sa";
                this.textEditPassword.EditValue = "sa";
            }
        }

        private void radioGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroupType.SelectedIndex == 0)
            {
                this.textEditServer.EditValue = SystemInformation.ComputerName + "\\SQLSGD2005";
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!this.ismorong)
            {
                this.layoutControlItem3.Visibility = LayoutVisibility.Always;
                this.layoutControlItem4.Visibility = LayoutVisibility.Always;
                this.layoutControlItem7.Visibility = LayoutVisibility.Always;
                this.layoutControlItem8.Visibility = LayoutVisibility.Always;
                this.layoutControlItem9.Visibility = LayoutVisibility.Always;
                this.ismorong = true;
                base.Height = 420;
            }
            else
            {
                this.layoutControlItem3.Visibility = LayoutVisibility.Never;
                this.layoutControlItem4.Visibility = LayoutVisibility.Never;
                this.layoutControlItem7.Visibility = LayoutVisibility.Never;
                this.layoutControlItem8.Visibility = LayoutVisibility.Never;
                this.layoutControlItem9.Visibility = LayoutVisibility.Never;
                this.ismorong = false;
                base.Height = 220;
            }
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            if (this.dxErrorProviderMain.HasErrors)
            {
                XtraMessageBox.Show("Thông tin chưa hợp lệ, vui lòng kiểm tra lại!");
            }
            else
            {
                this._ver = this.txtCDT.Text;
                string text = this.textEditServer.Text;
                string textRemote = this.txtRemoteServer.Text;
                DataMaintain maintain = new DataMaintain(text, textRemote, this.radioGroupCnnType.SelectedIndex, this.textEditUser.Text, this.textEditPassword.Text);

                maintain.isServer2005 = this.cEis2005.Checked;

                this.layoutControlItemProgress.Visibility = LayoutVisibility.Always;
                this.layoutControl1.Refresh();
                bool flag = false;
                if (this.radioGroupType.SelectedIndex == 1)
                {
                    flag = maintain.ClientExecute(this._ver);
                    string H_KEY = Config.GetValue("H_KEY").ToString();
                    if (flag)
                    {

                    }
                    if (flag && ckUpdateRemote.Checked)
                        maintain.UpdateRemoteServer(txtRemoteServer.Text,this._ver);
                    if (flag && CkUpdateLocal.Checked)
                        maintain.UpdateLocalServer(text, this._ver);
                }
                else
                {
                    flag = maintain.ServerExecute(Application.StartupPath, this._ver);
                }
                if (flag)
                {
                    base.DialogResult = DialogResult.OK;
                }
                else
                {
                    XtraMessageBox.Show("Có lỗi trong quá trình tạo số liệu, vui lòng kiểm tra lại!");
                    this.layoutControlItemProgress.Visibility = LayoutVisibility.Never;
                }
            }
        }

        private void textEditServer_EditValueChanged(object sender, EventArgs e)
        {
            if (this.textEditServer.Text == string.Empty)
            {
                this.dxErrorProviderMain.SetError(this.textEditServer, "Phải nhập");
            }
            else
            {
                this.dxErrorProviderMain.SetError(this.textEditServer, string.Empty);
            }
        }

        private void textEditUser_EditValueChanged(object sender, EventArgs e)
        {
            if ((this.radioGroupCnnType.SelectedIndex == 1) && (this.textEditUser.Text == string.Empty))
            {
                this.dxErrorProviderMain.SetError(this.textEditUser, "Phải nhập");
            }
            else
            {
                this.dxErrorProviderMain.SetError(this.textEditUser, string.Empty);
            }
        }

        private void CreateData_Load(object sender, EventArgs e)
        {
            txtCDT.Text = this._ver.Replace("CBA", "CDT");
        }

        private void txtCDT_EditValueChanged(object sender, EventArgs e)
        {
            //txtCDT.Text = this._ver.Replace("CBA", "CDT");
        }

    }
}