using System;
using System.Collections.Generic;
using System.Text;

namespace APIGirrafe.ApplicationServices.Requests.Queries.GetRequestGroups
{
    public class RequestGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<RequestIdWithName> Requests { get; set; }
    }
}
