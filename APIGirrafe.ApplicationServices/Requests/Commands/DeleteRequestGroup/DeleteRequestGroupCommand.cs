﻿using APIGirrafe.Data.UnitOfWork;
using System.Linq;

namespace APIGirrafe.ApplicationServices.Requests.Commands.DeleteRequestGroup
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
            var requests = _uow.Requests.Where(c => c.GroupId == group.Id);

            _uow.Requests.RemoveRange(requests);
            _uow.RequestGroups.Remove(group);
            _uow.SaveChanges();
        }
    }
}
