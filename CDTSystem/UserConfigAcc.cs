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
using System.Collections;
using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography;
namespace CDTSystem
{
    public partial class UserConfigAcc : DevExpress.XtraEditors.XtraForm
    {
        private SysConfig _sysConfig = new SysConfig();
        public bool IsShow = true;
        DataTable tb = new DataTable();
       private Boolean begin = true;
        public UserConfigAcc()
        {
            InitializeComponent();
            _sysConfig.GetUserConfig();
            if (_sysConfig.DsStartConfig != null)
                IsShow = _sysConfig.DsStartConfig.Tables[0].Rows.Count > 0;
        }
        //[HostProtectionAttribute(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
    
        private void UserConfigAcc_Load(object sender, EventArgs e)
        {
            if (_sysConfig.DsStartConfig.Tables.Count < 1) return;
            tb = _sysConfig.DsStartConfig.Tables[0];
            foreach (DataRow dr in tb.Rows)
            {
                try
                {
                    Config.NewKeyValue(dr["_Key"], dr["_Value"]);
                }
                catch (Exception ex)
                {
                }
            }
            groupControl1.Text = Config.GetValue("TenCongTy").ToString();
            tDiachiCty.Text = Config.GetValue("DiaChiCty").ToString();
            tMST.Text = Config.GetValue("MaSoThue").ToString();
            tGiamdoc.Text = Config.GetValue("GiamDoc").ToString();
            tKTT.Text = Config.GetValue("KeToanTruong").ToString();
            tNguoilap.Text = Config.GetValue("NguoiLap").ToString();
            tThukho.Text = Config.GetValue("Thukho").ToString();
            tThuQuy.Text = Config.GetValue("ThuQuy").ToString();
            //
            sNamLV.EditValue = int.Parse(Config.GetValue("NamLamViec").ToString());
            dNgayct1.EditValue = DateTime.Parse(Config.GetValue("Khoasolieu").ToString()).AddDays(1);
            dNgayct2.EditValue = DateTime.Parse(Config.GetValue("Khoasolieu1").ToString());
            mQuyetdinh.Text = Config.GetValue("QuyetDinh").ToString();
            //
            tReportPath.Text = Config.GetValue("DuongDanBaoCao").ToString();
            tBackupPath.Text = Config.GetValue("BackupPath").ToString();
            tPrinterName.Text = Config.GetValue("PrinterName").ToString();
            tCert.Text = Config.GetValue("PublicKey").ToString();
            begin = false;
            if (Config.GetValue("Language").ToString() == "1")
            {
                DevLocalizer.Translate(this);                
            }


        }

        private void tDiachiCty_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("DiaChiCty", tDiachiCty.Text);
        }

        private void tMST_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("MaSoThue", tMST.Text);
        }

        private void tGiamdoc_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("GiamDoc", tGiamdoc.Text);
        }

        private void tKTT_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("KeToanTruong", tKTT.Text);
        }

        private void tNguoilap_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("NguoiLap", tNguoilap.Text);
        }

        private void tThuQuy_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("ThuQuy", tThuQuy.Text);
        }

        private void tThukho_EditValueChanged(object sender, EventArgs e)
       {
            Config.NewKeyValue("Thukho", tThukho.Text);
        }

        private void sNamLV_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("NamLamViec", sNamLV.EditValue.ToString());
            if (!begin)
            {
                dNgayct1.EditValue = DateTime.Parse("01/01/" + Config.GetValue("NamLamViec").ToString());
                dNgayct2.EditValue = DateTime.Parse("12/31/" + Config.GetValue("NamLamViec").ToString());
            }

        }

        private void dNgayct1_EditValueChanged(object sender, EventArgs e)
        {
            if(!begin)
            Config.NewKeyValue("Khoasolieu", DateTime.Parse(dNgayct1.EditValue.ToString()).AddDays(-1).ToShortDateString());
        }

        private void dNgayct2_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("Khoasolieu1", DateTime.Parse(dNgayct2.EditValue.ToString()).ToShortDateString());
        }

        private void mQuyetdinh_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("QuyetDinh", mQuyetdinh.Text);
        }

        private void cBrowReport_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != null)
            {
                tReportPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void cBrowBackup_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != null)
            {
                tBackupPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void tReportPath_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("DuongDanBaoCao", tReportPath.Text);
        }

        private void tBackupPath_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("BackupPath" ,tBackupPath.Text) ;
        }

        private void cBrowPrint_Click(object sender, EventArgs e)
        {
            PrinterListDialog dpt = new PrinterListDialog();
            dpt.ShowDialog();
            if (dpt.printerName != string.Empty)
                tPrinterName.Text = dpt.printerName;
        }

        private void tPrinterName_EditValueChanged(object sender, EventArgs e)
        {
            Config.NewKeyValue("PrinterName", tPrinterName.Text);            
        }

        private void cUpdate_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in tb.Rows)
            {
                try
                {
                    if (Config.Variables.Contains(dr["_Key"].ToString()))
                    {
                        dr["_Value"] = Config.GetValue(dr["_Key"].ToString());
                    }
                }
                catch
                {
                }
            }
            if (bool.Parse(Config.GetValue("Admin").ToString()))
            {
                _sysConfig.UpdateStartConfig();
                MessageBox.Show("UpdateComplete!");
            }
        }

        private void btGetken_Click(object sender, EventArgs e)
        {
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly);

                // Place all certificates in an X509Certificate2Collection object.
                X509Certificate2Collection certCollection = store.Certificates;
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                if (currentCerts.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy chữ ký số");
                    return;
                }
                else
                {
                    DataTable tb = new DataTable();
                    DataColumn c1 = new DataColumn("TenCongty", typeof(string));
                    tb.Columns.Add(c1);
                    DataColumn c2 = new DataColumn("Ngayhethan", typeof(DateTime));
                    tb.Columns.Add(c2);
                    foreach (X509Certificate2 cert in currentCerts)
                    {
                        DataRow dr = tb.NewRow();
                        string[] lInfo = cert.Subject.Split(",".ToCharArray());
                        foreach (string Info in lInfo)
                        {
                            if (Info.Contains("CN="))
                                dr["TenCongty"] = Info.Replace("CN=", "");
                        }
                        dr["Ngayhethan"] = cert.NotAfter;
                        tb.Rows.Add(dr);
                    }
                    selectCert sCert = new selectCert(tb);
                    sCert.ShowDialog();
                    if (sCert.SIndex != -1)
                    {
                        string value; //= Encoding.UTF8.GetString(currentCerts[sCert.SIndex].PublicKey.EncodedKeyValue.RawData);
                        value = Convert.ToBase64String(currentCerts[sCert.SIndex].PublicKey.EncodedKeyValue.RawData);
                        Config.NewKeyValue("PublicKey", value);
                        tCert.Text = value;
                    }
                }
            }
            finally
            {
                store.Close();
            }
        }
    }
}