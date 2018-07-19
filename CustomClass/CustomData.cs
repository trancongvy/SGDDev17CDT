using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using System.Collections;
using CDTLib;
using CDTDatabase;
using CDTControl;

namespace CustomClass
{
    public enum FormAction { New, Edit, Delete, Copy, Filter, View };
    public class CustomData
    {
        protected Database _db = Database.NewDataDatabase();
        protected Database _dbSt = Database.NewStructDatabase();
        public DataTable mt;
        public DataTable dt;

       public DataSet ds;
        public DataSet dsStr;
        internal DataRow _mtCur;
        internal DataMasterDetailPrint _printData;
        public List<DataRow> lstDt = new List<DataRow>();
        public List<DataRow> lstDtCr = new List<DataRow>();
        public bool _datachange = false;
        public DataSet dsTmp;
        protected AutoIncrementValues _AutoCreate;
        public string _Condition="";
        protected struct SqlField
        {
            public string FieldName;
            public SqlDbType DbType;
            public SqlField(string fieldName, SqlDbType dbType)
            {
                FieldName = fieldName;
                DbType = dbType;
            }
        }
        protected string AutoIncreateSoCt()
        {
            _AutoCreate = new AutoIncrementValues(dsStr.Tables[0].Rows[0]["sysTableID"].ToString(), dsStr.Tables[2]);
            _AutoCreate._dbStruct = this._dbSt;
            _AutoCreate.MakeNewStruct();
            DataRow[] ldr = _AutoCreate.DtStruct.Select("FieldName='SoCt'");
            if (ldr.Length == 0) return "HD0000001";
            return ldr[0]["DefaultValue"].ToString();
        }
        public DataTable GetReportFile(string TableIDid)
        {
            string sql = "select RDes, RFile from sysReportFile where sysTableID=" + TableIDid + " order by stt";
            DataTable tbMau = _dbSt.GetDataTable(sql);
            return tbMau;
        }
        public DataTable GetDataForPrint(int index)
        {
            string pk = dsStr.Tables[0].Rows[0]["Pk"].ToString();                
            string dataID = mt.Rows[index][pk].ToString();
            if (_printData == null)
                CreatePrintVoucher();
            return (_printData.GetData(dataID));

        }
        private void CreatePrintVoucher()
        {
            string mtTableID = dsStr.Tables[0].Rows[0]["sysTableID"].ToString();
            string dtTableID = dsStr.Tables[1].Rows[0]["sysTableID"].ToString();
            this._printData = new DataMasterDetailPrint(mtTableID, dtTableID);
        }
    }
}
