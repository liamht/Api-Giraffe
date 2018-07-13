namespace APIGirrafe.Domain
{
    public class Header
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Value { get; private set; }

        internal static Header FromDataLayerObject(Data.Entities.Header entity)
        {
            return new Header()
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value
            };
        }

        internal Data.Entities.Header ToDataLayerObject()
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
