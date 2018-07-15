using APIGiraffe.Domain;

namespace APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequest.Factory
{
    public interface IRequestFactory
    {
        Request Create(string name);
    }
}