using Shouldly;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace NetLearningGuide.UnitTest.NetLearning.InCommonUse
{
    public class ConvertTest
    {
        [Fact]
        public Task StreamToBytesCase1()
        { 
            var stream = new MemoryStream();
            var bt = StreamToBytes(stream);
            bt.ShouldNotBeNull();
            return Task.CompletedTask;
        }
        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        private byte[] StreamToBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
    }
}
