using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using FormFactory;
using DataFactory;
namespace FO
{
    public partial class fCheckin : DevExpress.XtraEditors.XtraForm
    {
        public fCheckin()
        {
            InitializeComponent();
        }
        private Database _dbData = Database.NewCustomDatabase("server = Vytc1; database = CBA44; user = sa; pwd = sa");
        private Database _dbStruct = Database.NewCustomDatabase("server = vytc1; database = CDT44; user = sa; pwd = sa");

        private void fCheckin_Load(object sender, EventArgs e)
        {
            FormDesigner f = new FormDesigner(new DataSingle("MT71","7"));
            
        }
    }
}