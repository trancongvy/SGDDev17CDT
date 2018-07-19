using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FormFactory;
using DataFactory;
using CDTLib;
using System.IO;
using CDTControl;
using CDTDatabase;

using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
namespace CDT
{


    public partial class fTaskList : Form
    {
        DataTable tbTaskList;
        List<DataStruct> lstStruct = new List<DataStruct> { };
        public fTaskList()
        {
            InitializeComponent();
        }
        Database _db = Database.NewDataDatabase();
        Database _dbstruct = Database.NewStructDatabase();
        DataTable tbTask = createTask();
        bool flag = false;
        private static DataTable createTask()
        {
            DataTable tb = new DataTable();
            DataColumn colID = new DataColumn("ID", typeof(Guid));
            tb.Columns.Add(colID);
            DataColumn colMaCT = new DataColumn("MaCT", typeof(string));
            tb.Columns.Add(colMaCT);
            DataColumn colSoCT = new DataColumn("SoCT", typeof(string));
            tb.Columns.Add(colSoCT);
            DataColumn colNgayCT = new DataColumn("NgayCT", typeof(DateTime));
            tb.Columns.Add(colNgayCT);
            DataColumn colDienGiai = new DataColumn("DienGiai", typeof(string));
            tb.Columns.Add(colDienGiai);
            DataColumn colTaskID = new DataColumn("TaskID", typeof(Guid));
            tb.Columns.Add(colTaskID);
            DataColumn colTaskName = new DataColumn("TaskName", typeof(string));
            tb.Columns.Add(colTaskName);
            DataColumn colWFName = new DataColumn("WFName", typeof(string));
            tb.Columns.Add(colWFName);
            DataColumn colTaskIcon = new DataColumn("TaskIcon", typeof(byte[]));
            tb.Columns.Add(colTaskIcon);
            DataColumn colcEdit = new DataColumn("CEdit",typeof(string));
            tb.Columns.Add(colcEdit);
            return tb;
        }
        private void fTaskList_Load(object sender, EventArgs e)
        {
            bool Language = Config.GetValue("Language").ToString() == "1";
            if (bool.Parse(Config.GetValue("Admin").ToString())) return;
            gridView1.KeyUp += gridView1_KeyUp;
            //RefreshTaskList();
            if (Language)
                DevLocalizer.Translate(this);
        }

