using System;
using APIGiraffe.Data.UnitOfWork;

namespace APIGiraffe.ApplicationServices.Headers.Commands.DeleteHeader
{
    public class DeleteHeaderCommand : IDeleteHeaderCommand
    {
        private readonly IUnitOfWork _uow;

        public DeleteHeaderCommand(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Execute(int headerId)
        {
            var header = _uow.Headers.Find(headerId);

            if (header == null)
            {
                throw new ArgumentOutOfRangeException(nameof(headerId), "Cannot find requestGroup in Db");
            }

            _uow.Headers.Remove(header);
            _uow.SaveChanges();
        }
    }
}
