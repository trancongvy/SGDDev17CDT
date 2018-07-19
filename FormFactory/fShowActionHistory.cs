using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using DataFactory;

namespace FormFactory
{
    public partial class fShowActionHistory : DevExpress.XtraEditors.XtraForm
    {
        DataMasterDetail _Data;
        Database _dbData = Database.NewStructDatabase();
        DataTable tb;
        public fShowActionHistory(DataMasterDetail _data)
        {
            InitializeComponent();
            _Data=_data;
            tb = getHistoryData();
        }
        private DataTable getHistoryData()
        {
            string sql = "  select a.hDate,d.CommandName,e.UserName, b.taskLabel as BTName, c.TaskLabel as ETName from sysactionhistory a ";
            sql += "inner join systask b on a.btid=b.id 	inner join systask c on a.etid=c.id";
            sql+= " inner join sysAction d on a.ActionID =d.ID inner join sysUser e on a.sysUserId=e.sysUserID ";
            sql += " where pkid='" + _Data.DrCurrentMaster[_Data.PkMaster.FieldName].ToString() + "' and actionid in (";
            sql += "select id from sysaction where wfid in (select id from syswf where systableid=" + this._Data.DrTable["sysTableID"].ToString() + "))";
            return _dbData.GetDataTable(sql);
        }

        private void fShowActionHistory_Load(object sender, EventArgs e)
        {
            if (tb == null) return;
            BindingSource bs = new BindingSource();
            bs.DataSource = tb;
            bs.DataMember = tb.TableName;
            gridControl1.DataSource = bs;
        }
    }
}