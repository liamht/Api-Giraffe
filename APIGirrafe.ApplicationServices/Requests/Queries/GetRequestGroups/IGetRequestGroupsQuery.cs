using System.Collections.Generic;

namespace APIGirrafe.ApplicationServices.Requests.Queries.GetRequestGroups
{
    public interface IGetRequestGroupsQuery
    {
        List<RequestGroup> Execute();
    }
}