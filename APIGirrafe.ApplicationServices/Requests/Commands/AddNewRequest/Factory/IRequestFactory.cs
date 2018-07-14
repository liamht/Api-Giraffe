using APIGirrafe.Domain;

namespace APIGirrafe.ApplicationServices.Requests.Commands.AddNewRequest.Factory
{
    public interface IRequestFactory
    {
        Request Create(string name);
    }
}