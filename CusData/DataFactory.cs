using System;
using System.Collections.Generic;
using System.Data;
using CDTDatabase;
using CDTControl;

namespace CusData
{
    public class CusData
    {
        public static CDTData Create(DataType type, string sysTableID)
        {
            CDTData tmp = null;
            switch (type)
            {
                case DataType.Detail:
                    tmp = new DataDetail(sysTableID);
                    break;
                case DataType.MasterDetail:
                    tmp = new DataMasterDetail(sysTableID);
                    break;
                case DataType.Single:
                    tmp = new DataSingle(sysTableID);
                    break;
                case DataType.Report:
                    tmp = new DataReport(sysTableID);
                    break;
            }
            return tmp;
        }

        public static CDTData Create(DataType type, DataRow drTable)
        {
            CDTData tmp = null;
            switch (type)
            {
                case DataType.Detail:
                    tmp = new DataDetail(drTable);
                    break;
                case DataType.MasterDetail:
                    tmp = new DataMasterDetail(drTable);
                    break;
                case DataType.Single:
                    tmp = new DataSingle(drTable);
                    break;
                case DataType.Report:
                    tmp = new DataReport(drTable);
                    break;
            }
            return tmp;
        }

        public static CDTData Create(DataType type, string TableName, string sysPackageID)
        {
            CDTData tmp = null;
            switch (type)
            {
                case DataType.Detail:
                    tmp = new DataDetail(TableName, sysPackageID);
                    break;
                case DataType.MasterDetail:
                    tmp = new DataMasterDetail(TableName, sysPackageID);
                    break;
                case DataType.Single:
                    tmp = new DataSingle(TableName, sysPackageID);
                    break;
            }
            return tmp;
        }
    }
}
