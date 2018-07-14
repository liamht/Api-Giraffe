using System.Threading.Tasks;

namespace APIGirrafe.ApplicationServices.Requests.Queries.GetRequestDetails
{
    public interface IGetRequestDetailsQuery
    {
        Task<RequestDetails> ExecuteAsync(int requestId);
    }
}
