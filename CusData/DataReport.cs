using System;
using System.Collections.Generic;
using System.Data;
using CDTLib;
using CDTControl;
using CDTDatabase;

namespace CusData
{
    public class DataReport : CDTData
    {
        private string _psString = string.Empty;
        private DataTable _dtData;
        public ReConfig reConfig = new ReConfig();

        public DataTable DtReportData
        {
            get { return _dtData; }
            set { _dtData = value; }
        }
        public string PsString
        {
            get { return _psString; }
            set { _psString = value; }
        }

        public DataReport(DataRow drTable)
        {
            this._dataType = DataType.Report;
            this.GetInfor(drTable);
            this.GetStruct();
        }

        public DataReport(string sysReportID)
        {
            this._dataType = DataType.Report;
            this.GetInfor(sysReportID);
            this.GetStruct();
            
        }

        public DataReport(string sysReportID, bool isStructDb)
        {
            this._dataType = DataType.Report;
            if (isStructDb)
                this.DbData = Database.NewStructDatabase();
            this.GetInfor(sysReportID);
            this.GetStruct();
        }

        public override void GetInfor(string sysReportID)
        {
            DataTable dt = _dbStruct.GetDataTable("select * from sysReport where sysReportID = " + sysReportID);
            if (dt != null && dt.Rows.Count > 0)
                _drTable = dt.Rows[0];
        }

        public override void GetStruct()
        {
            string sysReportID = _drTable["SysReportID"].ToString();
            string queryString = "select r.*, f.*, uf.* from sysField f, sysReportFilter r, sysUserField uf where r.sysReportID = " + sysReportID + " and r.sysFieldID = f.sysFieldID and r.Visible = 1 and f.sysFieldID *= uf.sysFieldID order by r.TabIndex";
            DataTable dtStruct = _dbStruct.GetDataTable(queryString);
            if (dtStruct != null)
                _dsStruct.Tables.Add(dtStruct);
        }

        public override void GetData()
        {
            DataTable _dtData = new DataTable();
            foreach (DataRow drField in _dsStruct.Tables[0].Rows)
            {
                string fieldName = drField["FieldName"].ToString();
                int fType = Int32.Parse(drField["Type"].ToString());
                Type dcType;
                switch (fType)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 13:
                        dcType = typeof(System.String);
                        break;
                    case 3:
                    case 4:
                    case 5:
                        dcType = typeof(System.Int32);
                        break;
                    case 6:
                    case 7:
                        dcType = typeof(System.Guid);
                        break;
                    case 8:
                        dcType = typeof(System.Decimal);
                        break;
                    case 9:
                    case 11:
                        dcType = typeof(System.DateTime);
                        break;
                    case 10:
                        dcType = typeof(System.Boolean);
                        break;
                    default:
                        dcType = typeof(System.Object);
                        break;
                }
                DataColumn dc = new DataColumn(fieldName, dcType);
                _dtData.Columns.Add(dc);
                if (Boolean.Parse(drField["IsBetween"].ToString()))
                {
                    dc.ColumnName += "1";
                    DataColumn dc1 = new DataColumn(fieldName + "2", dcType);
                    _dtData.Columns.Add(dc1);
                }
            }
            DataSet dsTmp = new DataSet();
            dsTmp.Tables.Add(_dtData);
            DsData = dsTmp;
        }

        private void IsBetweenCheckRules(DataRow drField)
        {
            if (!Boolean.Parse(drField["AllowNull"].ToString()))
            {
                string fieldName1 = drField["FieldName"].ToString() + "1";
                string fieldName2 = drField["FieldName"].ToString() + "2";

                if (_drCurrentMaster[fieldName1].ToString() == string.Empty)
                    _drCurrentMaster.SetColumnError(fieldName1, "Phải nhập");
                else
                    _drCurrentMaster.SetColumnError(fieldName1, string.Empty);

                if (_drCurrentMaster[fieldName2].ToString() == string.Empty)
                    _drCurrentMaster.SetColumnError(fieldName2, "Phải nhập");
                else
                    _drCurrentMaster.SetColumnError(fieldName2, string.Empty);
            }
        }

