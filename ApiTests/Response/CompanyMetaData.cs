namespace ApiTests.Response
{
    public class CompanyMetaData
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Url { get; set; }

        public Category[] Categories { get; set; }

        public Phone[] Phones { get; set; }

        public Hours Hours { get; set; }
    }
}
