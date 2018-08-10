using System.Collections.Generic;
using System.Linq;

namespace APIGiraffe.Domain.Entities
{
    public class RequestGroup
    {
        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public List<Request> Requests { get; internal set; }

        public Data.Entities.RequestGroup ToDataEntity()
        {
            return new Data.Entities.RequestGroup()
            {
                Id = Id,
                Name = Name,
                Requests = Requests?.Select(c => c.ToDatabaseEntity()).ToList() ?? new List<Data.Entities.Request>()
            };
        }
    }
}
