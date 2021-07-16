using System;
using System.Security.Cryptography;
using System.Text;

namespace NetLearningGuide.Util
{
    public static class CryptographyExtension
    {
        public static string ToMd5(this string strToEncrypt, bool RemovHorizontaLine = true)
        {
            using (MD5 md5 = MD5.Create())
            {
                string str = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(strToEncrypt)));
                if (RemovHorizontaLine)
                    str = str.Replace("-", "");
                return str;
            }
        }
    }
}