        public override void CheckRules(DataAction dataAction)
        {
            foreach (DataRow drField in _dsStruct.Tables[0].Rows)
            {
                if (!Boolean.Parse(drField["Visible"].ToString()))
                    continue;
                string fieldName = drField["FieldName"].ToString();
                int pType = Int32.Parse(drField["Type"].ToString());
                if (pType == 3 || pType == 6)
                    continue;
                if (Boolean.Parse(drField["IsBetween"].ToString()))
                {
                    IsBetweenCheckRules(drField);
                    continue;
                }
                if (!Boolean.Parse(drField["AllowNull"].ToString()))
                {
                    if (_drCurrentMaster[fieldName].ToString() == string.Empty)
                        _drCurrentMaster.SetColumnError(fieldName, "Phải nhập");
                    else
                        _drCurrentMaster.SetColumnError(fieldName, string.Empty);
                }
                if (_drCurrentMaster[fieldName].ToString() == string.Empty)
                    continue;
                int value = 0;
                if (!Int32.TryParse(_drCurrentMaster[fieldName].ToString(), out value))
                    continue;
                if (drField["MinValue"].ToString() != string.Empty)
                {
                    int minValue = Int32.Parse(drField["MinValue"].ToString());
                    if (minValue > value)
                    {
                        _drCurrentMaster.SetColumnError(fieldName, "Phải lớn hơn hoặc bằng " + minValue.ToString());
                        continue;
                    }
                    else
                        _drCurrentMaster.SetColumnError(fieldName, string.Empty);
                }
                if (drField["MaxValue"].ToString() != string.Empty)
                {
                    int maxValue = Int32.Parse(drField["MaxValue"].ToString());
                    if (maxValue < value)
                        _drCurrentMaster.SetColumnError(fieldName, "Phải nhỏ hơn hoặc bằng " + maxValue.ToString());
                    else
                        _drCurrentMaster.SetColumnError(fieldName, string.Empty);
                }
            }
        }

