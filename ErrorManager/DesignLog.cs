using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace ErrorManager
{
	public sealed class DesignLog
	{
		//*********************************************************************
		//
		// Mục đích: tạo lớp chuyên quản lý nhật kí lỗi của chương trình
		// Chức năng:	tạo, xóa file log, ghi và đọc dữ liệu từ file log
		//
		//*********************************************************************
        public DesignLog()
		{
		}
		private static void CreateFile(string strFileName)
		{
			if (File.Exists(strFileName) == false)
			{
				FileStream fstLog = File.Create(strFileName);
				fstLog.Close();
			}
		}
		private static void DeleteFile(string strFileName)
		{
			if (File.Exists(strFileName))
				File.Delete(strFileName);
		}
		// Chức năng:	ghi thêm vào một file
		// Tham số:		- strFileName: tên file cần ghi
		//				- strContent: nội dung cần ghi, mỗi dòng kết thúc bằng "\n"
        private static void AppendToFile(string strFileName, string strContent)
		{
			if (File.Exists(strFileName) == false)
			{
				FileStream fstLog = File.Create(strFileName);
				fstLog.Close();
			}
			StreamWriter swrLog = File.AppendText(strFileName);
            
			string strDelimiter = "\n";
			string[] strLines = strContent.Split(strDelimiter.ToCharArray());
			foreach (string strLine in strLines)
			{
				swrLog.WriteLine(strLine);
			}

			swrLog.Flush();
			swrLog.Close();
        }

        public static void SqlError(string sqlContent)
        {
            DesignLog.AppendToFile("DesignLog.txt", sqlContent);            
        }

        private static string GetUserErrorMessage(int errorCode)
        {
            if (System.IO.File.Exists("ErrorMsg.dat"))
            {
                string[] errorMsgs = System.IO.File.ReadAllLines("ErrorMsg.dat");
                for (int i = 0; i < errorMsgs.Length; i += 2)
                {
                    if (errorCode.ToString() == errorMsgs[i])
                        return errorMsgs[i + 1];
                }
            }
            return string.Empty;
        }

    }
}
