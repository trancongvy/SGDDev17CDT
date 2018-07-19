using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
namespace DevControls
{
    public partial class fileContener : TextEdit
    {
        public fileContener(DataRow drf)
        {
            InitializeComponent();
            simpleButton2.BringToFront();
            simpleButton1.BringToFront();
            drField = drf;
        }
        public byte[] data;
        public DataRow drField;
        public bool updateNew = false;
        public event System.EventHandler LoadnewData;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFdg = new OpenFileDialog();
            OFdg.ShowDialog();
            if (OFdg.FileName != null)
            {
                this.Text = OFdg.FileName;

                FileStream fs = new FileStream(this.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                long numBytes = new FileInfo(this.Text).Length;
               data = br.ReadBytes((int)numBytes);
               updateNew = true;
               this.RaiseLoadnewData(this, e);
            }
        }

        private void RaiseLoadnewData(object sender, EventArgs e)
        {
            LoadnewData(this, e);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sFdg=new SaveFileDialog();
            if (this.Text != null) sFdg.FileName = this.Text;
            sFdg.ShowDialog();
            if (data != null && sFdg.FileName!=string.Empty)
            {
                try
                {
                    System.IO.FileStream fs = new System.IO.FileStream(sFdg.FileName, FileMode.Create, FileAccess.Write);
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                }
                catch 
                { }
            }
        }


    }
}
