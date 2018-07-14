using APIGirrafe.ApplicationServices.Requests.Commands.AddNewHeader.Factory;
using APIGirrafe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APIGirrafe.ApplicationServices.Requests.Commands.AddNewHeader
{
    public class AddNewHeaderCommand : IAddNewHeaderCommand
    {
        private readonly IUnitOfWork _uow;
        private readonly IHeaderFactory _factory;

        public AddNewHeaderCommand(IUnitOfWork uow, IHeaderFactory factory)
        {
            _uow = uow;
            _factory = factory;
        }

        public void Execute(int requestId, string name, string value)
        {
            var domainObject = _factory.Create(name, value);

            var request = _uow.Requests.Include(c => c.Headers).Single(c => c.Id == requestId);

            request.Headers.Add(domainObject.ToDataLayerObject());

            _uow.Requests.Update(request);
            _uow.SaveChanges();
        }
    }
}
