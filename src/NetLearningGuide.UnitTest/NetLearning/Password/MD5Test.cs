using Shouldly;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.Password
{
    public class Md5Test
    {
        [Fact]
        public Task Md5UnitTest()
        {
            var userPassword = "ABCD";
            var inputPassword = "ABCD";
            var inputFailPassword = "ABCDE";
            var salt = (byte)Math.Abs(new object().GetHashCode() % 256);
            var guid = EncryptionMd5(userPassword, salt);
            DecryptionMd5(inputPassword, salt, guid).ShouldBeTrue();
            DecryptionMd5(inputFailPassword, salt, guid).ShouldBeFalse();
            return Task.CompletedTask;
        }
        private bool DecryptionMd5(string input, byte salt, Guid rmd5)
        {
            var arr = rmd5.ToByteArray();
            MD5 md5Provider = MD5.Create();
            input += salt;
            input += salt;
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = md5Provider.ComputeHash(bytes);
            for (int i = 1; i < 16; i++)
            {
                if (hash[i] != arr[i])
                {
                    return false;
                }
            }
            return true;
        }

        private Guid EncryptionMd5(string input, byte salt)
        {
            MD5 md5Provider = MD5.Create();
            input += salt;
            input += salt;
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = md5Provider.ComputeHash(bytes); 
            return new Guid(hash);
        }
    }
}