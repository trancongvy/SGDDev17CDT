using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
namespace CDTControl
{
    public class ImportExcel
    {
        string fileName;
        Workbook workbook;
        Application application = new ApplicationClass();
        public System.Data.DataTable Db;
        public ImportExcel(string FileName)
        {
            fileName = FileName;

        }

        public List<string> GetSheets()
        {
            List<string> list = new List<string>();

            workbook = application.Workbooks.Open(fileName, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            foreach (Worksheet worksheet in workbook.Sheets)
            {
                list.Add(worksheet.Name);
                
            }
            workbook.Close(false, false, false);
            application.Quit();
            return list;
        }



        public List<string> GetCol(string p)
        {
            workbook = application.Workbooks.Open(fileName, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            List<string> Collist = new List<string>();
            
            foreach (Worksheet worksheet in workbook.Sheets)
            {
                
                if (worksheet.Name == p)
                {
                    Db = new System.Data.DataTable();
                    for (int i = 65; i < 90; i++)
                    {
                        string c = ((char)i).ToString();
                        Range r = worksheet.get_Range(c + "1");
                        string t = r.Text.ToString();
                        Range r1 = worksheet.get_Range(c + "2");
                        Type type = r1.Text.GetType();
                        if (t.Trim() != "")
                        {
                            Collist.Add(t.Trim());
                            System.Data.DataColumn col = new DataColumn(t.Trim(), type);
                            Db.Columns.Add(col);
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int j = 2; j < 35000; j++)
                    {
                        DataRow dr= Db.NewRow();;
                        for (int i = 0; i < Db.Columns.Count; i++)
                        {
                            int k = i + 65;
                            string c = ((char)k).ToString();
                            Range r = worksheet.get_Range(c + j.ToString());
                            string t = r.Text.ToString();
                            Type type = r.Text.GetType();
                            
                            if (t.Trim() != "")
                            {                                
                                dr[Db.Columns[i]] = r.Text;
                            }
                            if (t.Trim() == "" && i == 0)
                            {
                                j = 35000;
                                i = Db.Columns.Count;
                            }
                            
                        }
                        if (j < 35000) 
                            Db.Rows.Add(dr);
                        
                    }
                }
                break;

            }
            workbook.Close(false, false, false);
            application.Quit();

            
            //OleDbDataAdapter ole = new OleDbDataAdapter(); ;
            //try
            //{
            //    string selectConnectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + this.fileName + "';Extended Properties=Excel 14.0;";
            //    ole = new OleDbDataAdapter("select * from [" + p + "$]", selectConnectionString);
            //    Db = new System.Data.DataTable();

            //    ole.Fill(Db);
            //    for (int i = 0; i < Db.Columns.Count; i++)
            //    {
            //        Collist.Add(Db.Columns[i].ColumnName);
            //    }
            //    ole.SelectCommand.Connection.Close();
            //}
            //catch (Exception ex) {
                
            //    ole.SelectCommand.Connection.Close(); }
            return Collist;
        }

    }
}
