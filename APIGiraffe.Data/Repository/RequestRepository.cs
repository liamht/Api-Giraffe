using System.Linq;
using Microsoft.EntityFrameworkCore;
using APIGiraffe.Data.Entities;
using APIGiraffe.Data.UnitOfWork;

namespace APIGiraffe.Data.Repository
{
    public class RequestRepository : IRepository<Request>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Request entity)
        {
            var requestGroup = _unitOfWork.RequestGroups.Include(c => c.Requests).Single(c => c.Id == entity.GroupId);
            requestGroup.Requests.Add(entity);
            _unitOfWork.SaveChanges();
        }

        public void Delete(Request entity)
        {
            _unitOfWork.Requests.Remove(entity);
            _unitOfWork.SaveChanges();
        }

        public IQueryable<Request> Get()
        {
            return _unitOfWork.Requests.Include(c => c.Headers);
        }

        public void Update(Request request)
        {
            var entity = Get().Single(c => c.Id == request.Id);

            entity.Url = request.Url;
            entity.Headers = request.Headers;
            _unitOfWork.Requests.Update(entity);

            _unitOfWork.SaveChanges();
        }
    }
}
