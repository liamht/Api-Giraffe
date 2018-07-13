using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APIGirrafe.Data.Entities;

namespace APIGirrafe.Domain
{
    public class SoapRequest
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public string RequestName { get; set; }

        private IList<Header> _headers;
        public IReadOnlyList<Header> Headers => new ReadOnlyCollection<Header>(_headers);

        public string Url { get; set; }

        public void AddHeader(Header header)
        {
            _headers.Add(header);
        }
        
        internal static SoapRequest FromDatabaseEntity(Request entity)
        {
            return new SoapRequest()
            {
                Id = entity.Id,
                GroupId = entity.GroupId,
                RequestName = entity.Name,
                Url = entity.Url,
                _headers = entity.Headers?.Select(Header.FromDataLayerObject).ToList() ?? new List<Header>()
            };
        }

        internal Request ToDatabaseEntity()
        {
            return new Request()
            {
                // todo
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
                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
