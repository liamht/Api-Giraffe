using System.Linq;

namespace APIGiraffe.Data.Entities.Factory
{
    public class RequestDataFactory : IRequestDataFactory
    {
        private readonly IHeaderDataFactory _headerFactory;

        public RequestDataFactory(IHeaderDataFactory headerFactory)
        {
            _headerFactory = headerFactory;
        }

        public Request Create(Domain.Entities.Request request)
        {
            return new Request()
            {
                Headers = request.Headers.Select(_headerFactory.Create).ToList(),
                Id = request.Id,
                Name = request.RequestName,
                Url = request.Url,
                GroupId = request.GroupId
            };
        }
    }
}
