using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CDT
{
    public partial class Startup : DevExpress.XtraEditors.XtraForm
    {
        public Startup()
        {
            InitializeComponent();
        }

        private void Startup_FormClosed(object sender, FormClosedEventArgs e)
        {
            webBrowser.Dispose();
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser.Document.Cookie == null)
                this.Close();
        }
    }
}