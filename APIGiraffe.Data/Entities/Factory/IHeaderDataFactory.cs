using APIGiraffe.Domain.Entities;

namespace APIGiraffe.Data.Entities.Factory
{
    public interface IHeaderDataFactory
    {
        Header Create(Domain.Entities.Header header);
    }
}