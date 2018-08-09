using System;
using System.Collections.Generic;
using System.Text;

namespace APIGiraffe.ApplicationServices.RequestGroups.Queries.GetRequestGroups
{
    public class RequestGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<RequestIdWithName> Requests { get; set; }
    }
}
