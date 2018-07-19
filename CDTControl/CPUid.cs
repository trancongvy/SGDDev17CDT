using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using CDTLib;

namespace CDTControl.CDTControl
{
    public class CPUid
    {
        private string _CompanyName;
        public CPUid(string CompanyName)
        {
             _CompanyName = CompanyName.ToUpper();
            SeperateString();
           _keyString= GetKeyString();
           
        }

        private string _keyString = string.Empty;

        public string KeyString
        {
            get
            { 
                return _keyString;
            }
            set { _keyString = value; }
        }

        private string _mixString = string.Empty;

        public string MixString
        {
            get { return _mixString; }
            set { _mixString = value; }
        }


        private string _cPUidStr = string.Empty;
   
        private string _cPUStr1 = string.Empty;

        private string _cPUStr2 = string.Empty;

        private void GetCPUid()
        {
            ManagementClass MC = new ManagementClass("Win32_Processor");
            ManagementObjectCollection MOC = MC.GetInstances();
            foreach (ManagementObject mo in MOC)
            {
                if (mo.Properties["ProcessorId"].Value.ToString() != "")
                {
                    _cPUidStr = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
            }
            

        }

        private void SeperateString()
        {

            GetCPUid();
            for (int i = 0; i < _cPUidStr.Length; i++)
            {
                if (i == 0 || i == _cPUidStr.Length / 2 || i == _cPUidStr.Length - 1)
                {
                    _cPUStr1 += _cPUidStr.ToCharArray()[i].ToString();
                }
                else
                    _cPUStr2 += _cPUidStr.ToCharArray()[i].ToString();

            }
            _cPUStr1 = _cPUStr1 + _CompanyName;
            MixString = Security.EnCode64(_cPUStr1) + "CnV" + Security.EnCode64(_cPUStr2);
            MixString = MixString.Replace("=","");
        }

        public string  GetKeyString()
        {
            
            string _MixStringt = MixString.Replace("CnV", "*");
            string[] strArray = _MixStringt.Split('*');

            if (strArray.Length < 2)
                return _keyString;
            string[] StrArr1 = Security.EnCode(strArray[0]+_CompanyName).Split('-');
            string[] StrArr2 = Security.EnCode(strArray[1]).Split('-');
            
            for (int i = 15; i >= 0; i--)
            {
                if (i % 2 == 0)
                    _keyString += StrArr1[i].ToString();
                else
                    _keyString += StrArr2[i].ToString();
            }
            return _keyString;
        }
    }
}
