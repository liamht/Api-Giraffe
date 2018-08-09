using APIGiraffe.Domain;

namespace APIGiraffe.ApplicationServices.Headers.Commands.AddNewHeader.Factory
{
    public interface IHeaderFactory
    {
        Header Create(string name, string value);
    }
}