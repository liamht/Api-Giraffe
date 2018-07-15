namespace APIGiraffe.Domain
{
    public class Header
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        internal static Header FromDataLayerObject(Data.Entities.Header entity)
        {
            return new Header()
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value
            };
        }

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
