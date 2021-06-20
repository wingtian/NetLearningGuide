using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.Password
{
    public class MD5Test
    {
        [Fact]
        public Task Md5UnitTest()
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.GetEncoding("ABC").GetBytes("EED"));
           return Task.CompletedTask;
        }

        private string EncryptionMD5()
        {
            return "";
        }

        private string DecryptionMD5()
        {
            return "";
        }
    }
}