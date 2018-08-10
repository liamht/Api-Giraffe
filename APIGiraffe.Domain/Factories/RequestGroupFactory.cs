using APIGiraffe.Domain.Entities;
using System.Linq;

namespace APIGiraffe.Domain.Factories
{
    public class RequestGroupFactory : IRequestGroupFactory
    {
        public RequestGroup Create(string name)
        {
            return Create(name, null);
        }

        public RequestGroup Create(string name, IQueryable<Request> requests)
        {
            return Create(0, name, requests);
        }

        public RequestGroup Create(int id, string name, IQueryable<Request> requests)
        {
            return new RequestGroup()
            {
                Id = id,
                Name = name,
                Requests = requests?.ToList()
            };
        }        
    }
}
