using System.Collections.Generic;
using System.Linq;
using APIGirrafe.Data.Repository;

namespace APIGirrafe.Domain.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Data.Entities.RequestGroup> _repository;

        public GroupService(IRepository<Data.Entities.RequestGroup> repository)
        {
            _repository = repository;
        }

        public List<RequestGroup> FetchFromDatabase()
        {
            var items =  _repository.Get().AsEnumerable().Select(RequestGroup.FromDataEntity).ToList();
            return items;
        }

        public void AddNewGroup(RequestGroup requestGroup)
        {
            _repository.Add(requestGroup.ToDataEntity());
        }

        public void Delete(RequestGroup requestGroup)
        {
            _repository.Delete(requestGroup.ToDataEntity());
        }
    }
}
