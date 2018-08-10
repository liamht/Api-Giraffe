using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIGiraffe.Domain.Entities
{
    public class Request
    {
        public int Id { get; internal set; }

        public int GroupId { get; internal set; }

        public string RequestName { get; internal set; }

        private IList<Header> _headers;

        public IReadOnlyList<Header> Headers => new ReadOnlyCollection<Header>(_headers);

        public string Url { get; internal set; }

        public Request()
        {
            _headers = new List<Header>();
        }

        public void AddHeader(Header header)
        {
            _headers.Add(header);
        }

        public Data.Entities.Request ToDatabaseEntity()
        {
            return new Data.Entities.Request()
            {
                Headers = Headers.Select(c => c.ToDataLayerObject()).ToList(),
                Id = Id,
                Name = RequestName,
                Url = Url,
                GroupId = GroupId
            };
        }

        public async Task<string> GetResponse()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(this.Url)
                };

                foreach (var header in Headers)
                {
                    request.Headers.Add(header.Name, header.Value);
                }

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
