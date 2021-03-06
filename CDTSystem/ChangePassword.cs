using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTControl;
using CDTLib;
using FormFactory;

namespace CDTSystem
{
    public partial class ChangePassword : DevExpress.XtraEditors.XtraForm
    {
        public ChangePassword()
        {
            InitializeComponent();
            if (Config.GetValue("Language").ToString() == "1")
                DevLocalizer.Translate(this);
        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            string sysUserID = Config.GetValue("sysUserID").ToString();
            SysUser su = new SysUser(sysUserID);
            if (!su.ValidUser(textEditOldPassword.Text))
            {
                XtraMessageBox.Show("Mật khẩu cũ chưa đúng, vui lòng kiểm tra lại!");
                return;
            }
            if (textEditNewPassword.Text != textEditNewPassword2.Text)
            {
                XtraMessageBox.Show("Mật khẩu mới và mật khẩu mới nhập lại không giống nhau, vui lòng kiểm tra lại!");
                return;
            }
            su.ChangePassword(textEditNewPassword.Text);
            if (su.ChangePassword(textEditNewPassword.Text))
            {
                XtraMessageBox.Show("Đổi mật khẩu thành công");
                this.Close();
            }
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}