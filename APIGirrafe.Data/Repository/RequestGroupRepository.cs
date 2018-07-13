using System.Linq;
using Microsoft.EntityFrameworkCore;
using APIGirrafe.Data.Entities;
using APIGirrafe.Data.UnitOfWork;

namespace APIGirrafe.Data.Repository
{
    public class RequestGroupRepository : IRepository<RequestGroup>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestGroupRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(RequestGroup entity)
        {
            _unitOfWork.RequestGroups.Add(entity);
            _unitOfWork.SaveChanges();
        }

        public void Delete(RequestGroup entity)
        {
            var group = _unitOfWork.RequestGroups.Single(c => c.Id == entity.Id);
            _unitOfWork.RequestGroups.Remove(group);
            _unitOfWork.SaveChanges();
        }

        public IQueryable<RequestGroup> Get()
        {
            return _unitOfWork.RequestGroups.Include(c => c.Requests);
        }

        public void Update(RequestGroup entity)
        {
            _unitOfWork.RequestGroups.Update(entity);
            _unitOfWork.SaveChanges();
        }
    }
}
