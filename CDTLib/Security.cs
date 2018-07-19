using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace CDTLib
{
    public static class Security
    {
        public static string EnCode(string originalPassword)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }

        public static string EnCode64(string originalString)
        {
            byte[] Enc = ASCIIEncoding.UTF7.GetBytes(originalString);
            string EncString = Convert.ToBase64String(Enc);
            return EncString;
        }

        public static string DeCode64(string orginalString)
        {
            byte[] Dec = Convert.FromBase64String(orginalString);
            string DecString = ASCIIEncoding.UTF7.GetString(Dec);
            return DecString;
        }
    }
}
