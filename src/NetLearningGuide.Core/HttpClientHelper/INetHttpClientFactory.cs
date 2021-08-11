using System.Net.Http;

namespace NetLearningGuide.Core.HttpClientHelper
{
    public interface INetHttpClientFactory
    {
        HttpClient CreateClient(HttpModel model);
    }
}
