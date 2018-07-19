using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
namespace CDTControl
{
    public class ExcelReport
    {
        // Fields
        private Database _dbData = Database.NewDataDatabase();
        private string _fileName = string.Empty;
        private DateTime _fromDate;
        private bool _isError = false;
        private string _tmpFile = string.Empty;
        private DateTime _toDate;

        // Methods
        public ExcelReport(string tmpFile, string fileName, DateTime fromDate, DateTime toDate)
        {
            this._fileName = fileName;
            this._tmpFile = tmpFile;
            this._fromDate = fromDate;
            this._toDate = toDate;
        }

        public void FillData()
        {
            Application application = new ApplicationClass();
            Workbook workbook = application.Workbooks.Open(this._tmpFile, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            try
            {
                Worksheet worksheet = (Worksheet)workbook.Sheets[1];
                worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, 1]).ClearNotes();
                Range usedRange = worksheet.UsedRange;
                object[,] objArray = (object[,])usedRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);
                for (int i = 1; i <= objArray.GetLength(0); i++)
                {
                    for (int j = 1; j <= objArray.GetLength(1); j++)
                    {
                        int length = objArray.GetLength(1);
                        if (objArray[i, j] != null)
                        {
                            string s = objArray[i, j].ToString().Trim();
                            if (s.StartsWith("&") && s.EndsWith("&"))
                            {
                                double num4 = this.GetData(s);
                                if (num4 == double.MinValue)
                                {
                                    this._isError = true;
                                    (usedRange[i, j] as Range).AddComment("Incorrect format formula");
                                }
                                else
                                {
                                    usedRange[i, j] = num4;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                this._isError = true;
            }
            finally
            {
                try
                {
                    workbook.SaveAs(this._fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    workbook.Close(false, false, false);
                    application.Quit();
                }
                catch
                {
                }
                finally
                {
                    application.Quit();
                }
            }
        }

        private double GetData(string s)
        {
            string[] strArray2;
            object[] objArray;
            object[] objArray3;
            string[] strArray = s.Replace("&", "").Split(new char[] { ';' });
            if ((strArray.Length < 3) || (strArray.Length > 4))
            {
                return -1;
            }
            string str = "GetValueForExcelReport";
            if (strArray.Length == 3)
            {
                strArray2 = new string[] { "@ngayct1", "@ngayct2", "@loaiCt", "@tk", "@tkdu", "@iscn", "@value" };
                objArray3 = new object[7];
                objArray3[0] = this._fromDate;
                objArray3[1] = this._toDate;
                objArray3[2] = strArray[2];
                objArray3[3] = strArray[0];
                objArray3[4] = strArray[1];
                objArray3[5] = 0;
                objArray = objArray3;
            }
            else
            {
                strArray2 = new string[] { "@ngayct1", "@ngayct2", "@loaiCt", "@tk", "@tkdu", "@iscn", "@value" };
                objArray3 = new object[7];
                objArray3[0] = this._fromDate;
                objArray3[1] = this._toDate;
                objArray3[2] = strArray[2];
                objArray3[3] = strArray[0];
                objArray3[4] = strArray[1];
                objArray3[5] = strArray[3];
                objArray = objArray3;
            }
            double objArray2 = this._dbData.GetValueByStore(str, strArray2, objArray,  new ParameterDirection[] { ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Output },5);

            return objArray2;
        }

        // Properties
        public bool IsError
        {
            get
            {
                return this._isError;
            }
        }
    }


}
