using APIGiraffe.Data.UnitOfWork;
using System;

namespace APIGiraffe.ApplicationServices.Requests.Commands.DeleteRequest
{
    public class DeleteRequestCommand : IDeleteRequestCommand
    {
        private readonly IUnitOfWork _uow;

        public DeleteRequestCommand(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Execute(int id)
        {
            var request = _uow.Requests.Find(id);

            if (request == null)
            {
                throw new ArgumentOutOfRangeException("Cannot find request with the given Id");
            }

            _uow.Requests.Remove(request);
            _uow.SaveChanges();
        }
    }
}