        public override bool UpdateData(DataAction dataAction)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable GetDataForPrint(int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public DataTable GetFormReport()
        {
            string sysReportID = _drTable["sysReportID"].ToString();
            return (_dbStruct.GetDataTable("select * from sysFormReport where sysReportID = " + sysReportID));
        }

        public void SaveVariables()
        {
            DataRow drData = DsData.Tables[0].Rows[0];
            foreach (DataColumn dc in DsData.Tables[0].Columns)
            {
                string fieldName = dc.ColumnName;
                object value = drData[fieldName];
                if (value == null)
                    continue;
                reConfig.NewKeyValue("@" + fieldName, value);
                Config.NewKeyValue("@" + fieldName, value);
            }
        }

        protected override void SetDefaultValues(DataTable dtStruct, DataRow drData)
        {
            base.SetDefaultValues(dtStruct, drData);
            foreach (DataColumn dc in DsData.Tables[0].Columns)
            {
                string fieldName = dc.ColumnName;
                object value = Config.GetValue("@" + fieldName);
                if (value == null)
                    continue;
                try
                {
                    drData[fieldName] = value;
                }
                catch
                {
                }
            }

        }

        public void GenFilterString()
        {
            string ps = "1 = 1";
            if (DsData == null || DsData.Tables.Count == 0 || DsData.Tables[0].Rows.Count == 0)
            {
                _psString = ps;
                return;
            }
            DataRow drData = DsData.Tables[0].Rows[0];
            foreach (DataRow drField in _dsStruct.Tables[0].Rows)
            {
                string fieldName = drField["FieldName"].ToString();
                string value = string.Empty;
                if (!Boolean.Parse(drField["IsBetween"].ToString()))
                {
                    value = drData[fieldName].ToString();
                    if (value == string.Empty)
                        continue;
                }
                else
                    if (drData[fieldName + "1"].ToString() == string.Empty && drData[fieldName + "2"].ToString() == string.Empty)
                        continue;
                if (Boolean.Parse(drField["SpecialCond"].ToString()))
                    continue;
                int pType = Int32.Parse(drField["Type"].ToString());
                string tmp = string.Empty, and = " and ", field = fieldName, operato = string.Empty;
                if (Boolean.Parse(drField["IsMaster"].ToString()))
                {
                    if (_drTable["mtTableID"].ToString() != string.Empty && _drTable["mtAlias"].ToString() != string.Empty)
                        field = _drTable["mtAlias"].ToString() + "." + fieldName;
                }
                else
                    if (_drTable["dtTableID"].ToString() != string.Empty && _drTable["dtAlias"].ToString() != string.Empty)
                        field = _drTable["dtAlias"].ToString() + "." + fieldName;
                if (Boolean.Parse(drField["IsBetween"].ToString()))
                {
                    if (drData[fieldName + "1"].ToString() != string.Empty)
                    {
                        operato = " >= ";
                        value = "'" + drData[fieldName + "1"].ToString() + "'";
                        tmp = and + field + operato + value;
                    }
                    if (drData[fieldName + "2"].ToString() != string.Empty)
                    {
                        operato = " <= ";
                        value = "'" + drData[fieldName + "2"].ToString() + "'";
                        tmp += and + field + operato + value;
                    }
                }
                else
                {
                    if (pType == 1 || pType == 2 || pType == 13)
                        operato = " like ";
                    else
                        operato = " = ";
                    if (pType == 10)
                    {
                        if (Boolean.Parse(value) == true)
                            value = "1";
                        else
                            value = "0";
                    }
                    else if (pType == 1 || pType == 2)
                            value = "'" + value + "%'";
                    else if (pType == 13)
                            value = "N'%" + value + "%'";
                    else
                        value = "'" + value + "'";
                    tmp = and + field + operato + value;
                }
                ps += tmp;
            }
            _psString = ps;
        }



        private void CreateTreeData()
        {
            string sql = _drTable["TreeData"].ToString();
            if (sql == string.Empty)
                return;
            DataTable dtTreeData = DbData.GetDataTable(sql);
            if (dtTreeData == null || dtTreeData.Rows.Count == 0)
                return;

            dtTreeData.PrimaryKey = new DataColumn[] { dtTreeData.Columns[0] };
            _dtData.Columns.Add("TTSX", typeof(string));
            _dtData.PrimaryKey = new DataColumn[] { _dtData.Columns[1] };
            _dtData.DefaultView.Sort = _dtData.Columns[1].ColumnName;
            for (int i = 0; i < _dtData.Rows.Count; i++)
            {
               //if( _dtData.DefaultView[i][0].ToString()=="")
               //    _dtData.Rows[i]["TTSX"] =  _dtData.DefaultView[i][1];
                if (_dtData.Rows[i][0].ToString() == "")
                    _dtData.Rows[i]["TTSX"] = _dtData.Rows[i][1];
            }

            List<string> LstLastId = new List<string>();

            for (int i = 0; i < _dtData.Rows.Count; i++)
            {
                if (_dtData.Rows[i]["TTSX"].ToString() != "")
                    continue;
                if (_dtData.Rows[i].RowState == DataRowState.Added)
                    _dtData.Rows[i].AcceptChanges();
                
                string meId = _dtData.Rows[i][0].ToString();
                if (_dtData.Rows.Contains(meId))
                    continue;

                DataRow drDataTemp = _dtData.NewRow();

                DataRow drTreeData = dtTreeData.Rows.Find(meId);
                if (drTreeData == null) continue;
                drDataTemp[1] = meId;
                drDataTemp[0] = drTreeData[1].ToString();
                drDataTemp[_dtData.Columns.Count - 2] = drTreeData[2].ToString();
                _dtData.Rows.Add(drDataTemp);
                if (drDataTemp[0].ToString() == "")
                    LstLastId.Add(meId);
            }

            foreach (string LastId in LstLastId)
            {
                DataRow drT = _dtData.Rows.Find(LastId);
                drT["TTSX"] = drT[1].ToString();
                ExecuteLastId(drT[1].ToString(), 0);
            }            
            //_dtData.DefaultView.Sort = "TTSX";
        }

        private void ExecuteLastId(string idMe, int k)
        {
            DataRow dridMe = _dtData.Rows.Find(idMe);
            string strT = "";
            for (int ij = 0; ij < k; ij++)
            {
                strT += "    ";
            }
            dridMe[1] = strT + dridMe[1].ToString();

            DataView dvData = new DataView(_dtData);
            dvData.RowFilter = _dtData.Columns[0].ColumnName + " = '" + idMe + "'";
            dvData.Sort = _dtData.Columns[1].ColumnName;
            if (dvData.Count <= 0) return;
            int t = k + 1;
            for (int i = 0; i < dvData.Count; i++)
            {
                dvData[i]["TTSX"] = dridMe["TTSX"].ToString() + i.ToString("00000");
                ExecuteLastId(dvData[i][1].ToString(), t);
            }

            for (int i = _dtData.Columns.Count - 1; i >= 0; i--)
            {
                if (_dtData.Columns[i].DataType != typeof(decimal)) continue;
                decimal sum = 0;
                for (int j = 0; j < dvData.Count; j++)
                {
                    sum += decimal.Parse(dvData[j][i].ToString());
                }
                dridMe[i] = sum;
            }
        }

        protected override string GetContentForHistory(DataSet dsDataCopy, ref string pkValue)
        {
            string s = string.Empty;
            DataView dvData = new DataView(dsDataCopy.Tables[0]);
            if (dvData.Count > 0)
                foreach (DataRow drField in _dsStruct.Tables[0].Rows)
                {
                    int fType = Int32.Parse(drField["Type"].ToString());
                    if (fType == 3 || fType == 4 || fType == 6 || fType == 7)
                        continue;
                    if (Boolean.Parse(drField["IsBetween"].ToString()))
                    {
                        string fieldName1 = drField["FieldName"].ToString() + "1";
                        string fieldName2 = drField["FieldName"].ToString() + "2";
                        string labelName = drField["LabelName"].ToString();

                        if (dvData[0][fieldName1].ToString() != string.Empty)
                            s += labelName + ":" + dvData[0][fieldName1].ToString() + "; ";

                        if (dvData[0][fieldName2].ToString() != string.Empty)
                            s += labelName + ":" + dvData[0][fieldName2].ToString() + "; ";
                    }
                    else
                    {
                        string fieldName = drField["FieldName"].ToString();
                        string fieldValue = dvData[0][fieldName].ToString();
                        if (fieldValue == string.Empty)
                            continue;
                        string labelName = drField["LabelName"].ToString();
                        s += labelName + ":" + fieldValue + "; ";
                    }
                }
            return s;
        }

        public void GetDataForReport()
        {
            int pType = Int32.Parse(_drTable["RpType"].ToString());
            switch (pType)
            {
                case 2:
                    if (_drTable["ColField"].ToString() == string.Empty)
                        FormReport();
                    else
                    {
                        DynamicReport();
                        FormReport();
                    }
                    break;
                case 3:
                    DynamicReport();
                    break;
                default:
                    DefaultReport();
                    break;
            }
            CreateTreeData();
            if (_dtData != null)
            {
                _dtData.TableName = "ReportData";
            }
            base.InsertHistory(DataAction.IUD, DsData);
        }

        private string trimSpace(string q)
        {
            int i = 0;
            while (i < q.Length - 2)
            {
                string s = q.Substring(i, 3);
                if (s == "  +")
                {
                    q = q.Replace("  +", " +");
                    i = i - 2;
                }
                if (s == "+  ")
                {
                    q = q.Replace("+  ", "+ ");
                    i = i - 2;
                }
                i++;
            }
            return q;
        }

        private string UpdateSpecialCondition(string query)
        {
            if (DsData == null || DsData.Tables.Count == 0 || DsData.Tables[0].Rows.Count == 0)
                return query;
            DataRow drData = DsData.Tables[0].Rows[0];
            foreach (DataRow drField in _dsStruct.Tables[0].Rows)
            {
                if (!Boolean.Parse(drField["SpecialCond"].ToString()))
                    continue;
                string fieldName = drField["FieldName"].ToString().ToUpper();
                if (!Boolean.Parse(drField["IsBetween"].ToString()))
                {
                    string value = drData[fieldName].ToString();
                    if (drData.Table.Columns[fieldName].DataType == typeof(System.Int32))
                    {
                        query = query.Replace("@@" + fieldName, value);
                    }
                    else //if(drData.Table.Columns[fieldName].DataType == typeof(string) || drData.Table.Columns[fieldName].DataType == typeof(System.DateTime))
                    {
                        query = query.Replace("@@" + fieldName, "'" + value + "'");
                    }
                }
                else
                {
                    string value1 = drData[fieldName + "1"].ToString();
                    string value2 = drData[fieldName + "2"].ToString();
                    if (value1 != string.Empty)
                        query = query.Replace("@@" + fieldName + "1", "'" + value1 + "'");
                    if (value2 != string.Empty)
                        query = query.Replace("@@" + fieldName + "2", "'" + value2 + "'");
                }
            }
            if (Config.Variables.Contains("NamLamViec"))
            {
                query = query.Replace("@@NAM", Config.GetValue("NamLamViec").ToString());
            }
            foreach(System.Collections.DictionaryEntry o  in Config.Variables)
            {

                query = query.Replace("@@" + o.Key.ToString().ToUpper(), Config.GetValue(o.Key.ToString()).ToString());

            }
            return query;
        }

        private void DefaultReport()
        {
            string query = _drTable["Query"].ToString().ToUpper();
            query = trimSpace(query);
            query = query.Replace("+ @@PS", "+ '" + _psString.Replace("'", "''") + "' ");
            query = query.Replace("+@@PS", "+ '" + _psString.Replace("'", "''") + "' ");
            query = query.Replace("'@@PS'", " '" + _psString.Replace("'", "''") + "' ");
            query = query.Replace("@@PS", _psString);
            query = UpdateSpecialCondition(query);
                _dtData = DbData.GetDataTable(query);
        }

        struct FColumn
        {
            public int Rowstt;
            public string FormulaColumn;
            private bool _isEvaluated;

            public bool IsEvaluated
            {
                get { return _isEvaluated; }
                set { _isEvaluated = value; }
            }
            public FColumn(int rowstt, string formulacolumn, bool isevaluated)
            {
                Rowstt = rowstt;
                FormulaColumn = formulacolumn;
                _isEvaluated = isevaluated;
            }

        }
        List<FColumn> listFColumn = new List<FColumn>();
        private void FormReport()
        {
            string query = _drTable["Query"].ToString().ToUpper();
            query = trimSpace(query);
            query = query.Replace("+ @@PS", "+ '" + _psString.Replace("'", "''") + "' ");
            query = query.Replace("+@@PS", "+ '" + _psString.Replace("'", "''") + "' ");
            query = query.Replace("'@@PS'", " '" + _psString.Replace("'", "''") + "' ");
            query = query.Replace("@@PS", _psString);
            query = UpdateSpecialCondition(query);
            query = query.Replace("@@SYSREPORTID", _drTable["sysReportID"].ToString());
            if (_dtData == null)
                _dtData = DbData.GetDataTable(query);
            if (_dtData == null)
                return;

            int n = _dtData.Columns.Count;


            for (int i = 0; i < _dtData.Rows.Count; i++)
            {
                if (_dtData.Rows[i]["CachTinh"].ToString() != string.Empty && _dtData.Rows[i]["CachTinh"].ToString() != "0")
                {
                    FColumn x = new FColumn(i, _dtData.Rows[i]["CachTinh"].ToString(), false);
                    listFColumn.Add(x);
                }
                else continue;
            }
            for (int i = _dtData.Columns.Count - 1; i >= 0; i--)
            {
                if (_dtData.Columns[i].DataType == typeof(System.Decimal))
                {
                    foreach (FColumn fc in listFColumn)
                    {
                        if (fc.IsEvaluated == false)
                            _dtData.Rows[fc.Rowstt][i] = Evaluated(fc, i, _dtData);
                    }
                }
                else
                    break;
            }
        }

        private double Evaluated(FColumn fcx, int columnstt, DataTable dt)
        {
            System.Collections.Hashtable h = new System.Collections.Hashtable();
            Formula.BieuThuc bt = new Formula.BieuThuc(fcx.FormulaColumn);
            foreach (string str in bt.variables)
            {
                bool kt = true;
                for (int j = 0; j < listFColumn.Count; j++)
                {
                    FColumn fc = listFColumn[j];
                    if (dt.Rows[fc.Rowstt]["MaSo"].ToString() == str && fc.IsEvaluated == false)
                    {
                        dt.Rows[fc.Rowstt][columnstt] = Evaluated(fc, columnstt, dt);
                        kt = false;
                        fc.IsEvaluated = true;
                        h.Add(str, dt.Rows[fc.Rowstt][columnstt]);
                        break;
                    }
                }
                if (kt == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string MaSo = dt.Rows[i]["MaSo"].ToString();
                        if (str == MaSo)
                        {
                            h.Add(str, dt.Rows[i][columnstt]);
                            break;
                        }
                    }
                }
            }
            return bt.Evaluate(h);
        }

