using APIGiraffe.Domain;

namespace APIGiraffe.ApplicationServices.Headers.Commands.AddNewHeader.Factory
{
    public class HeaderFactory : IHeaderFactory
    {
        public Header Create(string name, string value)
        {
            return new Header()
            {
                Name = name,
                Value = value
            };
        }
    }
}
