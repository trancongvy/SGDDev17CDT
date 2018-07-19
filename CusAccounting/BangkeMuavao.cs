using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace CusAccounting
{
    class  BangkeMuavao
    {
        private Database _dbData = Database.NewDataDatabase();
        private string _fileName = string.Empty;
        private DateTime tungay;
        private bool _isError = false;
        private string _tmpFile = string.Empty;
        private DateTime denngay;

        public BangkeMuavao(string tmpFile, string fileName, DateTime fromDate, DateTime toDate)
        {
            this._fileName = fileName;
            this._tmpFile = tmpFile;
            this.tungay = fromDate;
            this.denngay = toDate;
        }
        private System.Data.DataTable GetData(string type)
        {
          
            return _dbData.GetDataSetByStore("GetdataVatin", new string[] { "@Ngayct1", "@ngayct2", "@type" }, new object[] { tungay, denngay, type });                                       
           
        }
        private void AddDataroRow(DataRow dr, ref Range row)
        {
            //row.Cells[0, 3] = dr["MaHd"];
            row.Cells[0, 3] = dr["MauHd"];
            row.Cells[0, 4] = dr["soserie"];
            row.Cells[0, 5] = dr["sohoadon"];
            row.Cells[0, 6] = dr["ngayhd"].ToString();
            row.Cells[0, 7] = dr["TenKH"];
            row.Cells[0, 8] = dr["MSt"];
            row.Cells[0, 9] = dr["Diengiai"];
            row.Cells[0, 10] = dr["TTien"];
            row.Cells[0, 11] = dr["Thuesuat"];
            row.Cells[0, 12] = dr["TThue"];

        }
        public void AddDatatoFile()
        {
            //Application application = new Application();
            //Workbook workbook = application.Workbooks.Open(this._tmpFile, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //try
            //{
            //    Worksheet worksheet =(workbook.Sheets[1] as Worksheet);
                
            //    double TTien = 0;
            //    double TThue = 0;
            //    Range row;
            //    System.Data.DataTable tb = GetData("5");
            //    for (int i = 0; i < tb.Rows.Count; i++)
            //    {
            //        Range Row;

            //        Row =  worksheet.get_Range(worksheet.Cells[30, 1], worksheet.Cells[30, 1]).EntireRow;
            //        if (i > 0)
            //        {
            //            Row.Insert(XlInsertShiftDirection.xlShiftDown, System.Type.Missing);
            //        }
            //        Row = worksheet.get_Range(worksheet.Cells[31, 1], worksheet.Cells[31, 1]).EntireRow;
            //        Row.Cells[0, 2] = tb.Rows.Count - i;
            //        AddDataroRow(tb.Rows[i], ref Row);
            //        TTien += double.Parse(tb.Rows[i]["TTien"].ToString());
            //        TThue += double.Parse(tb.Rows[i]["TThue"].ToString());
            //    }
            //    row = worksheet.get_Range(worksheet.Cells[31 + tb.Rows.Count, 1], worksheet.Cells[31 + tb.Rows.Count, 1]).EntireRow;
            //    row.Cells[0, 10] = TTien;
            //    row.Cells[0, 12] = TThue;
            //    TTien = 0;
            //    TThue = 0;
            //    tb = GetData("4");
            //    for (int i = 0; i < tb.Rows.Count; i++)
            //    {
            //        Range Row;

            //        Row = worksheet.get_Range(worksheet.Cells[27, 1], worksheet.Cells[27, 1]).EntireRow;
            //        if (i > 0)
            //        {
            //            Row.Insert(XlInsertShiftDirection.xlShiftDown, System.Type.Missing);
            //        }
            //        Row = worksheet.get_Range(worksheet.Cells[28, 1], worksheet.Cells[28, 1]).EntireRow;
            //        Row.Cells[0, 2] = tb.Rows.Count - i;
            //        AddDataroRow(tb.Rows[i], ref Row);
            //        TTien += double.Parse(tb.Rows[i]["TTien"].ToString());
            //        TThue += double.Parse(tb.Rows[i]["TThue"].ToString());
            //    }
            //    row = worksheet.get_Range(worksheet.Cells[28+ tb.Rows.Count, 1], worksheet.Cells[28 + tb.Rows.Count, 1]).EntireRow;
            //    row.Cells[0, 10] = TTien;
            //    row.Cells[0, 12] = TThue;
            //    TTien = 0;
            //    TThue = 0;
            //    tb = GetData("3");
            //    for (int i = 0; i < tb.Rows.Count; i++)
            //    {
            //        Range Row;

            //        Row = worksheet.get_Range(worksheet.Cells[24, 1], worksheet.Cells[24, 1]).EntireRow;
            //        if (i > 0)
            //        {
            //            Row.Insert(XlInsertShiftDirection.xlShiftDown, System.Type.Missing);
            //        }
            //        Row = worksheet.get_Range(worksheet.Cells[25, 1], worksheet.Cells[25, 1]).EntireRow;
            //        Row.Cells[0, 2] = tb.Rows.Count - i;
            //        AddDataroRow(tb.Rows[i], ref Row);
            //        TTien += double.Parse(tb.Rows[i]["TTien"].ToString());
            //        TThue += double.Parse(tb.Rows[i]["TThue"].ToString());
            //    }
            //    row = worksheet.get_Range(worksheet.Cells[25 + tb.Rows.Count, 1], worksheet.Cells[25 + tb.Rows.Count, 1]).EntireRow;
            //    row.Cells[0, 10] = TTien;
            //    row.Cells[0, 12] = TThue;
            //    TTien = 0;
            //    TThue = 0;
            //    tb = GetData("2");
            //    for (int i = 0; i < tb.Rows.Count; i++)
            //    {
            //        Range Row;

            //        Row = worksheet.get_Range(worksheet.Cells[21, 1], worksheet.Cells[21, 1]).EntireRow;
            //        if (i > 0)
            //        {
            //            Row.Insert(XlInsertShiftDirection.xlShiftDown, System.Type.Missing);
            //        }
            //        Row = worksheet.get_Range(worksheet.Cells[22, 1], worksheet.Cells[22, 1]).EntireRow;
            //        Row.Cells[0, 2] = tb.Rows.Count - i;
            //        AddDataroRow(tb.Rows[i], ref Row);
            //        TTien += double.Parse(tb.Rows[i]["TTien"].ToString());
            //        TThue += double.Parse(tb.Rows[i]["TThue"].ToString());
            //    }
            //    row = worksheet.get_Range(worksheet.Cells[22 + tb.Rows.Count, 1], worksheet.Cells[22 + tb.Rows.Count, 1]).EntireRow;
            //    row.Cells[0, 10] = TTien;
            //    row.Cells[0, 12] = TThue;
            //    TTien = 0;
            //    TThue = 0;
            //    tb = GetData("1");
            //    for (int i = 0; i < tb.Rows.Count; i++)
            //    {
            //        Range Row;

            //        Row = worksheet.get_Range(worksheet.Cells[18, 1], worksheet.Cells[18, 1]).EntireRow;
            //        if (i > 0)
            //        {
            //            Row.Insert(XlInsertShiftDirection.xlShiftDown, System.Type.Missing);
            //        }
            //        Row = worksheet.get_Range(worksheet.Cells[19, 1], worksheet.Cells[19, 1]).EntireRow;
            //        Row.Cells[0, 2] = tb.Rows.Count - i;
            //        AddDataroRow(tb.Rows[i], ref Row);
            //        TTien += double.Parse(tb.Rows[i]["TTien"].ToString());
            //        TThue += double.Parse(tb.Rows[i]["TThue"].ToString());
            //    }
            //    row = worksheet.get_Range(worksheet.Cells[19+ tb.Rows.Count, 1], worksheet.Cells[19 + tb.Rows.Count, 1]).EntireRow;
            //    row.Cells[0, 10] = TTien;
            //    row.Cells[0, 12] = TThue;

            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    workbook.SaveAs(this._fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //    workbook.Close(false, false, false);
            //    application.Quit();


            //}
        }
        public bool IsError
        {
            get
            {
                return this._isError;
            }
        }
    }
}
