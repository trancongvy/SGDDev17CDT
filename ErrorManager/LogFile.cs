using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using ErrorManager;

namespace ErrorManager
{
	public sealed class LogFile
	{
		//*********************************************************************
		//
		// Mục đích: tạo lớp chuyên quản lý nhật kí lỗi của chương trình
		// Chức năng:	tạo, xóa file log, ghi và đọc dữ liệu từ file log
		//
		//*********************************************************************
		public LogFile()
		{
		}
		public static void CreateFile(string strFileName)
		{
			if (File.Exists(strFileName) == false)
			{
				FileStream fstLog = File.Create(strFileName);
				fstLog.Close();
			}
		}
        public static string readFile(string strFileName)
        {

            string l = File.ReadAllText(strFileName);

            return l;
        }
        public static void DeleteFile(string strFileName)
		{
			if (File.Exists(strFileName))
				File.Delete(strFileName);
		}
		// Chức năng:	ghi thêm vào một file
		// Tham số:		- strFileName: tên file cần ghi
		//				- strContent: nội dung cần ghi, mỗi dòng kết thúc bằng "\n"
        public static void AppendToFile(string strFileName, string strContent)
		{
			if (File.Exists(strFileName) == false)
			{
				FileStream fstLog = File.Create(strFileName);
				fstLog.Close();
			}
			StreamWriter swrLog = File.AppendText(strFileName);
			//swrLog.Write("--Log Entry: ");
			//swrLog.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),DateTime.Now.ToLongDateString());
			string strDelimiter = "\n";
			string[] strLines = strContent.Split(strDelimiter.ToCharArray());
			foreach (string strLine in strLines)
			{
				swrLog.WriteLine(strLine);
			}
			swrLog.WriteLine ("-------------------------------");
			swrLog.Flush();
			swrLog.Close();
        }
        public static void AppendNewText(string strFileName, string strContent)
        {
            if (File.Exists(strFileName) == false)
			{
				FileStream fstLog = File.Create(strFileName);
				fstLog.Close();
			}
             File.WriteAllText(strFileName, strContent);
            
        }
        public static int SqlError(string sqlContent, SqlException sEct)
        {
            
            if (sqlContent.Contains("insert into sysHistory") || sEct.Message.Contains("insert into sysHistory"))
            {
                return 0;
            }
            if (sqlContent.Contains("Cannot open backup device") || sEct.Message.Contains("Cannot open backup device"))
            {
                return 0;
            }
            if (sqlContent.Contains("A transport-level error has occurred") || sEct.Message.Contains("A transport-level error has occurred"))
            {
                return 0;
            }
            if (sqlContent.Contains("A network-related or") || sEct.Message.Contains("A network-related or"))
            {
                return 0;
            }
            if (sqlContent.Contains("Cannot open database") || sEct.Message.Contains("Cannot open database"))
            {
                return 0;
            }
             
            string strErrorCode = "Error code: " + sEct.Number.ToString();
            string sqlContentTmp=sqlContent.Replace(" ","").ToLower();
            if (sqlContentTmp.Contains("server=") && sqlContentTmp.Contains("database=") && sqlContentTmp.Contains("user="))
            {
                sqlContent = "Connection String";
            }
            if (sEct.Number == 2)
                sqlContent = "Can't not connect to provider";
            string strMessage = "Message: " + sEct.Message;
            string strProcedure = "Procedure: " + sEct.Procedure;
            string strSource = "Source: " + sEct.Source;
            string strStackTrace = "StackTrace: " + sEct.StackTrace.Trim();
            string strChiTietLoi = "SqlContent: " + sqlContent + "\n" + strErrorCode + "\n" + strMessage + "\n" + strProcedure + "\n" + strSource + "\n" + strStackTrace;
            LogFile.AppendToFile("Log.txt", " Log at " + DateTime.Now.ToLongDateString() + "\n" + strChiTietLoi);
            string tmp = GetUserErrorMessage(sEct.Message);
           
            if (tmp == string.Empty)
                tmp = sEct.Message;
            return ErrMessageBox.Show(tmp, strChiTietLoi);
        }
        public static int SqlError(string sqlContent, SqlException sEct, bool ShowMess)
        {
            string strErrorCode = "Error code: " + sEct.Number.ToString();
            string sqlContentTmp = sqlContent.Replace(" ", "").ToLower();
            if (sqlContent.Contains("insert into sysHistory") || sEct.Message.Contains("insert into sysHistory"))
            {
                return 0;
            }
            if (sqlContent.Contains("Cannot open backup device") || sEct.Message.Contains("Cannot open backup device"))
            {
                return 0;
            }
            if (sqlContentTmp.Contains("server=") && sqlContentTmp.Contains("database=") && sqlContentTmp.Contains("user="))
            {
                sqlContent = "Connection String";
            }
            if (sEct.Number == 2)
                sqlContent = "Can't not connect to provider";
            string strMessage = "Message: " + sEct.Message;
            string strProcedure = "Procedure: " + sEct.Procedure;
            string strSource = "Source: " + sEct.Source;
            string strStackTrace = "StackTrace: " + sEct.StackTrace.Trim();
            string strChiTietLoi = "SqlContent: " + sqlContent + "\n" + strErrorCode + "\n" + strMessage + "\n" + strProcedure + "\n" + strSource + "\n" + strStackTrace;
            LogFile.AppendToFile("Log.txt", " Log at " + DateTime.Now.ToLongDateString() + "\n" + strChiTietLoi);
            string tmp = GetUserErrorMessage(sEct.Message);
            if (tmp == string.Empty)
                tmp = sEct.Message;
            if (ShowMess)
                return ErrMessageBox.Show(tmp, strChiTietLoi);
            else
                return 0;
        }
        public static void LogTruyXuatDL(string sql)
        {
            //LogFile.AppendToFile("TruyXuat.txt", sql);
        }
        private static string GetUserErrorMessage(string errorCode)
        {
            if (System.IO.File.Exists("ErrorMsg.dat"))
            {
                string[] errorMsgs = System.IO.File.ReadAllLines("ErrorMsg.dat");
                for (int i = 0; i < errorMsgs.Length; i += 2)
                {
                    if (errorCode.IndexOf(errorMsgs[i]) >= 0)
                        return errorMsgs[i + 1];
                }
            }
            return string.Empty;
        }

    }
}
