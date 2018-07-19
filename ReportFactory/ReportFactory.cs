using System;
using System.Collections.Generic;
using System.Data;
using DataFactory;
using FormFactory;
using CDTControl;

namespace ReportFactory
{
    public class ReportFactory
    {
        public static CDTForm Create(string sysReportID)
        {
            CDTData data = DataFactory.DataFactory.Create(DataType.Report, sysReportID);
            CDTForm tmp = new ReportFilter(data);
            return tmp;
        }

        public static CDTForm Create(DataRow drTable)
        {
            CDTData data = DataFactory.DataFactory.Create(DataType.Report, drTable);
            CDTForm tmp = new ReportFilter(data);
            return tmp;
        }

        public static CDTForm Create(CDTData data)
        {
            CDTForm tmp = new ReportFilter(data);
            return tmp;
        }
    }
}
