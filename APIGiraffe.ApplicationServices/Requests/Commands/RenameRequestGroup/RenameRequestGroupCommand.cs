using System;
using APIGiraffe.Data.UnitOfWork;

namespace APIGiraffe.ApplicationServices.Requests.Commands.RenameRequestGroup
{
    public class RenameRequestGroupCommand : IRenameRequestGroupCommand
    {
        private IUnitOfWork _uow;

        public RenameRequestGroupCommand(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Execute(int requestGroupId, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(newName));
            }

            var group = _uow.RequestGroups.Find(requestGroupId);

            if (group == null)
            {
                throw new ArgumentOutOfRangeException(nameof(requestGroupId), "Could not find group with the given Id");
            }

            group.Name = newName;

            _uow.RequestGroups.Update(group);
            _uow.SaveChanges();
        }
    }
}
