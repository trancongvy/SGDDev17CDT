using System;
using System.Collections.Generic;
using System.Data;
using CusData;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList;
using CDTLib;
using CDTControl;
using DevControls;
using DevExpress.XtraGrid.Columns;
namespace CusForm
{
    public partial class FrmSgMergeCode : DevExpress.XtraEditors.XtraForm
    {
        private CDTData _data;
        private FormDesigner _frmDesigner;
        private CDTGridLookUpEdit GLookup;
        public BindingSource bs=new BindingSource();
        public DataRow re;
        public FrmSgMergeCode(CDTData data,FormDesigner fDesigner)
        {
            InitializeComponent();
            this._data = data;
            //SetRight();
            this._frmDesigner = fDesigner;
            gridLookUpEdit1View.BeginInit();

            gridColumn1.FieldName = _data.DsData.Tables[0].Columns[0].ColumnName;
            DataRow[] lf = _data.DsStruct.Tables[0].Select("FieldName ='" + gridColumn1.FieldName + "'");
            if (lf.Length > 0) gridColumn1.Caption = lf[0]["LabelName"].ToString();
            gridLookUpEdit1View.Columns.Add(gridColumn1);

            gridColumn2.FieldName = _data.DsData.Tables[0].Columns[1].ColumnName;
            lf = _data.DsStruct.Tables[0].Select("FieldName ='" + gridColumn2.FieldName + "'");
            if (lf.Length > 0) gridColumn2.Caption = lf[0]["LabelName"].ToString();
            gridLookUpEdit1View.Columns.Add(gridColumn2);
            gridLookUpEdit1View.EndInit();

            bs.DataSource = _data.DsData.Tables[0];
            gridLookUpEdit1.Properties.DataSource = bs;
            gridLookUpEdit1.Properties.ValueMember = _data.DsData.Tables[0].Columns[0].ColumnName;
            gridLookUpEdit1.Properties.DisplayMember = _data.DsData.Tables[0].Columns[1].ColumnName;
        }

        private void FrmSgMergeCode_Load(object sender, EventArgs e)
        {
            
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (bs.Current == null) return;
            re = (bs.Current as DataRowView).Row;
            this.Dispose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            re = null;
            this.Dispose();
        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}