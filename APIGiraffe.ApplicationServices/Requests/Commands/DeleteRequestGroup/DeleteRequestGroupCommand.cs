using System;
using APIGiraffe.Data.UnitOfWork;
using System.Linq;

namespace APIGiraffe.ApplicationServices.Requests.Commands.DeleteRequestGroup
{
    public class DeleteRequestGroupCommand : IDeleteRequestGroupCommand
    {
        private readonly IUnitOfWork _uow;

        public DeleteRequestGroupCommand(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Execute(int requestGroupId)
        {
            var group = _uow.RequestGroups.Find(requestGroupId);

            if (group == null)
            {
                throw new ArgumentOutOfRangeException(nameof(requestGroupId), "Cannot find requestGroup in Db");
            }

            var requests = _uow.Requests.Where(c => c.GroupId == group.Id);

            _uow.Requests.RemoveRange(requests);
            _uow.RequestGroups.Remove(group);
            _uow.SaveChanges();
        }
    }
}
