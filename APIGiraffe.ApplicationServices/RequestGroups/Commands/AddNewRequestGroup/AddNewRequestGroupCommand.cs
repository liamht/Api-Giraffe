using System;
using APIGiraffe.Data.Entities.Factory;
using APIGiraffe.Data.UnitOfWork;
using APIGiraffe.Domain.Factories;

namespace APIGiraffe.ApplicationServices.RequestGroups.Commands.AddNewRequestGroup
{
    public class AddNewRequestGroupCommand : IAddNewRequestGroupCommand
    {
        private readonly IUnitOfWork _uow;
        private readonly IRequestGroupFactory _factory;
        private readonly IRequestGroupDataFactory _dataFactory;

        public AddNewRequestGroupCommand(IUnitOfWork uow, IRequestGroupFactory factory, IRequestGroupDataFactory dataFactory)
        {
            _uow = uow;
            _factory = factory;
            _dataFactory = dataFactory;
        }

        public void Execute(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Cannot add null name into database");
            }

            var domainObject = _factory.Create(name);

            _uow.RequestGroups.Add(_dataFactory.Create(domainObject));
            _uow.SaveChanges();
        }
    }
}
