using System;
using APIGiraffe.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Remotion.Linq.Parsing;
using APIGiraffe.Domain.Factories;

namespace APIGiraffe.ApplicationServices.Headers.Commands.AddNewHeader
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
            if (name == null)
                throw new ArgumentNullException(nameof(name), "The name property is not nullable");
            
            if (value == null)
                throw new ArgumentNullException(nameof(value), "The value property is not nullable");
            
            var domainObject = _factory.Create(name, value);

            var request = _uow.Requests.Include(c => c.Headers).Single(c => c.Id == requestId);

            request.Headers.Add(domainObject.ToDataLayerObject());

            _uow.Requests.Update(request);
            _uow.SaveChanges();
        }
    }
}
