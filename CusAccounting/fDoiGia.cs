using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using DevExpress.XtraReports.UI;
namespace CusAccounting
{
    public partial class fDoiGia : DevExpress.XtraEditors.XtraForm
    {
        public fDoiGia()
        {
            InitializeComponent();
        }
        string Makho;
        Database _db = Database.NewDataDatabase();
        BindingSource bsvt = new BindingSource();
        BindingSource bsitem = new BindingSource();
        BindingSource bsitemDuong = new BindingSource();
        DataTable tbItem;
        DataTable tbItemDuong;
        
        private void fDoiGia_Load(object sender, EventArgs e)
        {
            DataTable tbkho = _db.GetDataTable("select * from dmkho");
            if (tbkho != null) cdtGridLookUpEdit1.Properties.DataSource = tbkho;
            bsvt.CurrentChanged += new EventHandler(bsvt_CurrentChanged);
            bsitem.CurrentChanged += new EventHandler(bsitem_CurrentChanged);
           
        }

        void bsitem_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        void bsvt_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView dr = bsvt.Current as  DataRowView ;
            if (dr == null) return;
            tbItem = _db.GetDataSetByStore("DoigiaGetItem", new string[] { "@MaKho", "@mavt", "@soluong", "@dongia" }, new object[] { Makho, dr["mavt"].ToString(),Math.Abs( double.Parse(dr["soluong"].ToString())), double.Parse(dr["dongia"].ToString()) });
            bsitem.DataSource = tbItem;
            gridControl2.DataSource = bsitem;
            tbItemDuong = _db.GetDataSetByStore("DoigiaVTDuong", new string[] { "@MaKho", "@mavt" }, new object[] { Makho, dr["mavt"].ToString() });
            bsitemDuong.DataSource = tbItemDuong;
            gridControl3.DataSource = bsitemDuong;
        }

        private void cdtGridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (cdtGridLookUpEdit1.EditValue != null) Makho = cdtGridLookUpEdit1.EditValue.ToString();
        }
       
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Makho == null) return;
            DataTable tbAm = _db.GetDataSetByStore("DoigiaGetVT", new string[] { "@MaKho" }, new object[] { Makho });
            bsvt.DataSource = tbAm;
            gridControl1.DataSource = bsvt;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (bsitem.Current != null && bsitemDuong.Current != null && int.Parse((bsitem.Current as DataRowView).Row["Chon"].ToString()) == 0)
            {
                double soluong_x = double.Parse((bsitem.Current as DataRowView).Row["Soluong_x"].ToString());
                double soluong_n = double.Parse((bsitemDuong.Current as DataRowView).Row["Soluong"].ToString());
                if (soluong_x <= soluong_n)
                {
                    (bsitem.Current as DataRowView).Row["Dongia"] = (bsitemDuong.Current as DataRowView).Row["Dongia"];
                    (bsitem.Current as DataRowView).Row["Chon"] = 1;
                    (bsitemDuong.Current as DataRowView).Row["Soluong"] = soluong_n - soluong_x;
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (int.Parse((bsitem.Current as DataRowView).Row["Chon"].ToString()) == 1)
            {
                double soluong_x = double.Parse((bsitem.Current as DataRowView).Row["Soluong_x"].ToString());
                double dongia_n = double.Parse((bsitem.Current as DataRowView).Row["dongia"].ToString());
                double dongia_am = double.Parse((bsvt.Current as DataRowView).Row["dongia"].ToString());
                (bsitem.Current as DataRowView).Row["dongia"] = dongia_am;
                DataRow[] listRow = tbItemDuong.Select("Dongia=" + dongia_n.ToString());
                if (listRow.Length == 1) listRow[0]["soluong"] = double.Parse(listRow[0]["soluong"].ToString()) + soluong_x;
                (bsitem.Current as DataRowView).Row["Chon"] = 0;
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            DataRow[] listRow = tbItem.Select("Chon=1");
            try
            {
                //_db.BeginMultiTrans();
                foreach (DataRow dr in listRow)
                {
                    _db.UpdateDatabyStore("DoiGiaUpdate", new string[] { "MTIDDT", "Dongia" }, new object[] { dr["MTIDDT"], dr["Dongia"] });
                }
               // if (_db.HasErrors)
                //    _db.RollbackMultiTrans();
               // else                
                //    _db.EndMultiTrans();


            }
            catch
            {
                _db.RollbackMultiTrans();
            }
        }
        

    }
}