using APIGiraffe.Domain.Entities;

namespace APIGiraffe.Domain.Factories
{
    public interface IHeaderFactory
    {
        Header Create(int id, string name, string value);
        Header Create(string name, string value);
    }
}