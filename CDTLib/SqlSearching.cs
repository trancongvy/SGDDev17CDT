using System;
using System.Collections.Generic;
using System.Text;

namespace CDTLib
{
    public class SqlSearching
    {
        public string GenSqlFromGridFilter(string strFilter)
        {
            if (strFilter.Contains("?"))
                return string.Empty;
            if(strFilter.Contains(" Like '"))
            {
                updateLike(ref strFilter);                
            }
            if (strFilter.Contains("StartsWith(") )
            {
                UpdateStarWith(ref strFilter);
            }
            if (strFilter.Contains("EndsWith("))
            {
                UpdateEndWith(ref strFilter);
            }
            if (strFilter.Contains("Contains("))
            {
                int start = strFilter.IndexOf("Contains(");
                int end = 0;
                for (int i = start; i < strFilter.Length; i++)
                {
                    if (strFilter[i] == ')')
                    {
                        end = i;
                        string s = strFilter.Substring(start, end - start + 1);
                        string[] sl;
                        string r = s.Replace("Contains(", "");
                        r = r.Replace(")", "");
                        sl = r.Split(",".ToCharArray());
                        r = sl[0] + " like '%" + sl[1].Trim().Replace("'", "") + "%' ";
                        strFilter = strFilter.Replace(s, r);
                    }
                }
            }
            strFilter = strFilter.Replace("#", "'");
            strFilter = strFilter.Replace("{", "'");
            strFilter = strFilter.Replace("}", "'");
            strFilter = strFilter.Replace("True", "1");
            strFilter = strFilter.Replace("False", "0");
            strFilter = strFilter.Replace("0m", "0");
            if (strFilter.Contains("Between"))
                UpdateBetweenOperator(ref strFilter);
            
            return (strFilter);
        }
        private void updateLike(ref string strFilter)
        {
            int start = strFilter.IndexOf(" Like '");
            int end = 0;
            for (int i = start + 7; i < strFilter.Length; i++)
            {
                if (strFilter[i].ToString() == "'")
                {
                    end = i;
                    string s = strFilter.Substring(start + 7, end - (start + 7));
                    string S = s.ToUpper();
                    strFilter = strFilter.Replace(s, "%" + S + "%");
                    break;
                }
            }
            if (strFilter.Contains(" Like '"))
            {
                updateLike(ref strFilter);
            }
        }
        private void UpdateEndWith(ref string strFilter)
        {

            int start = strFilter.IndexOf("EndsWith(");
            int end = 0;
            for (int i = start; i < strFilter.Length; i++)
            {
                if (strFilter[i] == ')')
                {
                    end = i;
                    string s = strFilter.Substring(start, end - start +1);
                    string[] sl;
                    string r = s.Replace("EndsWith(", "");
                    r = r.Replace(")", "");
                    sl = r.Split(",".ToCharArray());
                    r = sl[0] + " like '%" + sl[1].Trim().Replace("'", "") + "' ";
                    strFilter = strFilter.Replace(s, r);

                    break;
                }
            }
            if (strFilter.Contains("EndsWith("))
                UpdateEndWith(ref strFilter);
        }
        private void UpdateStarWith(ref string strFilter)
        {
            
            int start = strFilter.IndexOf("StartsWith(");
            int end = 0;
            for (int i = start; i < strFilter.Length; i++)
            {
                if (strFilter[i] == ')')
                {
                    end = i;
                    string s = strFilter.Substring(start, end - start + 1);
                    string[] sl;
                    string r = s.Replace("StartsWith(", "");
                    r = r.Replace(")", "");
                    sl = r.Split(",".ToCharArray());
                    r = sl[0] + " like '" + sl[1].Trim().Replace("'","") + "%' ";
                    strFilter = strFilter.Replace(s, r);
                    
                    break;
                }
            }
            if (strFilter.Contains("StartsWith("))
                UpdateStarWith(ref strFilter);
        }
        private void UpdateBetweenOperator(ref string strFilter)
        {
            int bIndex = strFilter.IndexOf("Between");
            int bStart = -1, bEnd = -1;
            for (int i = bIndex; i >= 0; i--)
                if (strFilter[i] == '[')
                {
                    bStart = i;
                    break;
                }
            for (int i = bIndex; i < strFilter.Length; i++)
                if (strFilter[i] == ')')
                {
                    bEnd = i;
                    break;
                }
            if (bStart >= 0 && bEnd >= 0)
            {
                string bString = strFilter.Substring(bStart, bEnd - bStart + 1);
                string bStringNew = bString.Replace("Between", "between");
                bStringNew = bStringNew.Replace("(", " ");
                bStringNew = bStringNew.Replace(",", " and ");
                bStringNew = "(" + bStringNew;
                strFilter = strFilter.Replace(bString, bStringNew);
            }
            if (strFilter.Contains("Between"))
                UpdateBetweenOperator(ref strFilter);
        }
    }
}
