using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using CDTLib;
using System.Windows.Forms;
namespace CusAccounting
{
    class BangkeBanra
    {
        private Database _dbData = Database.NewDataDatabase();
        private string _fileName = string.Empty;
        private DateTime tungay;
        private bool _isError = false;
        private string _tmpFile = string.Empty;
        private DateTime denngay;

        public BangkeBanra(string tmpFile, string fileName, DateTime fromDate, DateTime toDate)
        {
            this._fileName = fileName;
            this._tmpFile = tmpFile;
            this.tungay = fromDate;
            this.denngay = toDate;
        }
        private System.Data.DataTable GetData(string MaThue)
        {
            return _dbData.GetDataSetByStore("GetdataVatout", new string[] { "@Ngayct1", "@ngayct2", "@Mathue" }, new object[] { tungay, denngay, MaThue });                        
            
        }
        private void AddDataroRow(DataRow dr, ref Range row)
        {
            //row.Cells[0, 3] = Config.GetValue("MaHd").ToString();
            row.Cells[0, 3] = Config.GetValue("MauHd").ToString();
            row.Cells[0, 4] = dr["soserie"];
            row.Cells[0, 5] = dr["sohoadon"];
            row.Cells[0, 6] = DateTime.Parse( dr["ngayct"].ToString()).ToString("dd/MM/yyyy");
            row.Cells[0, 7] = dr["TenKH"];
            row.Cells[0, 8] = dr["MSt"];
            row.Cells[0, 9] = dr["Diengiai"];
            row.Cells[0, 10] = dr["TTien"];
            row.Cells[0, 11] = dr["TThue"];

        }
        public void AddDatatoFile()
        {
            //Microsoft.Office.Interop.Excel.Application application = new ApplicationClass();
            //Workbook workbook = application.Workbooks.Open(this._tmpFile, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //try
            //{
            //    Worksheet worksheet = (Worksheet)workbook.Sheets[1];
            //    System.Data.DataTable tb = GetData("10");
            //    double TTien = 0;
            //    double TThue = 0;
            //    Range row;
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
            //    row = worksheet.get_Range(worksheet.Cells[28 + tb.Rows.Count, 1], worksheet.Cells[28 + tb.Rows.Count, 1]).EntireRow;
            //    row.Cells[0, 11] = TTien;
            //    row.Cells[0, 12] = TThue;
            //    TTien = 0;
            //    TThue = 0;
            //    tb = GetData("05");
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
            //    row.Cells[0, 11] = TTien;
            //    row.Cells[0, 12] = TThue;
            //    tb = GetData("00");
            //    TTien = 0;
            //    TThue = 0;
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
            //    }
            //    tb = GetData("KT");
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
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
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
