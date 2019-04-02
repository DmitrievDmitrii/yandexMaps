namespace ApiTests.Response
{
    public class OrganizationsResponse
    {
        public string Type { get; set; }

        public RequestProperties Properties { get; set; }

        public Feature[] Features { get; set; }
    }
}
