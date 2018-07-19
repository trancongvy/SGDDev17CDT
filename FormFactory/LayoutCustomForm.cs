using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Customization;
using DataFactory;
using System.IO;
namespace FormFactory
{
    public partial class LayoutCustomForm : UserCustomizationForm
    {
        public LayoutCustomForm()
        {
            InitializeComponent();
            this.Disposed += LayoutCustomForm_Disposed;
        }

        void LayoutCustomForm_Disposed(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save and Upload layout?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    ((sender as DevExpress.XtraLayout.Customization.UserCustomizationForm).Tag as LayoutControl).SaveLayoutToStream(ms);
                    _data.DrTable["FileLayout"] = ms.ToArray();
                    saveLayoutToData();
                }
                catch { }
            }
        }
        LayoutControl _mainLayout;
        CDTData _data;
        public CDTData Data
        {
            get { return _data; }
            set { _data = value; }
        }
        public LayoutControl MainLayout
        {
            get
            {
                return _mainLayout;
            }
            set
            {
                _mainLayout = value;
            }
        }
        public void saveLayoutToData()
        {
            _data.updateLayoutFile(_data.DrTable);
        }
    }
}
