using System;
using APIGiraffe.Data.UnitOfWork;

namespace APIGiraffe.ApplicationServices.Requests.Commands.UpdateRequest
{
    public class UpdateRequestCommand : IUpdateRequestCommand
    {
        private readonly IUnitOfWork _uow;

        public UpdateRequestCommand(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Execute(int id, string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url), "URL cannot be null, consider passing String.Empty instead");
            }

            var request = _uow.Requests.Find(id);

            if (request == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Cannot find request with the given id");
            }

            request.Url = url;

            _uow.Requests.Update(request);
            _uow.SaveChanges();
        }
    }
}
