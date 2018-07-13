using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGirrafe.Data.Entities
{
    public class Request : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int GroupId { get; set; }

        public List<Header> Headers { get; set; }

        [ForeignKey("GroupId")]
        public RequestGroup Group { get; set; }
    }
}
