namespace APIGiraffe.Domain.Entities
{
    public class Header
    {
        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public string Value { get; internal set; }

        public Data.Entities.Header ToDataLayerObject()
        {
            return new Data.Entities.Header()
            {
                Id = this.Id,
                Name = this.Name,
                Value = this.Value
            };
        }
    }
}
