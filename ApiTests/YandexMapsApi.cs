using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using ApiTests.Constants;
using ApiTests.Enums;
using Type = ApiTests.Enums.Type;

namespace ApiTests
{
    public class YandexMapsApi
    {
        private RequestSpecification requestSpecification =
            new RequestSpecification().SetBaseUri("https://search-maps.yandex.ru/v1/");

        private ResponseSpecification responseSpecification =
            new ResponseSpecification().ExpectResponseTime(TimeSpan.FromSeconds(2)).ExpectStatusCode(HttpStatusCode.OK);

        public const string Key = "1bd24c37-57ae-4f2e-b9d6-4d860dd9f4ad";

        private Uri uri;

        private Uri BaseUri;

        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        private YandexMapsApi() { }

        public static ApiBuilder GetBuilder => new ApiBuilder(new YandexMapsApi());

        public HttpResponseWithTime GetResponse()
        {
            var content = new FormUrlEncodedContent(this.parameters).ReadAsStringAsync().Result;
            
            HttpResponseMessage response;
            TimeSpan responseTime;

            using (HttpClient client = this.requestSpecification.GetClient())
            {
                //var requestUri = new Uri(client.BaseAddress, $"?{content}");
                var stopWatch = Stopwatch.StartNew();
                response = client.GetAsync($"?{content}").Result;
                responseTime = stopWatch.Elapsed;
            }

            var responseWithTime = new HttpResponseWithTime {ResponseTime = responseTime, ResposseMessage = response};

            this.responseSpecification.CheckResponse(responseWithTime);

            return responseWithTime;
        }

        public class ApiBuilder
        {
            private readonly YandexMapsApi mapsApi;

            public YandexMapsApi GetResult() => this.mapsApi;

            public ApiBuilder(YandexMapsApi yandexMapsApi)
            {
                this.mapsApi = yandexMapsApi;
            }

            public ApiBuilder Text(string text)
            {
                this.mapsApi.parameters.Add(Parameters.Text, text);
                return this;
            }

            public ApiBuilder ApiKey(string apiKey)
            {
                this.mapsApi.parameters.Add(Parameters.Apikey, apiKey);
                return this;
            }

            public ApiBuilder Type(Type type)
            {
                this.mapsApi.parameters.Add(Parameters.Type, type.ToString());
                return this;
            }

            public ApiBuilder Language(Language lang)
            {
                this.mapsApi.parameters.Add(Parameters.Language, lang.ToString());
                return this;
            }

            public ApiBuilder LongitudeLatitude(string longitudeLatitude)
            {
                this.mapsApi.parameters.Add(Parameters.LongitudeLatitude, longitudeLatitude.ToString());
                return this;
            }

            public ApiBuilder Span(string span)
            {
                this.mapsApi.parameters.Add(Parameters.Span, span.ToString());
                return this;
            }

            public ApiBuilder BordersBox(string bordersBox)
            {
                this.mapsApi.parameters.Add(Parameters.BordersBox, bordersBox.ToString());
                return this;
            }

            public ApiBuilder RestrictedSpan(bool restrictedSpan)
            {
                this.mapsApi.parameters.Add(Parameters.RestrictedSpan, restrictedSpan ? "1" : "0");
                return this;
            }

            public ApiBuilder Results(int results)
            {
                this.mapsApi.parameters.Add(Parameters.Results, results.ToString());
                return this;
            }

            public ApiBuilder Skip(int skip)
            {
                this.mapsApi.parameters.Add(Parameters.Skip, skip.ToString());
                return this;
            }

            public ApiBuilder Callback(string callback)
            {
                this.mapsApi.parameters.Add(Parameters.Callback, callback);
                return this;
            }

            public ApiBuilder RequestSpecification(RequestSpecification specification)
            {
                this.mapsApi.requestSpecification = specification;
                return this;
            }

            public ApiBuilder ResponseSpecification(ResponseSpecification specification)
            {
                this.mapsApi.responseSpecification = specification;
                return this;
            }
        }
    }
}
