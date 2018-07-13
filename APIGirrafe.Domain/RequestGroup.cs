using System.Collections.Generic;
using System.Linq;
using APIGirrafe.Data.Entities;

namespace APIGirrafe.Domain
{
    public class RequestGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<SoapRequest> Requests { get; set; }

        internal Data.Entities.RequestGroup ToDataEntity()
        {
            return new Data.Entities.RequestGroup()
            {
                Id = Id,
                Name = Name,
                Requests = Requests?.Select(c => c.ToDatabaseEntity()).ToList() ?? new List<Request>()
            };
        }

        internal static RequestGroup FromDataEntity(Data.Entities.RequestGroup entity)
        {
            return new RequestGroup()
            {
                Id = entity.Id,
                Name = entity.Name,
                Requests = entity?.Requests?.Select(SoapRequest.FromDatabaseEntity).ToList() ?? new List<SoapRequest>()
            };
        }
    }
}