        private void DynamicReport()
        {
            _dtData = null;
            string colField = _drTable["ColField"].ToString();
            if (colField == string.Empty)
                return;
            string tableName = _dbStruct.GetValue("select TableName from sysTable where sysTableID = " + _drTable["mtTableID"].ToString()).ToString();
            string s = "select " + colField + " from " + tableName + " where " + _psString + " group by " + colField;
            DataTable dtColStruct = DbData.GetDataTable(s);
            if (dtColStruct == null)
                return;
            DataTable dtTmp = null;
            string query = _drTable["Query"].ToString().ToUpper();
            query = UpdateSpecialCondition(query);
            query = query.Replace("@@SYSREPORTID", _drTable["sysReportID"].ToString());
            foreach (DataRow dr in dtColStruct.Rows)
            {
                string colName = dr[0].ToString();
                string psTmp = _psString + " and " + colField + " = '" + colName + "'";
                string queryNew = query.Replace("@@PS", psTmp);
                colName = "T" + colName;
                dtTmp = DbData.GetDataTable(queryNew);
                if (dtTmp == null)
                    break;
                if (dtTmp.Rows.Count == 0)
                    continue;
                if (_dtData == null)
                {
                    _dtData = dtTmp.Clone();
                    _dtData.PrimaryKey = new DataColumn[] { _dtData.Columns[0] };
                }
                _dtData.Columns.Add(colName, typeof(System.Decimal));
                MergeData(dtTmp);
            }
            if (_dtData == null)
                return;
            _dtData.Columns.Add("Tổng cộng", typeof(System.Decimal));
            foreach (DataRow dr in _dtData.Rows)
            {
                decimal t = 0;
                for (int i = dtTmp.Columns.Count; i < _dtData.Columns.Count - 1; i++)
                    if (dr[i].ToString() != string.Empty)
                        t += Decimal.Parse(dr[i].ToString());
                dr[_dtData.Columns.Count - 1] = t;
            }
            _dtData.DefaultView.Sort = _dtData.Columns[0].ColumnName;
            
        }

