using System.Collections.Generic;
using System.Linq;
using APIGiraffe.Data.Entities;

namespace APIGiraffe.Domain
{
    public class RequestGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Request> Requests { get; set; }

        public Data.Entities.RequestGroup ToDataEntity()
        {
            return new Data.Entities.RequestGroup()
            {
                Id = Id,
                Name = Name,
                Requests = Requests?.Select((System.Func<Request, Data.Entities.Request>)(c => c.ToDatabaseEntity())).ToList() ?? new List<Data.Entities.Request>()
            };
        }

        internal static RequestGroup FromDataEntity(Data.Entities.RequestGroup entity)
        {
            return new RequestGroup()
            {
                Id = entity.Id,
                Name = entity.Name,
                Requests = entity?.Requests?.Select(Request.FromDatabaseEntity).ToList() ?? new List<Request>()
            };
        }
    }
}
