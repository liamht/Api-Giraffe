using System;
using System.Collections.Generic;
using System.Text;
using APIGiraffe.Data.UnitOfWork;

namespace APIGiraffe.ApplicationServices.Requests.Commands.RenameRequestGroup
{
    public class RenameRequestGroupCommand
    {
        private IUnitOfWork _uow;

        public RenameRequestGroupCommand(IUnitOfWork uow)
        {
            _uow = _uow;
        }

        public void Execute(int requestGroupId, string newName)
        {

        }
    }
}
