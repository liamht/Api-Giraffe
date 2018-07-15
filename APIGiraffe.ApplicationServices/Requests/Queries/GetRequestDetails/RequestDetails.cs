using System.Collections.Generic;

namespace APIGiraffe.ApplicationServices.Requests.Queries.GetRequestDetails
{
    public class RequestDetails
    {
        public int Id { get; internal set; }

        public string RequestName { get; internal set; }

        public string Url { get; internal set; }

        public List<Header> Headers { get; internal set; }
    }
}
