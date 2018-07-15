using System.Threading.Tasks;

namespace APIGiraffe.ApplicationServices.Requests.Queries.GetRequestDetails
{
    public interface IGetRequestDetailsQuery
    {
        RequestDetails Execute(int requestId);
    }
}
