using System.Linq;
using APIGiraffe.Data.Entities;

namespace APIGiraffe.Data.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> Get();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
