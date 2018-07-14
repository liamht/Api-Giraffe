using APIGirrafe.Domain;

namespace APIGirrafe.ApplicationServices.Requests.Commands.AddNewHeader.Factory
{
    public interface IHeaderFactory
    {
        Header Create(string name, string value);
    }
}