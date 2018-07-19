using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CDTDatabase;

namespace Plugins
{
    public class StructPara
    {
        private Database _dbData;
        private DataRow _drTable;
        private DataRow _drTableMaster;
        private DataSet _dsData;
        private string _tableName;
        private int _curMasterIndex;
        private DataSet _dsDataCopy;

        public Database DbData
        {
            get { return _dbData; }
            set { _dbData = value; }
        }

        public DataRow DrTable
        {
            get { return _drTable; }
            set { _drTable = value; }
        }

        public DataRow DrTableMaster
        {
            get { return _drTableMaster; }
            set { _drTableMaster = value; }
        }

        public DataSet DsData
        {
            get { return _dsData; }
            set { _dsData = value; }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public int CurMasterIndex
        {
            get { return _curMasterIndex; }
            set { _curMasterIndex = value; }
        }

        public DataSet DsDataCopy
        {
            get { return _dsDataCopy; }
            set { _dsDataCopy = value; }
        }

        public StructPara(Database dbData, DataRow drTable, DataRow drTableMaster, DataSet dsData, string tableName, int curMasterIndex, DataSet dsDataCopy)
        {
            _dbData = dbData;
            _drTable = drTable;
            _drTableMaster = drTableMaster;
            _dsData = dsData;
            _tableName = tableName;
            _curMasterIndex = curMasterIndex;
            _dsDataCopy = dsDataCopy;

        }
    }
}
