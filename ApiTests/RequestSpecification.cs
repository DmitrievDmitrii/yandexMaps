using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ApiTests
{
    public class RequestSpecification
    {
        private Uri baseUri;
        private string acceptContentType;
        private Dictionary<string, string> headers = new Dictionary<string, string>();

        public RequestSpecification SetBaseUri(string uri)
        {
            this.baseUri = new Uri(uri);
            return this;
        }

        public RequestSpecification Accept(string contentType)
        {
            this.acceptContentType = contentType;
            return this;
        }

        public RequestSpecification AddHeader(string key, string value)
        {
            this.headers.Add(key, value);
            return this;
        }

        public HttpClient GetClient()
        {
            var client = new HttpClient();
            if (this.baseUri != null)
            {
                client.BaseAddress = this.baseUri;
            }

            if (this.acceptContentType != null)
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.acceptContentType));
            }
            
            foreach (var header in this.headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            return client;
        }
    }
}
