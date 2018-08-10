using APIGiraffe.Domain.Entities;
using System.Collections.Generic;

namespace APIGiraffe.Domain.Factories
{
    public class RequestFactory : IRequestFactory
    {
        public Request Create(int groupid, string name, string url, List<Header> headers)
        {
            return Create(0, groupid, name, url, headers);
        }

        public Request Create(int groupid, string name)
        {
            return Create(groupid, name, null, null);
        }

        public Request Create(int id, int groupid, string name, string url, List<Header> headers)
        {
            var request = new Request()
            {
                GroupId = groupid,
                Id = id,
                RequestName = name,
                Url = url,
            };

            headers?.ForEach(header =>
            {
                request.AddHeader(header);
            });

            return request;
        }

        public Request Create(string url, List<Header> headers)
        {
            var request = new Request()
            {
                Url = url,
            };

            headers?.ForEach(header =>
            {
                request.AddHeader(header);
            });

            return request;
        }
    }
}
