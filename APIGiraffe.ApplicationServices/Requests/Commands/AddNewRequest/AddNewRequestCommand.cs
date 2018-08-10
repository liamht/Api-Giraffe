using System;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using APIGiraffe.Domain.Factories;
using APIGiraffe.Data.Entities.Factory;

namespace APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequest
{
    public class AddNewRequestCommand : IAddNewRequestCommand
    {
        private readonly IUnitOfWork _uow;
        private readonly IRequestFactory _factory;
        private readonly IRequestDataFactory _dataFactory;

        public AddNewRequestCommand(IUnitOfWork uow, IRequestFactory factory, IRequestDataFactory dataFactory)
        {
            _uow = uow;
            _factory = factory;
            _dataFactory = dataFactory;
        }

        public void Execute(int groupId, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "name cannot be null when added to database");
            }

            var group = _uow.RequestGroups.Include(c => c.Requests).Single(c => c.Id == groupId);

            var domainObject = _factory.Create(groupId, name);

            group.Requests.Add(_dataFactory.Create(domainObject));

            _uow.RequestGroups.Update(group);
            _uow.SaveChanges();
        }
    }
}
