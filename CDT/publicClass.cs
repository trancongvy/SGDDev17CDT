using System;
using System.Collections.Generic;
using System.Text;
using FormFactory;
using DataFactory;
using System.Data;
using CDTLib;
using System.IO;
using CDTControl;
using CDTDatabase;
namespace CDT
{
   public partial class DataStruct
    {
        public DataTable tbStruct { get; set; }
        public DataRow drStruct { get; set; }
        public string TableName { get; set; }
    }
}
