using System;
using System.Linq;
using System.Net;
using NUnit.Framework;

namespace ApiTests
{
    public class ResponseSpecification
    {
        private string expectContentType;
        private string expectHeader;
        private HttpStatusCode expectStatusCode;
        private TimeSpan expectResponseTime = TimeSpan.MaxValue;

        public ResponseSpecification ExpectContentType(string contentType)
        {
            this.expectContentType = contentType;
            return this;
        }

        public ResponseSpecification ExpectHeader(string header)
        {
            this.expectHeader = header;
            return this;
        }

        public ResponseSpecification ExpectStatusCode(HttpStatusCode statusCode)
        {
            this.expectStatusCode = statusCode;
            return this;
        }

        public ResponseSpecification ExpectResponseTime(TimeSpan responseTime)
        {
            this.expectResponseTime = responseTime;
            return this;
        }

        public void CheckResponse(HttpResponseWithTime response) 
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(response.ResponseTime < this.expectResponseTime,
                    $"response time {response.ResponseTime} is more than {this.expectResponseTime}");
                Assert.AreEqual(this.expectStatusCode, response.ResposseMessage.StatusCode, "not expected status code");

                if (this.expectContentType != null)
                {
                    Assert.IsTrue(response.ResposseMessage.Headers.FirstOrDefault(header => header.Key == "Content-Type")
                        .Value.Any(content => content == this.expectContentType),
                    $"content type {this.expectContentType} not found in response");
                }

                if (this.expectHeader != null)
                {
                    Assert.IsTrue(response.ResposseMessage.Headers.Any(header => header.Key == this.expectHeader),
                    $"{this.expectHeader} header not found");
                    
                }
            });
        }
    }
}
