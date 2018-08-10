using APIGiraffe.Domain.Entities;

namespace APIGiraffe.Data.Entities.Factory
{
    public interface IRequestGroupDataFactory
    {
        RequestGroup Create(Domain.Entities.RequestGroup requestGroup);
    }
}