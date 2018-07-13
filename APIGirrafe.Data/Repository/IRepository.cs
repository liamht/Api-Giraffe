using System.Linq;
using APIGirrafe.Data.Entities;

namespace APIGirrafe.Data.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> Get();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
