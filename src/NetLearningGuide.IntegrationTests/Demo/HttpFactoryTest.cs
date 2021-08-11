using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NetLearningGuide.Core.HttpClientHelper;
using Xunit;

namespace NetLearningGuide.IntegrationTests.Demo
{
    public class HttpFactoryTest : TestBase
    {
        [Fact]
        public async Task HttpGetTest()
        {
            //new NetHttpClientFactory(new HttpClient());
            //await Run<INetHttpClientFactory>(async (http) =>
            //{
            //    var client = http.CreateClient(
            //          new HttpModel()
            //          {
            //              BaseUrl = "https://www.baidu.com/",
            //              Headers = new Dictionary<string, string>()
            //          });
            //    var result = await client.GetAsync("https://www.baidu.com/");
            //});
        }
    }
}
