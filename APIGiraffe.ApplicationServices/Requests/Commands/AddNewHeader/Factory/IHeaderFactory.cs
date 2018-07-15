using APIGiraffe.Domain;

namespace APIGiraffe.ApplicationServices.Requests.Commands.AddNewHeader.Factory
{
    public interface IHeaderFactory
    {
        Header Create(string name, string value);
    }
}