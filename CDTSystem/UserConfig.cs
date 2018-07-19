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
    public partial class UserConfig : DevExpress.XtraEditors.XtraForm
    {
        private SysConfig _sysConfig = new SysConfig();
        public bool IsShow = false;
        public UserConfig()
        {
            InitializeComponent();
            _sysConfig.GetUserConfig();
            if (_sysConfig.DsStartConfig != null)
                IsShow = _sysConfig.DsStartConfig.Tables[0].Rows.Count > 0;
            if (Config.GetValue("Language").ToString() == "1")
                DevLocalizer.Translate(this);
        }

        private void StartConfig_Load(object sender, EventArgs e)
        {
            if (_sysConfig.DsStartConfig != null)
                gcMain.DataSource = _sysConfig.DsStartConfig.Tables[0];
        }

        private void StartConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void simpleButtonChapNhan_Click(object sender, EventArgs e)
        {
            _sysConfig.UpdateStartConfig();
            this.Close();
        }

        private void simpleButtonQuayRa_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}