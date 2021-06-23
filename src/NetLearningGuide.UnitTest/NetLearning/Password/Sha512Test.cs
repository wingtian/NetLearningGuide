using Shouldly;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.Password
{
    public class Sha512Test
    {
        [Fact]
        public Task Sha512UnitTest()
        {
            var userPassword = "ABCD";
            var inputPassword = "ABCD";
            var inputFailPassword = "ABCDE";
            var salt = (byte)Math.Abs(new object().GetHashCode() % 256);
            var guid = Encryption(userPassword, salt);
            Decryption(inputPassword, salt, guid).ShouldBeTrue();
            Decryption(inputFailPassword, salt, guid).ShouldBeFalse();
            return Task.CompletedTask;
        }
        private bool Decryption(string input, byte salt, Guid password)
        {
            var arr = password.ToByteArray();
            SHA512 provider = SHA512.Create();
            input += salt;
            input += salt;
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = provider.ComputeHash(bytes);
            for (int i = 1; i < 16; i++)
            {
                if (hash[i] != arr[i])
                {
                    return false;
                }
            }
            return true;
        }

        private Guid Encryption(string input, byte salt)
        {
            SHA512 provider = SHA512.Create();
            input += salt;
            input += salt;
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = provider.ComputeHash(bytes);
            var finalByte = new byte[16];
            for (int i = 1; i < 16; i++)
            {
                finalByte[i] = hash[i];
            }
            return new Guid(finalByte);
        }
    }
}
