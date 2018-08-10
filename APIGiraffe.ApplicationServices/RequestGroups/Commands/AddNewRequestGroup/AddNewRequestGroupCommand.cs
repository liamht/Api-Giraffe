using System;
using APIGiraffe.Data.UnitOfWork;
using APIGiraffe.Domain.Factories;

namespace APIGiraffe.ApplicationServices.RequestGroups.Commands.AddNewRequestGroup
{
    public class AddNewRequestGroupCommand : IAddNewRequestGroupCommand
    {
        private readonly IUnitOfWork _uow;
        private readonly IRequestGroupFactory _factory;

        public AddNewRequestGroupCommand(IUnitOfWork uow, IRequestGroupFactory factory)
        {
            _uow = uow;
            _factory = factory;
        }

        public void Execute(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Cannot add null name into database");
            }

            var domainObject = _factory.Create(name);

            _uow.RequestGroups.Add(domainObject.ToDataEntity());
            _uow.SaveChanges();
        }
    }
}
