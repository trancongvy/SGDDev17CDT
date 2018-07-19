using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using System.Net.Sockets;

namespace CDTSystem
{
    public partial class NetSupport : DevExpress.XtraEditors.XtraForm
    {
        string Myip;
        public NetSupport()
        {
            InitializeComponent();
            WebClient w = new WebClient();
          Myip  = w.DownloadString("http://whatismyip.org/");
          txtMyIp.Text = Myip;
          int widthScreen = Screen.PrimaryScreen.WorkingArea.Width;
          int heightScreen = Screen.PrimaryScreen.WorkingArea.Height;
        }
    }
}