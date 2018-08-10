using System.Collections.Generic;
using System.Linq;

namespace APIGiraffe.Domain.Entities
{
    public class RequestGroup
    {
        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public List<Request> Requests { get; internal set; }
    }
}
