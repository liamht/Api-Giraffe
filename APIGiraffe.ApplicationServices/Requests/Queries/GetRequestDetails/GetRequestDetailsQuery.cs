using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace APIGiraffe.ApplicationServices.Requests.Queries.GetRequestDetails
{
    public class GetRequestDetailsQuery : IGetRequestDetailsQuery
    {
        private readonly IUnitOfWork _uow;

        public GetRequestDetailsQuery(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public RequestDetails Execute(int requestId)
        {
            var request = _uow.Requests
                    .Include(c => c.Headers)
                    .Single(c => c.Id == requestId);

            var headers = request.Headers.Select(header => new Header()
            {
                Id = header.Id,
                Name = header.Name,
                Value = header.Value
            }).ToList();

            return new RequestDetails()
            {
                Id = request.Id,
                Headers = headers,
                RequestName = request.Name,
                Url = request.Url
            };
        }
    }
}
