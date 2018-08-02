using APIGiraffe.Data.UnitOfWork;
using System;

namespace APIGiraffe.ApplicationServices.Requests.Commands.RenameRequest
{
    public class RenameRequestCommand : IRenameRequestCommand
    {
        private IUnitOfWork _uow;

        public RenameRequestCommand(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Execute(int id, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(newName));
            }

            var item = _uow.Requests.Find(id);
            if (item == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Could not find group with the given Id");
            }

            item.Name = newName;

            _uow.Requests.Update(item);
            _uow.SaveChanges();
        }
    }
}
