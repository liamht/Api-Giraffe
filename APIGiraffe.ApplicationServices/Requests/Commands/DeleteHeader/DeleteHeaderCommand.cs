using APIGiraffe.Data.UnitOfWork;

namespace APIGiraffe.ApplicationServices.Requests.Commands.DeleteHeader
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

            _uow.Headers.Remove(header);
            _uow.SaveChanges();
        }
    }
}
