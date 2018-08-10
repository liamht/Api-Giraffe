using System;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using APIGiraffe.Domain.Factories;
using APIGiraffe.Data.Entities.Factory;

namespace APIGiraffe.ApplicationServices.Headers.Commands.AddNewHeader
{
    public class AddNewHeaderCommand : IAddNewHeaderCommand
    {
        private readonly IUnitOfWork _uow;
        private readonly IHeaderFactory _factory;
        private readonly IHeaderDataFactory _dataFactory;

        public AddNewHeaderCommand(IUnitOfWork uow, IHeaderFactory factory, IHeaderDataFactory dataFactory)
        {
            _uow = uow;
            _factory = factory;
            _dataFactory = dataFactory;
        }

        public void Execute(int requestId, string name, string value)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "The name property is not nullable");
            
            if (value == null)
                throw new ArgumentNullException(nameof(value), "The value property is not nullable");
            
            var request = _uow.Requests.Include(c => c.Headers).Single(c => c.Id == requestId);

            var domainObject = _factory.Create(name, value);

            request.Headers.Add(_dataFactory.Create(domainObject));

            _uow.Requests.Update(request);
            _uow.SaveChanges();
        }
    }
}
