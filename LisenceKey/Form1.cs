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


namespace LisenceKey
{
    public partial class Form1 : Form
    {   private string companeyName = "";
        public Form1()
        {
            InitializeComponent();
        }
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            companeyName = textEdit1.Text;
            string Product = textEdit4.Text;
            CPUid cpu=new CPUid(companeyName+Product);

            cpu.KeyString = string.Empty;
            cpu.MixString = textEdit2.Text;
            textEdit3.Text=cpu.GetKeyString();
        }
        private void textEdit2_EditValueChanged()
        {
        
        }
    }
}