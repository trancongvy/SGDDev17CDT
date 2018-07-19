using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using CDTDatabase;
using DevExpress.XtraScheduler;
namespace FormFactory
{
    public partial class fFlowAnalyze : DevExpress.XtraEditors.XtraForm
    {
        public DataTable tbSumary;
        Database db = Database.NewStructDatabase();
        DateTime ngayct1;
        DateTime ngayct2 ;
        public fFlowAnalyze()
        {
            InitializeComponent();
           
                
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (dateEdit1.EditValue == null || dateEdit1.EditValue == null) return;
            ngayct1 = DateTime.Parse(dateEdit1.EditValue.ToString());
            ngayct2 = DateTime.Parse(dateEdit2.EditValue.ToString());
            SetDataTotal();
        }
        private void SetDataTotal()
        {
            tbSumary = db.GetDataSetByStore("getAFSumary", new string[] { "@ngayct1", "@ngayct2" }, new object[] { ngayct1, ngayct2 });
            chartSumary.DataSource = tbSumary;
            chartSumary.GetSeriesByName("Total").ArgumentDataMember = "WFName";
            chartSumary.GetSeriesByName("Total").ValueDataMembers.AddRange(new string[] { "Total" });
            chartSumary.GetSeriesByName("Finish").ArgumentDataMember = "WFName";
            chartSumary.GetSeriesByName("Finish").ValueDataMembers.AddRange(new string[] { "Finish" });
            chartSumary.GetSeriesByName("Cancel").ArgumentDataMember = "WFName";
            chartSumary.GetSeriesByName("Cancel").ValueDataMembers.AddRange(new string[] { "Cancel" });

        }
        private void getWorkDay()
        {
                
        }
        private void fFlowAnalyze_Load(object sender, EventArgs e)
        {
            if (Config.GetValue("@NgayCT1") != null)
            {
                dateEdit1.EditValue = DateTime.Parse(Config.GetValue("@NgayCT1").ToString());
            }
            if (Config.GetValue("@NgayCT2") != null)
            {
                dateEdit2.EditValue = DateTime.Parse(Config.GetValue("@NgayCT2").ToString());
            }
        }
    }
}