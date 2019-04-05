using System.Linq;
using ApiTests.Enums;
using ApiTests.Response;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace ApiTests
{
    [TestFixture]
    public class Tests
    {        

        [Test]
        public void Test()
        {
            var companyName = "Yandex";
            var builder = YandexMapsApi.GetBuilder
                .ApiKey(YandexMapsApi.Key)
                .Text(companyName)
                .Language(Language.en_US)
                .Type(Type.biz);
            var response = builder.GetResult().GetResponse();

            var organizations = response.GetResponseObject<OrganizationsResponse>();

            Assert.IsTrue(organizations.Features.Any(org => org.Properties.Name.Contains(companyName)),
                $"no companies with {companyName} found");
        }

        [Test]
        public void RestSharpTest()
        {
            var client = new RestClient(YandexMapsApi.ApiUrl);

            var companyName = "Yandex";
            var request = YandexMapsApi.GetBuilder
                .ApiKey(YandexMapsApi.Key)
                .Text(companyName)
                .Language(Language.en_US)
                .Type(Type.biz)
                .GetRequest();

            var response = client.Execute(request);
            var organizations = this.GetOrganitions(response);

            Assert.IsTrue(organizations.Features.Any(org => org.Properties.Name.Contains(companyName)),
                $"no companies with {companyName} found");
        }

        private OrganizationsResponse GetOrganitions(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<OrganizationsResponse>(response.Content);
        }

    }
}
