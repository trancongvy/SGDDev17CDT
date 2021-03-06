using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CDTLib;
using Microsoft.Win32;
using CDTControl.CDTControl;

namespace CDTSystem
{
    public partial class RegisterF : Form
    {
        string H_KEY;
        CPUid cpu; 
        public RegisterF()
        {
            InitializeComponent();            
            H_KEY = Config.GetValue("H_KEY").ToString();
        }
        private string _ProductName;
        public string producName
        {
            get{return _ProductName;}
            set
            {
                _ProductName = value;
                textProduct.Text = _ProductName;
            }
        }

        int sl = 0;



        private void RegisterF_Load(object sender, EventArgs e)
        {
            
        }



        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            CDTControl.Log log = new CDTControl.Log();
            if (log.Check(tUser.Text, tPass.Text))
            {
                cpu = new CPUid(textCompanyName.Text + _ProductName + "SGDEMTOnline");
                textEditMaskcode.Text = cpu.MixString;
                string key = cpu.KeyString;
                string keyGet = log.log(textCompanyName.Text, _ProductName, key, tUser.Text, tPass.Text);
                if (keyGet == key)
                {
                    textEditValue.Text = key;
                    simpleButtonRegister.Enabled = true;
                    Registry.SetValue(H_KEY, "CompanyName", textCompanyName.Text);
                    Registry.SetValue(H_KEY, "RegisterNumber", key);
                    Registry.SetValue(H_KEY, "isDemo", 0);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    textEditValue.Text = keyGet;
                }

            }
            else
            {
                textEditValue.Text = "User hoặc pass chưa đúng,liên hệ SGDSoft để lấy key";
                _ProductName = textProduct.Text.Trim().ToUpper();
                cpu = new CPUid(textCompanyName.Text + _ProductName + "SGDEMTOnline");
                textEditMaskcode.Text = cpu.MixString;
            }
        }

        private void simpleButtonRegister_Click(object sender, EventArgs e)
        {
            _ProductName = textProduct.Text.Trim().ToUpper();
            cpu = new CPUid(textCompanyName.Text + _ProductName + "SGDEMTOnline");
            textEditMaskcode.Text = cpu.MixString;
            if (textCompanyName.Text == "SGD")
                textEditValue.Text = cpu.KeyString;
            cpu.KeyString = string.Empty;
            if (cpu == null) return;
            string key = cpu.GetKeyString();
            Registry.SetValue(H_KEY, "Structtmp", key);
            if (textEditValue.Text == key)
            {
                Registry.SetValue(H_KEY, "CompanyName", textCompanyName.Text);
                Registry.SetValue(H_KEY, "RegisterNumber", textEditValue.Text);
                Registry.SetValue(H_KEY, "isDemo", 0);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (sl == 10)
                {
                    Registry.SetValue(H_KEY, "CompanyName", textCompanyName.Text);
                    Registry.SetValue(H_KEY, "RegisterNumber", key);
                    Registry.SetValue(H_KEY, "isDemo", 0);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    sl += 1;
                    MessageBox.Show("Vui lòng liên hệ SGD soft để được đăng ký sử dụng!");
                }
            }
        }
    }
}