        private void MergeData(DataTable dtTmp)
        {
            foreach (DataRow drTmp in dtTmp.Rows)
            {
                DataRow drData = _dtData.Rows.Find(drTmp[0]);
                if (drData != null)
                    drData[_dtData.Columns.Count - 1] = drTmp[dtTmp.Columns.Count - 1];
                else
                {
                    _dtData.ImportRow(drTmp);
                    _dtData.Rows[_dtData.Rows.Count - 1][_dtData.Columns.Count - 1] = drTmp[dtTmp.Columns.Count - 1];
                }
            }
        }

        public DataTable GetColumnsInfo()
        {
            string sql = "select * from sysField where sysTableID = " + _drTable["mtTableID"].ToString();
            string i = _drTable["dtTableID"].ToString();
            if (i != string.Empty)
                sql += " or sysTableID = " + i;
            DataTable dt = _dbStruct.GetDataTable(sql);
            return (dt);
        }

        public CDTData GetFormForReport()
        {
            string sysReportID = _drTable["sysReportID"].ToString();
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            DataTable dtTable = _dbStruct.GetDataTable("select * from sysTable where TableName = 'sysFormReport' and sysPackageID = " + sysPackageID);
            CDTData data = CusData.Create(DataType.Single, dtTable.Rows[0]);
            data.Condition = "sysReportID = " + sysReportID;
            return data;
        }

