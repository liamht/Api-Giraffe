using APIGirrafe.Domain;

namespace APIGirrafe.ApplicationServices.Requests.Commands.AddNewRequest.Factory
{
    public class RequestFactory : IRequestFactory
    {
        public Request Create(string name)
        {
            return new Request()
            {
                RequestName = name
            };
        }
    }
}
