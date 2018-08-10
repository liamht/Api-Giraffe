using APIGiraffe.Domain.Entities;

namespace APIGiraffe.Data.Entities.Factory
{
    public interface IRequestDataFactory
    {
        Request Create(Domain.Entities.Request request);
    }
}