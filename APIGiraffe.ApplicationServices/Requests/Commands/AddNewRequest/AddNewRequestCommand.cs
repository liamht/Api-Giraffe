using APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequest.Factory;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequest
{
    public class AddNewRequestCommand : IAddNewRequestCommand
    {
        private readonly IUnitOfWork _uow;
        private readonly IRequestFactory _factory;

        public AddNewRequestCommand(IUnitOfWork uow, IRequestFactory factory)
        {
            _uow = uow;
            _factory = factory;
        }

        public void Execute(int groupId, string name)
        {
            var domainObject = _factory.Create(name);

            var group = _uow.RequestGroups.Include(c => c.Requests).Single(c => c.Id == groupId);
            group.Requests.Add(domainObject.ToDatabaseEntity());

            _uow.RequestGroups.Update(group);
            _uow.SaveChanges();
        }
    }
}
