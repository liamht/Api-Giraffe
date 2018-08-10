using System.Linq;
using APIGiraffe.Domain.Entities;

namespace APIGiraffe.Domain.Factories
{
    public interface IRequestGroupFactory
    {
        RequestGroup Create(int id, string name, IQueryable<Request> requests);
        RequestGroup Create(string name);
        RequestGroup Create(string name, IQueryable<Request> requests);
    }
}