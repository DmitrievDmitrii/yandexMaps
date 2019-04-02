using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace ApiTests
{
    public class HttpResponseWithTime
    {
        public HttpResponseMessage ResposseMessage { get; set; }

        public TimeSpan ResponseTime { get; set; }

        public T GetResponseObject<T>()
        {
            var json = this.ResposseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}