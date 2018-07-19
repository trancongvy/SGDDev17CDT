using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ScriptGeneration
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            PackageAdded pa = new PackageAdded(textEditCnnSource.Text, textEditPackage.Text);
            string sql = pa.GenScript();
            string fileName = textEditPackage.Text + ".dat";
            if (File.Exists(fileName))
                File.Delete(fileName);
            File.AppendAllText(fileName, sql, Encoding.Unicode);
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}