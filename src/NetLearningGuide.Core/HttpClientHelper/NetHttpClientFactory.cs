using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NetLearningGuide.Core.HttpClientHelper
{
    public class NetHttpClientFactory : INetHttpClientFactory
    {
        private IHttpClientFactory HttpClientFactory { get; }

        public NetHttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public HttpClient CreateClient(HttpModel inModel)
        {
            var http = HttpClientFactory.CreateClient();
            http.BaseAddress = new Uri(inModel.BaseUrl);
            if (inModel.Headers != null)
                foreach (var item in inModel.Headers)
                    http.DefaultRequestHeaders.Add(item.Key, item.Value);
            if (inModel.Token != null && !String.IsNullOrEmpty(inModel.Token.TokenString))
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(inModel.Token.TokenType, inModel.Token.TokenString);
            return http; 
        }
    }
    /// <summary>
    /// 提供常用的 Url屬性
    /// </summary>
    public class HttpModel
    {
        public string BaseUrl { get; set; }
        /// <summary>
        /// 默認請求頭
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        public TokenModel Token { get; set; }
    }
    public class TokenModel
    {
        public string TokenType { get; set; } = "Bearer";
        public string TokenString { get; set; }
    }
}
