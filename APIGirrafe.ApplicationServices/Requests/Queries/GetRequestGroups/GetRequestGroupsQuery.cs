using APIGirrafe.Data.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace APIGirrafe.ApplicationServices.Requests.Queries.GetRequestGroups
{
    public class GetRequestGroupsQuery : IGetRequestGroupsQuery
    {
        private readonly IUnitOfWork _uow;

        public GetRequestGroupsQuery(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<RequestGroup> Execute()
        {
            return _uow.RequestGroups.Select(group =>
                 new RequestGroup()
                 {
                     Name = group.Name,
                     Id = group.Id,
                     Requests = group.Requests.Select(request => new RequestIdWithName()
                     {
                         Id = request.Id,
                         Name = request.Name
                     }).ToList()
                 }).ToList();
        }
    }
}