        public CDTData GetDataForDetailReport(string linkfield, string linkitem)
        {
            string s = "select * from sysReport where sysReportParentID = " + _drTable["SysReportID"].ToString();
            DataTable dt = _dbStruct.GetDataTable(s);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            CDTData data = CusData.Create(DataType.Report, dt.Rows[0]);
            data.GetData();            
            //Chuyển data cha qua data con
            DataRow dr= data.DsData.Tables[0].NewRow();
            foreach (DataColumn dc in DsData.Tables[0].Columns)
            {
                if (data.DsData.Tables[0].Columns[dc.ColumnName] == null)
                    continue;
                dr[dc.ColumnName] = DsData.Tables[0].Rows[0][dc.ColumnName];

            }
            if (!(data.DsData.Tables[0].Columns[linkfield] == null))
            {
                dr[linkfield] = linkitem.Trim();
            }
            data.DsData.Tables[0].Rows.Add(dr);
            //this.GenFilterString();
            return data;
        }

        public CDTData GetDataForVoucher(string maCT, string linkItem)
        {
            string sysReportID = _drTable["sysReportID"].ToString();
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            string s = "select * from sysTable where MaCT = '" + maCT + "' and sysPackageID = " + sysPackageID;
            DataTable dt = _dbStruct.GetDataTable(s);
            if (dt == null || dt.Rows.Count == 0)
                return null;
            CDTData data = CusData.Create(DataType.MasterDetail, dt.Rows[0]);
            data.ConditionMaster = data.DrTableMaster["Pk"].ToString() + " = '" + linkItem + "'";
            data.GetData();
            return data;

        }
    }
}
