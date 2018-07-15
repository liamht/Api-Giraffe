using System;
using APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequestGroup.Factory;
using APIGiraffe.Data.UnitOfWork;

namespace APIGiraffe.ApplicationServices.Requests.Commands.AddNewRequestGroup
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
