using System.ComponentModel.DataAnnotations;

namespace APIGirrafe.Data.Entities
{
    public class Header : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
