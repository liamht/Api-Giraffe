using APIGiraffe.Domain.Entities;

namespace APIGiraffe.Domain.Factories
{
    public class HeaderFactory : IHeaderFactory
    {
        public HeaderFactory()
        {
        }

        public Header Create(int id, string name, string value)
        {
            return new Header()
            {
                Id = id,
                Name = name,
                Value = value
            };
        }

        public Header Create(string name, string value)
        {
            return Create(0, name, value);
        }
    }
}

