using System.Linq;
using ApiTests.Enums;
using ApiTests.Response;
using NUnit.Framework;

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

    }
}
