using System.Linq;

namespace APIGiraffe.Data.Entities.Factory
{
    public class RequestGroupDataFactory : IRequestGroupDataFactory
    {
        private readonly IRequestDataFactory _requestFactory;

        public RequestGroupDataFactory(IRequestDataFactory requestFactory)
        {
            _requestFactory = requestFactory;
        }

        public RequestGroup Create(Domain.Entities.RequestGroup requestGroup)
        {
            return new RequestGroup()
            {
                Id = requestGroup.Id,
                Name = requestGroup.Name,
                Requests = requestGroup.Requests.Select(_requestFactory.Create).ToList()
            };
        }
    }
}
