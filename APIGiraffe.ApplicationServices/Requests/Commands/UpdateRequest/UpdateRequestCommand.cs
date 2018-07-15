﻿using APIGiraffe.Data.UnitOfWork;

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
            var request = _uow.Requests.Find(id);
            request.Url = url;

            _uow.Requests.Update(request);
            _uow.SaveChanges();
        }
    }
}