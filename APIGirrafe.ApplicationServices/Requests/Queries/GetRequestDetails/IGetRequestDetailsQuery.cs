using System.Threading.Tasks;

namespace APIGirrafe.ApplicationServices.Requests.Queries.GetRequestDetails
{
    public interface IGetRequestDetailsQuery
    {
        RequestDetails Execute(int requestId);
    }
}
