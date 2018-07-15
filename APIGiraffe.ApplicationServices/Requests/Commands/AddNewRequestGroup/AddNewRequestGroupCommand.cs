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
            var domainObject = _factory.Create(name);

            _uow.RequestGroups.Add(domainObject.ToDataEntity());
            _uow.SaveChangesAsync();
        }
    }
}
