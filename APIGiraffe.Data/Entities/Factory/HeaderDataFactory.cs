namespace APIGiraffe.Data.Entities.Factory
{
    public class HeaderDataFactory : IHeaderDataFactory
    {
        public Header Create(Domain.Entities.Header header)
        {
            return new Header()
            {
                Id = header.Id,
                Name = header.Name,
                Value = header.Value
            };
        }
    }
}