        public string UpdateSpecialCondition(string query)
        {
            if (Config.Variables.Contains("NamLamViec"))
            {
                query = query.Replace("@@NAM", Config.GetValue("NamLamViec").ToString());
            }
            foreach (DictionaryEntry o in Config.Variables)
            {
                if (Config.GetValue(o.Key.ToString()) == null) continue;
                query = query.Replace("@@" + o.Key.ToString(), Config.GetValue(o.Key.ToString()).ToString());
                query = query.Replace("@@" + o.Key.ToString().ToUpper(), Config.GetValue(o.Key.ToString()).ToString());
            }
            return query;
        }
        string text = "";
        private void GetTaskList(object lan)
        {
            try
            {
                text = "";
                if (flag) return;
                flag = true;

                tbTask.Clear();
                bool Language = Config.GetValue("Language").ToString() == "1";
                tbTaskList = _dbstruct.GetDataSetByStore("GetTask4User", new string[] { "@sysuserid" }, new object[] { int.Parse(Config.GetValue("sysUserID").ToString()) });
                foreach (DataRow dr in tbTaskList.Rows)
                {
                    string MTTableName = dr["MasterTable"].ToString();
                    string CView = dr["cView"].ToString();
                    if (CView == string.Empty) CView = "1=1";
                    string CEdit = dr["CEDit"].ToString();
                    if (CEdit == string.Empty) CEdit = "1=1";
                    DataStruct dtstruct;
                    if (!lstStruct.Exists(x => x.TableName == MTTableName))
                    {
                        dtstruct = new DataStruct();
                        dtstruct.TableName = MTTableName;
                        dtstruct.tbStruct = _dbstruct.GetDataTable("select * from sysField where sysTableID in(select sysTableID from sysTable where tableName='" + MTTableName + "')");
                        DataTable dttmp = _dbstruct.GetDataTable("select  * from systable where tableName='" + MTTableName + "'");
                        if (dttmp.Rows.Count == 0) return;
                        dtstruct.drStruct = dttmp.Rows[0];
                        lstStruct.Add(dtstruct);
                    }
                    else
                    {
                        dtstruct = lstStruct.Find(x => x.TableName == MTTableName);
                    }
                    string Diengiai = "";
                    string from = MTTableName;
                    foreach (DataRow drStruct in dtstruct.tbStruct.Rows)
                    {
                        if (bool.Parse(drStruct["ViewOnDes"].ToString()))
                        {
                            Diengiai += MTTableName + "." + drStruct["FieldName"] + ",";
                        }
                        if (bool.Parse(drStruct["ViewOnDes"].ToString()) && drStruct["DisplayMember"] != DBNull.Value && drStruct["refTable"] != null)
                        {
                            string alias = drStruct["refTable"].ToString() + drStruct["tabIndex"].ToString().Replace("-", "");
                            Diengiai += alias + "." + drStruct["DisplayMember"].ToString() + " as " + drStruct["DisplayMember"].ToString() + drStruct["tabIndex"].ToString().Replace("-", "") + ",";
                            // Diengiai += ",";
                            from += " left join " + drStruct["refTable"].ToString() + " as " + alias + " on " + MTTableName + "." + drStruct["FieldName"].ToString() + "=" + alias + "." + drStruct["RefField"].ToString();
                        }
                    }
                    string TaskName = dr["TaskLabel"].ToString();

                    CEdit = UpdateSpecialCondition(CEdit);
                    CEdit = updateFieldName(CEdit, dtstruct);
                    //if (dr["ID"].ToString() == "443CA3FD-6093-4C7C-9919-CA9B592479EB")
                    //{
                    //}
                    string sql = "select top(" + Config.GetValue("RowCount").ToString() + ") " + MTTableName + "ID as ID, " + MTTableName + ".MaCT,  " + MTTableName + ".SoCT,  " + MTTableName + ".NgayCT," + Diengiai + MTTableName + ".TaskID, '" + CEdit.Replace("'", "''") + "' as CEdit from " + from + " where " + MTTableName + ".TaskID='" + dr["ID"].ToString() + "' and (" + CEdit + ")";
                    DataTable tbdata = _db.GetDataTable(sql);
                    //if (tbdata.Rows.Count > 0)
                    //{
                    //}
                    if (tbdata == null) continue;
                    tbTask.Columns["TaskName"].DefaultValue = dr["TaskLabel"];
                    tbTask.Columns["TaskIcon"].DefaultValue = dr["Icon"];
                    tbTask.Columns["WFName"].DefaultValue = dr["WFName"];

                    foreach (DataRow drData in tbdata.Rows)
                    {
                        DataRow drImport = tbTask.NewRow();
                        drImport["ID"] = drData["ID"];
                        drImport["MaCT"] = drData["MaCT"];
                        drImport["SoCT"] = drData["SoCT"];
                        drImport["NgayCT"] = drData["NgayCT"];
                        drImport["CEdit"] = drData["CEdit"];
                        string Des = "";
                        foreach (DataRow drStruct in dtstruct.tbStruct.Rows)
                        {
                            if (bool.Parse(drStruct["ViewOnDes"].ToString()))
                            {
                                string value = drData[drStruct["FieldName"].ToString()].ToString();
                                if (tbdata.Columns[drStruct["FieldName"].ToString()].DataType == typeof(decimal) && drData[drStruct["FieldName"].ToString()] != DBNull.Value)
                                    value = decimal.Parse(drData[drStruct["FieldName"].ToString()].ToString()).ToString(drStruct["EditMask"].ToString());
                                if (drStruct["DisplayMember"] != DBNull.Value)
                                {
                                    value += "  " + drData[drStruct["DisplayMember"].ToString() + drStruct["TabIndex"].ToString().Replace("-", "")].ToString();
                                }
                                if (value != string.Empty)
                                {
                                    Des += Language ? drStruct["LabelName2"].ToString() : drStruct["LabelName"].ToString();
                                    Des += ": " + value + "    ";
                                }

                            }
                        }
                        drImport["DienGiai"] = Des;
                        tbTask.Rows.Add(drImport);
                    }

                }
                clone = tbTask.Copy();
                flag = false;
                text = " \n Lấy dữ liệu lần thứ " + lan.ToString();


            }
            catch (Exception ex)
            {
                flag = false;
            }
        }
        DataTable clone;
        private string updateFieldName(string CEdit, DataStruct Struct)
        {
            foreach (DataRow dr in Struct.tbStruct.Rows)
            {
                string fieldName = dr["FieldName"].ToString().ToLower();
               List<int> start=new List<int>();
                
                if (CEdit.ToLower().Contains(fieldName))
                {        
                    start.Add(CEdit.ToLower().IndexOf(fieldName));
                    for(int i=start[0];i<CEdit.Length-fieldName.Length;i++)
                    {
                        if(CEdit.ToLower().Substring(i,fieldName.Length)==fieldName)
                        {
                            if (i + fieldName.Length + 1 > CEdit.Length)
                            {
                                CEdit = CEdit.Insert(i, Struct.TableName + ".");
                                i += fieldName.Length + Struct.TableName.Length+1;
                            }
                            else
                            {
                                string kytu = CEdit.ToLower().Substring(i + fieldName.Length, 1);
                                if (("+-*/=><% ").Contains(kytu))
                                {
                                    CEdit = CEdit.Insert(i, Struct.TableName + ".");
                                    i += fieldName.Length + Struct.TableName.Length+1;
                                }

                            }
                        }
                    }
                }
            }
            return CEdit;
        }
        void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5|| e.KeyCode == Keys.F3)
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    int[] i = gridView1.GetSelectedRows();
                    DataRow dr = gridView1.GetDataRow(i[0]);
                    ShowVoucher(dr);
                }
            }
           // if (e.KeyCode == Keys.Escape) this.Dispose();
        }
        private void ShowVoucher(DataRow dr)
        {
            timer1.Enabled = false;
            BindingSource _bindingSource = new BindingSource();
            string maCT;//= gridViewReport.GetFocusedRowCellValue("MACT").ToString();
            maCT = dr["MaCT"].ToString();
            string cEdit = dr["CEdit"].ToString();
            CDTData data1 = GetDataForVoucher(maCT, dr["ID"].ToString(), cEdit);
            if (data1.DsData.Tables[0].Rows.Count==0) return;
            FormDesigner _frmDesigner = new FormDesigner(data1);
            
            _bindingSource = new BindingSource();
            _bindingSource.DataSource = data1.DsData;
            _bindingSource.DataMember = data1.DsData.Tables[0].TableName;
            _frmDesigner = new FormDesigner(data1, _bindingSource);
            _frmDesigner.formAction = FormAction.Edit;
            FrmMasterDetailDt frmMtDtCt = new FrmMasterDetailDt(_frmDesigner);
            frmMtDtCt.ShowDialog();
            timer1.Enabled = true;
        }
        public CDTData GetDataForVoucher(string maCT, string linkItem,string cEdit)
        {
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            string s = "select * from sysTable where type=3 and MaCT = '" + maCT + "' and sysPackageID = " + sysPackageID;
            DataTable dt = _dbstruct.GetDataTable(s);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            CDTData data = DataFactory.DataFactory.Create(DataType.MasterDetail, dt.Rows[0]);
            data.ConditionMaster = data.DrTableMaster["Pk"].ToString() + " = '" + linkItem + "' and (" + cEdit + ")";
            data.GetData();
            return data;

        }
        int i=0;
        private void RefreshTaskList()
        {
           // richTextBox1.Text += "\n Thực hiện lần thứ " + i.ToString();
            //if (text != "") richTextBox2.Text += text;
            if (i < 1000000) i++;
            else i = 0;
            if (!flag)
            {
                Thread Do = new Thread(this.GetTaskList);
                Do.Name = i.ToString();
                Do.Start(i);                
            }
            try
            {
                if (text != "")
                {
                    DataTable clone1 = clone.Copy();
                    gridControl1.BeginInvoke(new MethodInvoker(delegate { gridControl1.DataSource = clone; }));
                }
                    //gridControl1.DataSource = tbTask;
            }
            catch (Exception ex)
            {
            }
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bool.Parse(Config.GetValue("Admin").ToString())) return;
                       RefreshTaskList();

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = int.Parse(spinEdit1.EditValue.ToString()) * 60;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (bool.Parse(Config.GetValue("Admin").ToString())) return;
            RefreshTaskList();
        }
    }
    
}
