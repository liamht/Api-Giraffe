using APIGiraffe.Data.UnitOfWork;
using System;

namespace APIGiraffe.ApplicationServices.Headers.Commands.UpdateHeader
{
    public class UpdateHeaderCommand : IUpdateHeaderCommand
    {
        private IUnitOfWork _uow;

        public UpdateHeaderCommand(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Execute(int id, string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name), "Name cannot be null or whitespace");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value), "Value cannot be null or whitespace");
            }

            var header = _uow.Headers.Find(id);

            if (header == null)
            {
                throw new ArgumentOutOfRangeException("Could not find header");
            }

            header.Name = name;
            header.Value = value;

            _uow.Headers.Update(header);
            _uow.SaveChanges();
        }
    }
}
