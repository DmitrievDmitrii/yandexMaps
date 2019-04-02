namespace ApiTests.Response
{
    public class SearchRequest
    {
        public string Request { get; set; }

        public int Skip { get; set; }

        public int Result { get; set; }

        public double[][] BoundedBy { get; set; }
    }
}
