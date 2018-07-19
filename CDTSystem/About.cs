using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using FormFactory;
using CDTControl;
namespace CDTSystem
{
    public partial class About : DevExpress.XtraEditors.XtraForm
    {
        public About()
        {
            InitializeComponent();
            object packageName = Config.GetValue("PackageName");
            if (packageName != null)
                textEditPackageName.Text = packageName.ToString();
            object version = Config.GetValue("Version");
            if (version != null)
                textEditVersion.Text = version.ToString();
            object copyright = Config.GetValue("Copyright");
            if (copyright != null)
                textEditCopyright.Text = copyright.ToString();
            object vendor = Config.GetValue("Vendor");
            if (vendor != null)
                textEditVendor.Text = vendor.ToString();
            object vendorInfo = Config.GetValue("VendorInfo");
            if (vendorInfo != null)
                memoEditVendorInfo.Text = vendorInfo.ToString();
            object customer = Config.GetValue("CompanyName");
            if (customer != null)
                textEditCustomer.Text = customer.ToString();
            if (Config.GetValue("Language").ToString() == "1")
                DevLocalizer.Translate(this);
        }

        private void About_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}