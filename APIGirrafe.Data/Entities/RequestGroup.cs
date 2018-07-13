using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIGirrafe.Data.Entities
{
    public class RequestGroup : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
